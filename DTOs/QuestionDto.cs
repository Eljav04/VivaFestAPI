namespace VivaFestAPI.DTOs;

public class QuestionDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<AnswerDto> Answers { get; set; } = new();
}
