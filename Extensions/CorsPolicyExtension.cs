
namespace VivaFestAPI.Extensions;

public static class CorsPolicyExtension
{
    public static IServiceCollection AddAllowedSpecificOrigins(this IServiceCollection services)
    {
        services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});


        return services;
    }
}
