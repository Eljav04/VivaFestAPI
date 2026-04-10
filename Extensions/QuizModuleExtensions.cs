using Microsoft.EntityFrameworkCore;
using VivaFestAPI.Data;
using VivaFestAPI.Interfaces;
using VivaFestAPI.Services;

namespace VITRACK.Api.Extensions;

public static class QuizModuleExtensions
{
    public static IServiceCollection AddQuizModule(this IServiceCollection services, IConfiguration configuration)
    {
        var dbPath = Path.Join(Directory.GetCurrentDirectory(), "quiz.db");
        services.AddDbContext<QuizDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        services.AddScoped<IQuizService, QuizService>();

        return services;
    }
}
