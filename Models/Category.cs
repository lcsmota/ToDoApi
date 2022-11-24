namespace ToDoApi.Models;
public class Category : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public List<ToDo> Itens { get; set; } = new();
}
