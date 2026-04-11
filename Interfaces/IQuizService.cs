using VivaFestAPI.DTOs;
using VivaFestAPI.Entities;

namespace VivaFestAPI.Interfaces;

public interface IQuizService
{
    // Admin
    Task<List<QuestionDto>> GetAllQuestionsAsync();
    Task<QuestionDto> CreateQuestionAsync(QuestionDto dto);
    Task<QuestionDto?> UpdateQuestionAsync(int id, QuestionDto dto);
    Task<bool> DeleteQuestionAsync(int id);
    Task ResetLeaderboardAsync();

    // Participant
    Task<List<ParticipantQuestionDto>> GetParticipantQuestionsAsync();
    Task<QuizResult> SubmitQuizAsync(SubmitQuizDto dto);

    // Activity
    Task<bool> IsQuizActiveAsync();
    Task<bool> ToggleQuizActivityAsync();

    // Global
    Task<List<QuizResult>> GetLeaderboardAsync();
}
