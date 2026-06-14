using AutoMapper;
using AvilokTaskAssignment.Data.Interfaces;
using AvilokTaskAssignment.Data.Models;
using AvilokTaskAssignment.Api.DTO;
using AvilokTaskAssignment.Api.Interfaces;
using AvilokTaskAssignment.Api.Helpers;

using TaskStatus = AvilokTaskAssignment.Data.Models.TaskStatus;

using Microsoft.AspNetCore.Identity;

namespace AvilokTaskAssignment.Api.Managers
{
    public class TaskManager : ITaskManager
    {
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public TaskManager(ITaskRepository taskRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _userManager = userManager;
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

        /// <summary>
        /// Vratí detailní informace o zakázce podle jejího ID.
        /// </summary>
        public async Task<TaskDetailDto> GetTaskDetailAsync(Guid taskId)
        {
            var task = await _taskRepository.GetDetailAsync(taskId);
            
            if (task == null)
                throw new Exception("Zakázka nebyla nalezena.");

            return _mapper.Map<TaskDetailDto>(task);
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

        public async Task AssignTaskAsync(Guid taskId, Guid? userID, List<string> roles)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
                throw new Exception("Zakázka nebyla nalezena.");

            PermissionHelper.EnsureTaskLeaderAccess(roles, task.WorkType, "Nemáte oprávnění přiřadit tuto zakázku.");

            if (userID != null)
            {
               var roleName = task.WorkType.GetRoleName();

                if (!roles.Contains(roleName) && !roles.Contains($"Leader {roleName}"))
                    throw new Exception("Uživatel nemá oprávnění pro tento typ zakázky."); 
            }

            task.AssignedUserId = userID;

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

        /// <summary>
        /// Metoda pro úpravu short a long description a deadline. Adminovy umožní měnit i worktype
        /// </summary>
        public async Task UpdateTaskAsync(Guid taskId, UpdateTaskDto dto, List<string> roles)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
                throw new Exception("Zakázka nebyla nalezena.");

            PermissionHelper.EnsureTaskLeaderAccess(roles, task.WorkType, "Nemáte oprávnění upravit zakázku.");

            if (dto.WorkType != task.WorkType && !roles.Contains("Admin"))
                throw new Exception("Pouze administrátor může změnit typ zakázky");

            task.ShortDescription = dto.ShortDescription;

            task.LongDescription = dto.LongDescription;

            task.WorkType = dto.WorkType;

            task.Deadline = dto.Deadline;

            await _taskRepository.SaveChangesAsync();
        }

        #endregion

        #region Delete
        public async Task DeleteTaskAsync(Guid taskId, List<string> roles)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
                throw new Exception("Zakázka nebyla nalezena.");

            if (task.Status == TaskStatus.Finished)
                throw new Exception("Dokončenou zakázku nelze smazat.");

            PermissionHelper.EnsureTaskLeaderAccess(roles, task.WorkType, "Nemáte oprávnění smazat zakázku.");

            _taskRepository.Remove(task);

            await _taskRepository.SaveChangesAsync();
        }
        #endregion

    }
}
