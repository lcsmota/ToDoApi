namespace ToDoApi.DTOs;

public class ToDoForCreationDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public int PersonId { get; set; }
    public int CategoryId { get; set; }
}
