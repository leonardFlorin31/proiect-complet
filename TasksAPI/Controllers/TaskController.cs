using Microsoft.AspNetCore.Mvc;
using TasksAPI.Models;
using TasksAPI.Services;

namespace TasksAPI.Controllers
{
    /// <summary>
    /// Controller for managing tasks
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        ITaskCollectionService _taskCollectionService;

        /// <summary>
        ///  Constructor
        /// </summary>
        public TaskController(ITaskCollectionService taskCollectionService)
        {
            _taskCollectionService = taskCollectionService ?? throw new ArgumentNullException(nameof(TaskCollectionService));
        }


        /// <summary>
        /// Retrieves all tasks.
        /// </summary>
        /// <returns>Returns a list of all tasks.</returns>
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            List<TaskModel> tasks = await _taskCollectionService.GetAll();
            if (tasks == null)
            {
                return BadRequest("Could not retrieve tasks");
            }

            return Ok(tasks);
        }

        /// <summary>
        /// Retrieves a task by id
        /// </summary>
        /// <param name="id">The id of the task to retrieve</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            TaskModel retrievedTask = await _taskCollectionService.Get(id);

            if (retrievedTask == null)
            {
                return BadRequest("Id was not found");
            }

            return Ok($"Returned task is: {retrievedTask}");
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="task">The task to create.</param>
        /// <returns>Returns the created task ID.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskModel task)
        {
            if (task == null)
            {
                return BadRequest("Task cannot be null");
            }

            bool result = await _taskCollectionService.Create(task);

            if (result == false)
            {
                return BadRequest("Could not create task!");
            }

            return Ok($"Added task: {task.Id}");
        }

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="task">The updated task information.</param>
        /// <returns>Returns a message indicating the success of the update.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask( string id, [FromBody] TaskModel task)
        {
            if (task == null)
            {
                return BadRequest("Task cannot be null");
            }

            bool result = await _taskCollectionService.Update(Guid.Parse(id), task);

            if (result == false)
            {
                return BadRequest("Could not update task");
            }

            return Ok("Task updated succesfully!");
        }

        /// <summary>
        /// Deletes a task by ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>Returns a message indicating the success of the deletion.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _taskCollectionService.Delete(id);

            if (result == false)
            {
                return NotFound($"Task with ID {id} not found");
            }

            return Ok($"Task with ID {id} deleted successfully");
        }

        /// <summary>
        /// Retrieves all tasks with specified status
        /// </summary>
        /// <param name="status">The status to filter</param>
        /// <returns></returns>
        [HttpGet("byStatus/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            List<TaskModel> tasks = await _taskCollectionService.GetTasksByStatus(status);
            
            if (tasks == null)
            {
                return BadRequest("Could not retrieve tasks");
            }

            return Ok(tasks);
        }
    }
}
