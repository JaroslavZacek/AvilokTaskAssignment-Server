using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AvilokTaskAssignment.Api.Managers;
using AvilokTaskAssignment.Api.DTO;
using AvilokTaskAssignment.Data.Models;
using AvilokTaskAssignment.Api.Interfaces;

using TaskStatus = AvilokTaskAssignment.Data.Models.TaskStatus;

using System.Security.Claims;

namespace AvilokTaskAssignment.Api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskManager _taskManager;

        public TaskController(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        

        #region Get

        /// <summary>
        /// Vypíše všechny úkoly, které jsou v systému. Frontend bude rozdělovat úkoly do kategorií podle WorkType.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskListDto>>> GetTasks([FromQuery] 
            WorkType? workType,
            Guid? createdById,
            Guid? assignedUserId,
            TaskStatus? status)
        {
            var tasks = await _taskManager.GetFilteredTasksAsync(workType, createdById, assignedUserId, status);
            return Ok(tasks);
        }

        /// <summary>
        /// Vypíše detail úkolu podle jeho ID. Detail obsahuje všechny informace o úkolu, včetně jména a ID uživatele, kterému je úkol přiřazen, a jména a ID uživatele, který úkol vytvořil.
        /// </summary>
        [HttpGet("{taskId}")]
        public async Task<ActionResult<TaskDetailDto>> GetTaskDetail(Guid taskId)
        {
            var task = await _taskManager.GetTaskDetailAsync(taskId);
            
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        #endregion

        #region Post
        /// <summary>
        /// Vytvoří nový úkol.
        /// </summary>
        [HttpPost]
        //[Authorize (Roles = "Admin,Leader Developer,Leader Graphic,Leader Story")]
        public async Task<IActionResult> CreateTask(CreateTaskDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var task = await _taskManager.CreateTaskAsync(dto, userId);

            return Ok(task);
        }

        

        #endregion

        #region Put

        


        #endregion

        #region Patch

        /// <summary>
        /// Přiřadí zakázku uživateli a změní její stav na "InProgress".
        /// </summary>
        [HttpPost("{taskId}/take")]
        public async Task<IActionResult> AssignTask(Guid taskId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var role = User.FindAll(ClaimTypes.Role)
                            .Select(r => r.Value)
                            .ToList();

            await _taskManager.AssignTaskAsync(taskId, userId, role);

            return Ok();
        }


        /// <summary>
        /// Změní stav úkolu. Například z "InProgress" na "Completed".
        /// Prozatím pro všechny. Časem by bylo možné přidat oprávnění, aby stav mohl měnit pouze uživatel, kterému je úkol přiřazen,leader nebo administrátor.
        /// </summary>
        [HttpPatch("{taskId}/status")]
        public async Task<IActionResult> ChanceStatus(Guid taskId, [FromBody] UpdateTaskStatusDto newStatus)
        {
            await _taskManager.UpdateStatusAsync(taskId, newStatus.Status);

            return Ok();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Metoda pro smazání úkolu. Smaže úkol z databáze. Používá se pro odstranění neaktuálních nebo chybně vytvořených úkolů.
        /// Prozatím pro všechny. Časem by bylo možné přidat oprávnění, aby úkol mohl smazat pouze jeho tvůrce nebo administrátor.
        /// </summary>
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            var deleted = await _taskManager.DeleteTaskAsync(taskId);

            if (!deleted)
                return NotFound();

            return NoContent();
        }


        // Sem bude časem přidána metoda pro odhlášení pracovníka z úkolu, která změní stav úkolu zpět na "New" a odstraní přiřazení uživatele.
        #endregion
    }
}
