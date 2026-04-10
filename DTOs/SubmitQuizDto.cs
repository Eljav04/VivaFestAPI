namespace VivaFestAPI.DTOs;

public class SubmitQuizDto
{
    public string Name { get; set; } = string.Empty;
    public int TotalRemainingSeconds { get; set; }
    public List<ParticipantAnswerDto> Answers { get; set; } = new();
}
