namespace ToDoApi.DTOs;

public class ToDoForUpdateDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PersonId { get; set; }
    public int CategoryId { get; set; }
}
