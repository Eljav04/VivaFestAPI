using Microsoft.EntityFrameworkCore;
using VivaFestAPI.Data;

namespace VivaFestAPI.Utility
{
	public class DataHelper
	{
		public static async Task ManageDataAsync(IServiceProvider srcProvider)
		{
			var dbContextSvc = srcProvider.GetRequiredService<AppDbContext>();

			//Migration: This is the programmatic equivalent to Update-Database
			await dbContextSvc.Database.MigrateAsync();
	}

}
}
