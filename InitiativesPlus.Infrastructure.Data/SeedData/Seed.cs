using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InitiativesPlus.Domain.Models;
using InitiativesPlus.Infrastructure.Data.Context;
using Newtonsoft.Json;

namespace InitiativesPlus.Infrastructure.Data.SeedData
{
    public class Seed
    {
        private readonly InitiativesPlusDbContext _context;

        public Seed(InitiativesPlusDbContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
            var userData = System.IO.File.ReadAllText("../InitiativesPlus.Infrastructure.Data/SeedData/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            var initiativeData = System.IO.File.ReadAllText("../InitiativesPlus.Infrastructure.Data/SeedData/InitiativeSeedData.json");
            var initiatives = JsonConvert.DeserializeObject<List<Initiative>>(initiativeData);

            var userRecordsInDb = users.Where(x => _context.Users.Any(z => z.Username == x.Username)).ToList();
            var initiativeRecordsInDb = initiatives.Where(x => !_context.Initiatives.Any(z => z.Name == x.Name)).ToList();

            if (userRecordsInDb == null)
            {
                //Seed users

                foreach (var user in users)
                {
                    //Create password
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    user.Username = user.Username.ToLower();

                    _context.Users.Add(user);
                }
            }

            if (initiativeRecordsInDb == null)
            {
                //Seed Initiatives

                foreach (var initiative in initiatives)
                {
                    _context.Initiatives.Add(initiative);
                }
                _context.SaveChanges();
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
