using Microsoft.AspNetCore.Mvc;
using ToDoApi.Contracts;
using ToDoApi.DTOs;

namespace ToDoApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categRepository;
    public CategoriesController(ICategoryRepository categRepository)
    {
        _categRepository = categRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync()
    {
        try
        {
            var categories = await _categRepository.GetAllAsync();

            if (!categories.Any())
                return NotFound("Categories not found.");

            return Ok(categories);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}", Name = "GetCategoryById")]
    public async Task<IActionResult> GetCategoryAsync(int id)
    {
        try
        {
            var category = await _categRepository.GetByIdAsync(id);

            if (category is null) return NotFound("Category not found.");

            return Ok(category);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync(CategoryForCreationDTO category)
    {
        try
        {
            var createdCategory = await _categRepository.CreateAsync(category);

            return CreatedAtRoute("GetCategoryById", new { id = createdCategory.Id }, createdCategory);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategoryAsync(int id, CategoryForUpdateDTO category)
    {
        try
        {
            var dbCategory = await _categRepository.GetByIdAsync(id);

            if (dbCategory is null) return NotFound("Category not found.");

            await _categRepository.UpdateAsync(id, category);

            return NoContent();
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoryAsync(int id)
    {
        try
        {
            var dbCategory = await _categRepository.GetByIdAsync(id);

            if (dbCategory is null) return NotFound("Category not found.");

            await _categRepository.DeleteAsync(id);

            return NoContent();
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}/multipleResults")]
    public async Task<IActionResult> GetCategoryToDoMultipleResultsAsync(int id)
    {
        try
        {
            var category = await _categRepository.GetCategoryToDoMultipleResultsAsync(id);

            if (category is null) return NotFound("Category not found.");

            return Ok(category);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("multipleMapping")]
    public async Task<IActionResult> GetCategoryToDoMultipleMappingAsync()
    {
        try
        {
            var category = await _categRepository.GetCategoryToDoMultipleMappingAsync();

            return Ok(category);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
