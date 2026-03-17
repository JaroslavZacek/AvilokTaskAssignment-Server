using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AvilokTaskAssignment.Api.Managers;
using AvilokTaskAssignment.Api.DTO;
using AvilokTaskAssignment.Data.Models;
using TaskStatus = AvilokTaskAssignment.Data.Models.TaskStatus;
using System.Security.Claims;

namespace AvilokTaskAssignment.Api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly TaskManager _taskManager;

        public TaskController(TaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        

        #region Get

        /// <summary>
        /// Vypíše všechny úkoly, které jsou v systému. Frontend bude rozdělovat úkoly do kategorií podle WorkType.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskListDto>>> GetTasks([FromQuery] 
            WorkType? worktype,
            Guid? createdById,
            Guid? assignedUserId,
            TaskStatus? status)
        {
            var tasks = await _taskManager.GetFilteredTasksAsync(worktype, createdById, assignedUserId, status);
            return Ok(tasks);
        }

        #endregion

        #region Post
        /// <summary>
        /// Vytvoří nový úkol.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var task = await _taskManager.CreateTaskAsync(dto, userId);

            return Ok(task);
        }

        

        #endregion

        #region Put

        /// <summary>
        /// Přiřadí zakázku uživateli a změní její stav na "InProgress".
        /// </summary>
        [HttpPost("{taskId}/take")]
        public async Task<IActionResult> AssignTask(Guid taskId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _taskManager.AssignTaskAsync(taskId, userId);

            return Ok();
        }

        #endregion
    }
}
