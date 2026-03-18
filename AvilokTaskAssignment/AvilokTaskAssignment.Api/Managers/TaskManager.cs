using AutoMapper;
using AvilokTaskAssignment.Data.Interfaces;
using AvilokTaskAssignment.Data.Models;
using AvilokTaskAssignment.Api.DTO;

using TaskStatus = AvilokTaskAssignment.Data.Models.TaskStatus;

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
        /// Vratí filtrovaný seznam zakázek. Pokud není zadán žádný filtr, vrátí všechny zakázky.
        /// </summary>
        public async Task<IEnumerable<TaskListDto>> GetFilteredTasksAsync(
            WorkType? workType,
            Guid? createdById,
            Guid? assignedUserId,
            TaskStatus? status)

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

        

        #endregion

        #region Patch

        /// <summary>
        /// Přiřadí zakázku uživateli a změní její stav na "InProgress".
        /// </summary>

        public async Task AssignTaskAsync(Guid taskId, Guid userID, List<string> role)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            var user = await _taskRepository.GetByIdAsync(userID);

            if (task == null)
                throw new Exception("Zakázka nebyla nalezena.");

            if (!role.Contains(task.WorkType.ToString()) || !role.Contains("Leader " + task.WorkType.ToString()))
                throw new Exception("Uživatel nemá oprávnění pro tento typ zakázky.");

            task.AssignedUserId = userID;
            task.Status = TaskStatus.InProgress;

            _taskRepository.Update(task);
            await _taskRepository.SaveChangesAsync();
        }


        /// <summary>
        /// Upraví stav zakázky. Například z "InProgress" na "Finished".
        /// </summary>
        public async Task UpdateStatusAsync(Guid taskId, TaskStatus newStatus)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
                throw new Exception("Zakázka nebyla nalezena.");

            task.Status = newStatus;

            _taskRepository.Update(task);
            await _taskRepository.SaveChangesAsync();
        }

        #endregion

        #region Delete
        public async Task<bool> DeleteTaskAsync(Guid taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
                return false;

            _taskRepository.Remove(task);
            await _taskRepository.SaveChangesAsync();

            return true;
        }
        #endregion
    }
}
