using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Delta.Web.Api
{
    static class Seed
    {
        public static async Task SeedUsers(DataContext? context)
        {
            if (context == null || context.Users == null) return;
            if (await context.Users.AnyAsync()) return;
            var userData = await File.ReadAllTextAsync("Data/Users.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach (var user in users!)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user!.UserName!.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                user.PasswordSalt = hmac.Key;
                context.Users.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }
}
