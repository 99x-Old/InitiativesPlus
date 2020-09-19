using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InitiativesPlus.Domain.Interfaces;
using InitiativesPlus.Domain.Models;
using InitiativesPlus.Infrastructure.Data.Context;
using InitiativesPlus.Infrastructure.Data.ExternalServices;
using Microsoft.EntityFrameworkCore;

namespace InitiativesPlus.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private InitiativesPlusDbContext _context;
        public UserRepository(InitiativesPlusDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AssignRoleAsync(string username, int role)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            user.RoleId = role;
            _context.Entry(user).State = EntityState.Modified;

            bool success = await _context.SaveChangesAsync() > 0;
            if (success)
            {
                await MessageProducer.SendMessageAsync("role-change", "inbox.nishan@gmail.com",$"Hello {username}, your role has been changed");
            }
            return success;
        }
    }
}
