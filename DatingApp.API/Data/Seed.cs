using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext context)
        {
            if (!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordhash, passworSalt;
                    CreatePasswordHash("password", out passwordhash, out passworSalt);

                    user.PasswordHash = passwordhash;
                    user.PasswordSalt = passworSalt;
                    user.Username = user.Username.ToLower();
                    context.Users.Add(user);

                }

                context.SaveChanges();
            }

        }

          private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
            
        }


    }
}