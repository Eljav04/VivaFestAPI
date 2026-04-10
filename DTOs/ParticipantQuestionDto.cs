namespace VivaFestAPI.DTOs;

public class ParticipantQuestionDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<ParticipantAnswerOptionDto> Answers { get; set; } = new();
}
