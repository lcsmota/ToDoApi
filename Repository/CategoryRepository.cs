using System.Data;
using Dapper;
using ToDoApi.Context;
using ToDoApi.Contracts;
using ToDoApi.DTOs;
using ToDoApi.Models;

namespace ToDoApi.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly DapperContext _context;
    public CategoryRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var query = @"SELECT 
                            Id, Name, Description 
                        FROM 
                            Categories";

        var connection = _context.CreateConnection();

        var categories = await connection.QueryAsync<Category>(query);

        return categories.ToList();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        var query = @"SELECT 
                            Id, Name, Description
                        FROM 
                            Categories
                        WHERE 
                            Id = @Id";

        var connection = _context.CreateConnection();

        var category = await connection.QuerySingleOrDefaultAsync<Category>(query, new { id });

        return category;
    }

    public async Task<Category> CreateAsync(CategoryForCreationDTO category)
    {
        var query = @"INSERT INTO Categories(
                            Name, Description) VALUES(@name, @descr);
                    
                    SELECT CAST(SCOPE_IDENTITY() AS int)";

        var parameters = new DynamicParameters();
        parameters.Add("name", category.Name, DbType.String);
        parameters.Add("descr", category.Description, DbType.String);

        var connection = _context.CreateConnection();

        var id = await connection.QuerySingleOrDefaultAsync<int>(query, parameters);

        var createdCategory = new Category
        {
            Id = id,
            Name = category.Name,
            Description = category.Description
        };

        return createdCategory;
    }

    public async Task UpdateAsync(int id, CategoryForUpdateDTO category)
    {
        var query = @"UPDATE 
                            Categories 
                        SET 
                            Name = @name, Description = @descr
                        WHERE 
                            Id = @Id";

        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32);
        parameters.Add("name", category.Name, DbType.String);
        parameters.Add("descr", category.Description, DbType.String);

        var connection = _context.CreateConnection();

        await connection.ExecuteAsync(query, parameters);
    }

    public async Task DeleteAsync(int id)
    {
        var query = @"DELETE FROM 
                            Categories
                        WHERE 
                            Id = @Id";

        var connection = _context.CreateConnection();

        await connection.ExecuteAsync(query, new { id });
    }

    public async Task<Category> GetCategoryToDoMultipleResultsAsync(int id)
    {
        var query = @"SELECT 
                            Id, Name, Description 
                        FROM 
                            Categories 
                        WHERE 
                            Id = @Id;
                        
                        SELECT 
                            Id, Name, Description, CreatedDate, PersonId, CategoryId 
                        FROM 
                            ToDos 
                        WHERE 
                            CategoryId = @Id";

        var connection = _context.CreateConnection();

        using var multi = await connection.QueryMultipleAsync(query, new { id });

        var category = await multi.ReadSingleOrDefaultAsync<Category>();
        if (category != null)
            category.Itens = (await multi.ReadAsync<ToDo>()).ToList();

        return category;
    }

    public async Task<List<Category>> GetCategoryToDoMultipleMappingAsync()
    {
        var query = @"SELECT
                            cat.Id, cat.Name, cat.Description,
                            todo.Id, todo.Name, todo.Description, CreatedDate, PersonId, CategoryId 
                        FROM 
                            Categories cat
                        JOIN 
                            ToDos todo
                        ON cat.Id = todo.CategoryId";

        var connection = _context.CreateConnection();

        var categoryDic = new Dictionary<int, Category>();

        var categories = await connection.QueryAsync<Category, ToDo, Category>(
            query,
            (category, todo) =>
            {
                if (!categoryDic.TryGetValue(category.Id, out var currentCategory))
                {
                    currentCategory = category;
                    categoryDic.Add(currentCategory.Id, currentCategory);
                }

                currentCategory.Itens.Add(todo);

                return currentCategory;
            });

        return categories.Distinct().ToList();
    }
}

