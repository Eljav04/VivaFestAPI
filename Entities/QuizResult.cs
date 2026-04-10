namespace VivaFestAPI.Entities;

public class QuizResult
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int RightAnswersCount { get; set; }
    public int TotalRemainingSeconds { get; set; }
    public int WrongAnswersCount { get; set; }
    public int Points { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
}
