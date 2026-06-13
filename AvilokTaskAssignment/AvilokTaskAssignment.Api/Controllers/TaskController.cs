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
        private readonly ITaskCommentManager _taskCommentManager;

        public TaskController(ITaskManager taskManager, ITaskCommentManager taskCommentManager)
        {
            _taskManager = taskManager;
            _taskCommentManager = taskCommentManager;
        }



        #region Get
        // ------------------------------------------------------------------------------------------------------
        // Get metody pro zakázky
        // ------------------------------------------------------------------------------------------------------;

        /// <summary>
        /// Vypíše všechny úkoly, které jsou v systému. Frontend bude rozdělovat úkoly do kategorií podle WorkType.
        /// </summary>
        [HttpGet]
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<TaskDetailDto>> GetTaskDetail(Guid taskId)
        {
            var task = await _taskManager.GetTaskDetailAsync(taskId);
            
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // ------------------------------------------------------------------------------------------------------
        // Get metody pro komentáře k zakázkám
        // ------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Vypíše všechny komentáře k úkolu podle ID úkolu.
        /// </summary>
        [HttpGet("{takId}/comments")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments(Guid taskId)
        {
            var comments = await _taskCommentManager.GetCommentsAsync(taskId);

            return Ok(comments);
        }

        #endregion

        #region Post

        // ------------------------------------------------------------------------------------------------------
        // Post metody pro zakázky
        // ------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Vytvoří nový úkol.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin, Leader Developer, Leader Graphic, Leader Story")]
        public async Task<IActionResult> CreateTask(CreateTaskDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var task = await _taskManager.CreateTaskAsync(dto, userId);

            return Ok(task);
        }

        // -------------------------------------------------------------------------------------------------------
        // Post metody pro komentáře k zakázkám
        // -------------------------------------------------------------------------------------------------------

        [HttpPost("{taskId}/comments")]
        [Authorize]
        public async Task<IActionResult> AddComment(Guid taskId, [FromBody] CreateCommentDto dto)
        {
            var authorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _taskCommentManager.AddCommentAsync(taskId, authorId, dto.Text);

            return Ok(new
            {
                Massage = "Komentář byl úspěšně přidán k zakázce."
            });
        }



        #endregion

        #region Put




        #endregion

        #region Patch

        /// <summary>
        /// Přiřadí zakázku uživateli.
        /// </summary>
        [HttpPatch("{taskId}/assign")]
        public async Task<IActionResult> AssignTask(Guid taskId, [FromBody] AssignTaskDto assignTaskDto)
        {
            var role = User.FindAll(ClaimTypes.Role)
                            .Select(r => r.Value)
                            .ToList();

            await _taskManager.AssignTaskAsync(taskId, assignTaskDto.AssignedUserId, role);

            return Ok(new
            {
                Massage = "Zakázka byla úspěšně přiřazena uživateli."
            });

        }


        /// <summary>
        /// Změní stav úkolu. Například z "InProgress" na "Completed".
        /// Prozatím pro všechny. Časem by bylo možné přidat oprávnění, aby stav mohl měnit pouze uživatel, kterému je úkol přiřazen,leader nebo administrátor.
        /// </summary>
        [HttpPatch("{taskId}/status")]
        public async Task<IActionResult> ChanceStatus(Guid taskId, [FromBody] UpdateTaskStatusDto newStatus)
        {
            await _taskManager.UpdateStatusAsync(taskId, newStatus.Status);

            return Ok(new
            {
                massage = "Status byl úspěšně změněn"
            });
        }

        #endregion

        #region Delete

        /// <summary>
        /// Metoda pro smazání úkolu. Smaže úkol z databáze. Používá se pro odstranění neaktuálních nebo chybně vytvořených úkolů.
        /// Prozatím pro všechny. Časem by bylo možné přidat oprávnění, aby úkol mohl smazat pouze jeho tvůrce nebo administrátor.
        /// </summary>
        [HttpDelete("{taskId}")]
        [Authorize(Roles = "Admin, Leader Developer, Leader Graphic, Leader Story")]
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
