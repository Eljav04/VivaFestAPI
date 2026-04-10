namespace VivaFestAPI.Entities;

public class Answer
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsRight { get; set; }
    public int QuestionId { get; set; }

    public Question? Question { get; set; }
}
