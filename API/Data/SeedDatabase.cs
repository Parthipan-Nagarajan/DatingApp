using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Data
{
    public class SeedDatabase
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.AppUsers.AnyAsync()) return;
            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();               

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Passw0rd"));
                user.PasswordSalt = hmac.Key;

                context.AppUsers.Add(user);
            }

            await context.SaveChangesAsync();

        }
    }
}