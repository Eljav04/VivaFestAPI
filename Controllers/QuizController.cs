using Microsoft.AspNetCore.Mvc;
using VivaFestAPI.DTOs;
using VivaFestAPI.Interfaces;

namespace VivaFestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    // ================= ADMIN ENDPOINTS =================

    [HttpGet("admin/questions")]
    public async Task<IActionResult> GetAllQuestions()
    {
        var result = await _quizService.GetAllQuestionsAsync();
        return Ok(result);
    }

    [HttpPost("admin/questions")]
    public async Task<IActionResult> CreateQuestion([FromBody] QuestionDto dto)
    {
        var result = await _quizService.CreateQuestionAsync(dto);
        return Ok(result);
    }

    [HttpPut("admin/questions/{id}")]
    public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionDto dto)
    {
        var result = await _quizService.UpdateQuestionAsync(id, dto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("admin/questions/{id}")]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var success = await _quizService.DeleteQuestionAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("admin/results/reset")]
    public async Task<IActionResult> ResetLeaderboard()
    {
        await _quizService.ResetLeaderboardAsync();
        return NoContent();
    }

    [HttpPost("admin/quiz/toggle_activity")]
    public async Task<IActionResult> ToggleActivity()
    {
        var result = await _quizService.ToggleActivityAsync();
        return Ok(result);
    }

    // ================= PARTICIPANT ENDPOINTS =================

    [HttpGet("questions")]
    public async Task<IActionResult> GetParticipantQuestions()
    {
        var result = await _quizService.GetParticipantQuestionsAsync();
        return Ok(result);
    }

    [HttpPost("submit")]
    public async Task<IActionResult> SubmitQuiz([FromBody] SubmitQuizDto dto)
    {
        var result = await _quizService.SubmitQuizAsync(dto);
        return Ok(result);
    }


    // ================= GLOBAL ENDPOINTS =================

    [HttpGet("leaderboard")]
    public async Task<IActionResult> GetLeaderboard()
    {
        var result = await _quizService.GetLeaderboardAsync();
        return Ok(result);
    }

    [HttpGet("check_activity")]
    public async Task<IActionResult> CheckActivity()
    {
        var result = await _quizService.CheckActivityAsync();
        return Ok(result);
    }
}
