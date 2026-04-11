using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VivaFestAPI.Data;
using VivaFestAPI.Entities;
using VivaFestAPI.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace VivaFestAPI.Controllers;

[ApiController]
[Route("api/fortune/lottery-items")]
public class LotteryController : ControllerBase
{
    private readonly AppDbContext _context;

    public LotteryController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/fortune/lottery-items
    [HttpGet]
    public async Task<IActionResult> GetAllParticipants()
    {
        var list = await _context.Participants
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToListAsync();
        
        return Ok(new { participants = list });
    }

    // POST /api/fortune/lottery-items
    [HttpPost]
    public async Task<IActionResult> CreateParticipant([FromBody] CreateParticipantDto dto)
    {
        if (dto == null)
            return BadRequest("Participant data is required.");

        var participant = new Participant
        {
            Name = dto.Name,
            Plate = dto.Plate,
            Phone = dto.Phone
        };

        _context.Participants.Add(participant);
        await _context.SaveChangesAsync();

        return Ok(participant);
    }

    // POST /api/fortune/lottery-items/bulk
    [HttpPost("bulk")]
    public async Task<IActionResult> CreateParticipantsBulk([FromBody] CreateParticipantDto[] dtos)
    {
        if (dtos == null || dtos.Length == 0)
            return BadRequest("Participant data array is required.");

        var participants = dtos.Select(dto => new Participant
        {
            Name = dto.Name,
            Plate = dto.Plate,
            Phone = dto.Phone
        }).ToList();

        _context.Participants.AddRange(participants);
        await _context.SaveChangesAsync();

        return Ok(new { message = $"{participants.Count} participants added." });
    }

    // DELETE /api/fortune/lottery-items/delete-all
    [HttpDelete("delete-all")]
    public async Task<IActionResult> DeleteAllParticipants()
    {
        await _context.Participants.ExecuteDeleteAsync();
        return Ok(new { message = "All participants removed." });
    }

    // DELETE /api/fortune/lottery-items/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParticipant(int id)
    {
        var participant = await _context.Participants.FindAsync(id);
        if (participant == null)
            return NotFound();

        _context.Participants.Remove(participant);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Participant removed." });
    }

    // POST /api/fortune/lottery-items/set-winner/{id}
    [HttpPost("set-winner/{id}")]
    public async Task<IActionResult> SetWinner(int id)
    {
        var participant = await _context.Participants.FindAsync(id);
        if (participant == null)
            return NotFound();

        participant.IsWinner = true;
        await _context.SaveChangesAsync();

        return Ok(participant);
    }

    // POST /api/fortune/lottery-items/reset
    [HttpPost("reset")]
    public async Task<IActionResult> ResetWinners()
    {
        await _context.Participants.ExecuteUpdateAsync(p => p.SetProperty(x => x.IsWinner, false));
        return Ok(new { message = "All winners reset." });
    }
}
