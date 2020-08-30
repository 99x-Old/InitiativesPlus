using InitiativesPlus.Domain.Interfaces;
using InitiativesPlus.Domain.Models;
using InitiativesPlus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InitiativesPlus.Infrastructure.Data.Repositories
{
    public class InitiativeRepository : IInitiativeRepository
    {
        public InitiativesPlusDbContext _context;
        public InitiativeRepository(InitiativesPlusDbContext context)
        {
            _context = context;
        }
        public async Task<List<Initiative>> GetInitiatives()
        {
            return await _context.Initiatives.ToListAsync();
        }
    }
}
