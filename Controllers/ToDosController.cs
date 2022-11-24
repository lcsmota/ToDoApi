using Microsoft.AspNetCore.Mvc;
using ToDoApi.Contracts;
using ToDoApi.DTOs;

namespace ToDoApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ToDosController : ControllerBase
{
    private readonly IToDoRepository _toDoRepository;
    public ToDosController(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }


    [HttpGet]
    public async Task<IActionResult> GetToDosAsync()
    {
        try
        {
            var todos = await _toDoRepository.GetAllAsync();

            if (!todos.Any())
                return NotFound("ToDos not found.");

            return Ok(todos);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}", Name = "GetToDoById")]
    public async Task<IActionResult> GetToDoAsync(int id)
    {
        try
        {
            var todo = await _toDoRepository.GetByIdAsync(id);

            if (todo is null) return NotFound("ToDo not found.");

            return Ok(todo);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateToDoAsync(ToDoForCreationDTO toDo)
    {
        try
        {
            var createdToDo = await _toDoRepository.CreateAsync(toDo);

            return CreatedAtRoute("GetToDoById", new { id = createdToDo.Id }, createdToDo);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateToDoAsync(int id, ToDoForUpdateDTO toDo)
    {
        try
        {
            var dbToDo = await _toDoRepository.GetByIdAsync(id);

            if (dbToDo is null) return NotFound("ToDo not found.");

            await _toDoRepository.UpdateAsync(id, toDo);

            return NoContent();
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteToDoAsync(int id)
    {
        try
        {
            var dbToDo = await _toDoRepository.GetByIdAsync(id);

            if (dbToDo is null) return NotFound("ToDo not found.");

            await _toDoRepository.DeleteAsync(id);

            return NoContent();
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
