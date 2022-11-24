using ToDoApi.DTOs;
using ToDoApi.Models;

namespace ToDoApi.Contracts;

public interface IPersonRepository : IGenericRepository<Person, PersonForCreationDTO, PersonForUpdateDTO>
{
    Task<Person> GetPersonToDoMultipleResultsAsync(int id);
    Task<List<Person>> GetPersonToDoMultipleMappingAsync();
}
