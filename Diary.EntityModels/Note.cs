namespace Diary.EntityModels;

public class Note
{
    public int Id { get; set; } // Primary key
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}