using System.Data;
using Dapper;
using ToDoApi.Context;
using ToDoApi.Contracts;
using ToDoApi.DTOs;
using ToDoApi.Models;

namespace ToDoApi.Repository;

public class ToDoRepository : IToDoRepository
{
    private readonly DapperContext _context;
    public ToDoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<ToDo> GetByIdAsync(int id)
    {
        var query = @"SELECT 
                        Id, Name, Description, CreatedDate, PersonId, CategoryId
                    FROM
                        ToDos
                    WHERE 
                        Id = @Id";

        var connection = _context.CreateConnection();

        var toDo = await connection.QuerySingleOrDefaultAsync<ToDo>(query, new { id });

        return toDo;
    }

    public async Task<IEnumerable<ToDo>> GetAllAsync()
    {
        var query = @"SELECT 
                        Id, Name, Description, CreatedDate, PersonId, CategoryId
                    FROM
                        ToDos";

        var connection = _context.CreateConnection();

        var toDos = await connection.QueryAsync<ToDo>(query);

        return toDos.ToList();
    }

    public async Task<ToDo> CreateAsync(ToDoForCreationDTO toDo)
    {
        var query = @"INSERT INTO ToDos(
                        Name, Description, CreatedDate, PersonId, CategoryId) 
                    VALUES(
                        @name, @descr, @createdDate, @personId, @categoryId);
                    
                    SELECT CAST(SCOPE_IDENTITY() AS int)";

        var parameters = new DynamicParameters();
        parameters.Add("name", toDo.Name, DbType.String);
        parameters.Add("descr", toDo.Description, DbType.String);
        parameters.Add("createdDate", toDo.CreatedDate, DbType.DateTime);
        parameters.Add("personId", toDo.PersonId, DbType.Int32);
        parameters.Add("categoryId", toDo.CategoryId, DbType.Int32);

        var connection = _context.CreateConnection();

        var id = await connection.QuerySingleOrDefaultAsync<int>(query, parameters);

        var createdToDo = new ToDo
        {
            Id = id,
            Name = toDo.Name,
            Description = toDo.Description,
            CreatedDate = toDo.CreatedDate,
            PersonId = toDo.PersonId,
            CategoryId = toDo.CategoryId
        };

        return createdToDo;
    }

    public async Task UpdateAsync(int id, ToDoForUpdateDTO toDo)
    {
        var query = @"UPDATE 
                        ToDos 
                    SET 
                        Name = @name, Description = @desc, PersonId = @personId, CategoryId = @categoryId
                    WHERE 
                        Id = @Id";

        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32);
        parameters.Add("name", toDo.Name, DbType.String);
        parameters.Add("desc", toDo.Description, DbType.String);
        parameters.Add("personId", toDo.PersonId, DbType.Int32);
        parameters.Add("categoryId", toDo.CategoryId, DbType.Int32);

        var connection = _context.CreateConnection();

        await connection.ExecuteAsync(query, parameters);
    }

    public async Task DeleteAsync(int id)
    {
        var query = @"DELETE FROM 
                        ToDos
                    WHERE 
                        Id = @Id";

        var connection = _context.CreateConnection();

        await connection.ExecuteAsync(query, new { id });
    }
}