using ToDoApi.DTOs;
using ToDoApi.Models;

namespace ToDoApi.Contracts;

public interface ICategoryRepository : IGenericRepository<Category, CategoryForCreationDTO, CategoryForUpdateDTO>
{
    Task<Category> GetCategoryToDoMultipleResultsAsync(int id);
    Task<List<Category>> GetCategoryToDoMultipleMappingAsync();
}
