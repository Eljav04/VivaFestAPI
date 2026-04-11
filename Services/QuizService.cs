using Microsoft.EntityFrameworkCore;
using VivaFestAPI.Data;
using VivaFestAPI.DTOs;
using VivaFestAPI.Entities;
using VivaFestAPI.Interfaces;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace VivaFestAPI.Services;

public class QuizService : IQuizService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public QuizService(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<List<QuestionDto>> GetAllQuestionsAsync()
    {
        var questions = await _context.Questions
            .AsNoTracking()
			.Include(q => q.Answers)
            .ToListAsync();

        return questions.Select(q => new QuestionDto
        {
            Id = q.Id,
            Title = q.Title,
            Answers = q.Answers.Select(a => new AnswerDto
            {
                Id = a.Id,
                Title = a.Title,
                IsRight = a.IsRight
            }).ToList()
        }).ToList();
    }

    public async Task<QuestionDto> CreateQuestionAsync(QuestionDto dto)
    {
        var question = new Question
        {
            Title = dto.Title,
            Answers = dto.Answers.Select(a => new Answer
            {
                Title = a.Title,
                IsRight = a.IsRight
            }).ToList()
        };

        _context.Questions.Add(question);
        await _context.SaveChangesAsync();

        dto.Id = question.Id;
        for (int i = 0; i < question.Answers.Count; i++)
        {
            dto.Answers[i].Id = question.Answers.ElementAt(i).Id;
        }

        return dto;
    }

    public async Task<QuestionDto?> UpdateQuestionAsync(int id, QuestionDto dto)
    {
        var question = await _context.Questions
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (question == null) return null;

        question.Title = dto.Title;

        // Simplify answer update by removing old and adding new
        _context.Answers.RemoveRange(question.Answers);

        question.Answers = dto.Answers.Select(a => new Answer
        {
            Title = a.Title,
            IsRight = a.IsRight
        }).ToList();

        await _context.SaveChangesAsync();

        dto.Id = question.Id;
        for (int i = 0; i < question.Answers.Count; i++)
        {
            dto.Answers[i].Id = question.Answers.ElementAt(i).Id;
        }

        return dto;
    }

    public async Task<bool> DeleteQuestionAsync(int id)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question == null) return false;

        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task ResetLeaderboardAsync()
    {
        var allResults = await _context.QuizResults.ToListAsync();
        _context.QuizResults.RemoveRange(allResults);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ParticipantQuestionDto>> GetParticipantQuestionsAsync()
    {
        var questions = await _context.Questions
			.AsNoTracking()
			.Include(q => q.Answers)
            .ToListAsync();

        return questions.Select(q => new ParticipantQuestionDto
        {
            Id = q.Id,
            Title = q.Title,
            Answers = q.Answers.Select(a => new ParticipantAnswerOptionDto
            {
                Id = a.Id,
                Title = a.Title
            }).ToList()
        }).ToList();
    }

    public async Task<QuizResult> SubmitQuizAsync(SubmitQuizDto dto)
    {
        int rightAnswers = 0;
        int wrongAnswers = 0;
        Dictionary<int,int> answerIdsGrouped = new();

		try
		{
            answerIdsGrouped = dto.Answers.ToDictionary(a => a.QuestionId, a => a.AnswerId);
        }
        catch
        {
        }

        var questions = await _context.Questions
            .Include(q => q.Answers)
            .ToListAsync();

        foreach (var q in questions)
        {
            if (answerIdsGrouped.TryGetValue(q.Id, out var submittedAnswerId))
            {
                var isRight = q.Answers.FirstOrDefault(a => a.Id == submittedAnswerId)?.IsRight ?? false;
                if (isRight)
                {
                    rightAnswers++;
                }
                else
                {
                    wrongAnswers++;
                }
            }
            else
            {
                wrongAnswers++;
            }
        }

        int points = (rightAnswers * 100) + (dto.TotalRemainingSeconds * 3);

        var result = new QuizResult
        {
            Name = dto.Name,
            RightAnswersCount = rightAnswers,
            WrongAnswersCount = wrongAnswers,
            TotalRemainingSeconds = dto.TotalRemainingSeconds,
            Points = points,
            CreationTime = DateTime.UtcNow
        };

        _context.QuizResults.Add(result);
        await _context.SaveChangesAsync();

        return result;
    }

    public async Task<List<QuizResult>> GetLeaderboardAsync()
    {
        return await _context.QuizResults
			.AsNoTracking()
			.OrderByDescending(r => r.Points)
            .ToListAsync();
    }

    public async Task<bool> CheckActivityAsync()
    {
        var config = await _context.AppConfigs.AsNoTracking().FirstOrDefaultAsync(c => c.Name == "ActivityStatus");
        if (config == null) return true;

        return config.Value;
    }

    public async Task<bool> ToggleActivityAsync()
    {
        var config = await _context.AppConfigs.FirstOrDefaultAsync(c => c.Name == "ActivityStatus");
        
        if (config == null)
        {
            config = new AppConfig { Name = "ActivityStatus", Value = true };
            _context.AppConfigs.Add(config);
        }

        config.Value = !config.Value;
        await _context.SaveChangesAsync();

        return config.Value;
    }
}
