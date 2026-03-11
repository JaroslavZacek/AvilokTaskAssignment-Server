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

        #region Get

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

    }
}
