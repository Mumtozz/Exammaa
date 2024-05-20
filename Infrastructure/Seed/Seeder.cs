using System.Security.Cryptography;
using System.Text;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed;

public class Seeder(DataContext context)
{

    public async Task SeedUser()
    {
        var existing = await context.Users.FirstOrDefaultAsync(x => x.Name == "Maruf" && x.HashPassword == ConvertToHash("2211"));
        if (existing != null)
        {
            return;
        }

        var user = new User()
        {
            Id = 1,
            Name = "Maruf",
            DateOfRegister = DateTime.UtcNow,
            HashPassword = ConvertToHash("2211"),
        };
        user.Email = $"{user.Name}@gmail.com";

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }


    private static string ConvertToHash(string rawData)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }


}
