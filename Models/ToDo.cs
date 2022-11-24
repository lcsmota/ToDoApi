namespace ToDoApi.Models;
public class ToDo : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public int PersonId { get; set; }
    public int CategoryId { get; set; }
}
