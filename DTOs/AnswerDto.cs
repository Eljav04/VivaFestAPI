namespace VivaFestAPI.DTOs;

public class AnswerDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsRight { get; set; }
}
