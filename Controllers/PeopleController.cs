using Microsoft.AspNetCore.Mvc;
using ToDoApi.Contracts;
using ToDoApi.DTOs;

namespace ToDoApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IPersonRepository _personRepository;
    public PeopleController(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetPeopleAsync()
    {
        try
        {
            var people = await _personRepository.GetAllAsync();

            return Ok(people);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}", Name = "GetPersonById")]
    public async Task<IActionResult> GetPersonAsync(int id)
    {
        try
        {
            var person = await _personRepository.GetByIdAsync(id);

            if (person is null) return NotFound("Person not found.");

            return Ok(person);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson(PersonForCreationDTO person)
    {
        try
        {
            var createdPerson = await _personRepository.CreateAsync(person);

            return CreatedAtRoute("GetPersonById", new { id = createdPerson.Id }, createdPerson);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(int id, PersonForUpdateDTO person)
    {
        try
        {
            var dbPerson = await _personRepository.GetByIdAsync(id);

            if (dbPerson is null) return NotFound("Person not found.");

            await _personRepository.UpdateAsync(id, person);

            return NoContent();
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersonAsync(int id)
    {
        try
        {
            var dbPerson = await _personRepository.GetByIdAsync(id);

            if (dbPerson is null) return NotFound("Person not found.");

            await _personRepository.DeleteAsync(id);

            return NoContent();
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}/multipleResults")]
    public async Task<IActionResult> GetPersonToDoMultipleResultsAsync(int id)
    {
        try
        {
            var person = await _personRepository.GetPersonToDoMultipleResultsAsync(id);

            if (person is null) return NotFound("Person not found.");

            return Ok(person);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("multipleMapping")]
    public async Task<IActionResult> GetPersonToDoMultipleMappingAsync()
    {
        try
        {
            var person = await _personRepository.GetPersonToDoMultipleMappingAsync();

            return Ok(person);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
