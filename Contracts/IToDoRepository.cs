using ToDoApi.DTOs;
using ToDoApi.Models;

namespace ToDoApi.Contracts;

public interface IToDoRepository : IGenericRepository<ToDo, ToDoForCreationDTO, ToDoForUpdateDTO>
{

}
