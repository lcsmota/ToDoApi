using System.Data;
using Dapper;
using ToDoApi.Context;
using ToDoApi.Contracts;
using ToDoApi.DTOs;
using ToDoApi.Models;

namespace ToDoApi.Repository;

public class PersonRepository : IPersonRepository
{
    private readonly DapperContext _context;
    public PersonRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        var query = @"SELECT 
                        Id, Name 
                    FROM 
                        People";

        var connection = _context.CreateConnection();

        var people = await connection.QueryAsync<Person>(query);

        return people.ToList();
    }

    public async Task<Person> GetByIdAsync(int id)
    {
        var query = @"SELECT 
                        Id, Name 
                    FROM 
                        People
                    WHERE 
                        Id = @Id";

        var connection = _context.CreateConnection();

        var person = await connection.QuerySingleOrDefaultAsync<Person>(query, new { id });

        return person;
    }

    public async Task<Person> CreateAsync(PersonForCreationDTO person)
    {
        var query = @"INSERT INTO People(Name) VALUES(@name);
                    
                    SELECT CAST(SCOPE_IDENTITY() AS int)";

        var parameters = new DynamicParameters();
        parameters.Add("name", person.Name, DbType.String);

        var connection = _context.CreateConnection();

        var id = await connection.QuerySingleOrDefaultAsync<int>(query, parameters);

        var createdPerson = new Person
        {
            Id = id,
            Name = person.Name
        };

        return createdPerson;
    }

    public async Task UpdateAsync(int id, PersonForUpdateDTO person)
    {
        var query = @"UPDATE 
                        People 
                    SET 
                        Name = @name
                    WHERE 
                        Id = @Id";

        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32);
        parameters.Add("name", person.Name, DbType.String);

        var connection = _context.CreateConnection();

        await connection.ExecuteAsync(query, parameters);
    }

    public async Task DeleteAsync(int id)
    {
        var query = @"DELETE FROM 
                        People
                    WHERE 
                        Id = @Id";

        var connection = _context.CreateConnection();

        await connection.ExecuteAsync(query, new { id });
    }

    public async Task<Person> GetPersonToDoMultipleResultsAsync(int id)
    {
        var query = @"SELECT 
                        Id, Name 
                    FROM 
                        People 
                    WHERE 
                        Id = @Id;
                    
                    SELECT 
                        Id, Name, Description, CreatedDate, PersonId, CategoryId 
                    FROM 
                        ToDos 
                    WHERE 
                        PersonId = @Id";

        var connection = _context.CreateConnection();

        using var multi = await connection.QueryMultipleAsync(query, new { id });

        var person = await multi.ReadSingleOrDefaultAsync<Person>();
        if (person != null)
            person.ToDos = (await multi.ReadAsync<ToDo>()).ToList();

        return person;
    }

    public async Task<List<Person>> GetPersonToDoMultipleMappingAsync()
    {
        var query = @"SELECT
                        p.Id, p.Name,
                        todo.Id, todo.Name, Description, CreatedDate, PersonId, CategoryId 
                    FROM 
                        People p
                    JOIN 
                        ToDos todo
                    ON 
                        p.Id = todo.PersonId";

        var connection = _context.CreateConnection();

        var personDic = new Dictionary<int, Person>();

        var people = await connection.QueryAsync<Person, ToDo, Person>(
            query,
            (person, todo) =>
            {
                if (!personDic.TryGetValue(person.Id, out var currentPerson))
                {
                    currentPerson = person;
                    personDic.Add(currentPerson.Id, currentPerson);
                }

                currentPerson.ToDos.Add(todo);
                return currentPerson;
            });

        return people.Distinct().ToList();
    }
}
