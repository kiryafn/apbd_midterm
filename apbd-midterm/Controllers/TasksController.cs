using apbd_midterm.DTO;
using apbd_midterm.Exceptions;
using apbd_midterm.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace apbd_midterm.Controllers;

[Controller]
[Route("/api/[controller]")]
public class TasksController : ControllerBase
{
    public readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }
    
    [HttpGet]
    [Route("{memberId:int}")]
    public async Task<IActionResult> GetMemberTasksAsync(int memberId)
    {
        try
        {
            var memberTasks = await _taskService.GetMemberTasksAsync(memberId);
            return Ok(memberTasks);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (BadDataException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Message = "An unexpected error occurred while processing the request.",
                Details = ex.Message
            });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTaskAsync([FromBody] TaskCreationRequest taskRequest)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTaskId = await _taskService.CreateTaskAsync(taskRequest);

            return CreatedAtRoute(createdTaskId);
        }
        catch (BadDataException ex)
        {
            return BadRequest(new
            {
                Message = "The input data is invalid.",
                Details = ex.Message
            });
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(ex.HResult, ex.Message);
        }
    }

}