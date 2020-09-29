using InitiativesPlus.Domain.Interfaces;
using InitiativesPlus.Domain.Models;
using InitiativesPlus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InitiativesPlus.Infrastructure.Data.Repositories
{
    public class InitiativeRepository : IInitiativeRepository
    {
        private InitiativesPlusDbContext _context;
        public InitiativeRepository(InitiativesPlusDbContext context)
        {
            _context = context;
        }
        public async Task<List<Initiative>> GetInitiativesAsync()
        {
            return await _context.Initiatives.ToListAsync();
        }

        public async Task<Initiative> GetInitiativeAsync(int id)
        {
            return await _context.Initiatives.FirstOrDefaultAsync(x => x.Id == id);
        }        
        
        public async Task<bool> JoinInitiativeAsync(int id, int userId)
        {
            await _context.UserInitiatives.AddAsync(new UserInitiative
            {
                InitiativeId = id,
                UserId = userId
            });

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
