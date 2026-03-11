using AutoMapper;
using AvilokTaskAssignment.Data.Interfaces;
using AvilokTaskAssignment.Data.Models;
using AvilokTaskAssignment.Api.DTO;

namespace AvilokTaskAssignment.Api.Managers
{
    public class TaskManager
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskManager(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        #region GET

        /// <summary>
        /// Vratí filtrovaný seznam zakázek.
        /// </summary>
        public async Task<IEnumerable<TaskListDto>> GetFilteredTasksAsync(
            WorkType? workType,
            Guid? createdById,
            Guid? assignedUserId,
            AvilokTaskAssignment.Data.Models.TaskStatus? status)

        {
            var tasks = await _taskRepository.GetFilteredAsync(workType, createdById, assignedUserId, status);

            return _mapper.Map<IEnumerable<TaskListDto>>(tasks);
        }
        #endregion

        #region POST

        /// <summary>
        /// Vytvoří novou zakázku.
        /// </summary>
        public async Task<Guid> CreateTaskAsync(CreateTaskDto dto, Guid createdById)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                ShortDescription = dto.ShortDescription,
                LongDescription = dto.LongDescription,
                WorkType = dto.WorkType,
                Deadline = dto.Deadline,
                CreatedById = createdById,
            };

            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync();

            return task.Id;
        }

        #endregion

        #region PUT

        /// <summary>
        /// Přiřadí zakázku uživateli a změní její stav na "InProgress".
        /// </summary>

        public async Task AssignTaskAsync(Guid taskId, Guid userID)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
                throw new Exception("Zakázka nebyla nalezena.");

            task.AssignedUserId = userID;
            task.Status = Data.Models.TaskStatus.InProgress;

            _taskRepository.Update(task);
            await _taskRepository.SaveChangesAsync();
        }


        public async Task UpdateStatusAsync(Guid taskId, Data.Models.TaskStatus newStatus)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
                throw new Exception("Zakázka nebyla nalezena.");

            task.Status = newStatus;

            _taskRepository.Update(task);
            await _taskRepository.SaveChangesAsync();
        }

        #endregion
    }
}
