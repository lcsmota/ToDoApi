namespace ToDoApi.Models;
public class Person : BaseEntity
{
    public List<ToDo> ToDos { get; set; } = new();
}
