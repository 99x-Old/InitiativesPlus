using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InitiativesPlus.Domain.Interfaces;
using InitiativesPlus.Domain.Models;
using InitiativesPlus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InitiativesPlus.Infrastructure.Data.Repositories
{
    public class ActionRepository : IActionRepository
    {
        private readonly InitiativesPlusDbContext _context;
        public ActionRepository(InitiativesPlusDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateActionAsync(InitiativeAction initiativeAction)
        {
            await _context.InitiativeActions.AddAsync(initiativeAction);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<InitiativeAction>> GetActionsAsync(int id)
        {
            return await _context.InitiativeActions.Where(x => x.InitiativeId == id).ToListAsync();
        }

        public async Task<bool> UpdateActionAsync(InitiativeAction initiativeAction)
        {
            var actionInDb = await _context.InitiativeActions.FindAsync(initiativeAction.Id);
            if (actionInDb == null)
                return false;

            actionInDb.Progress = initiativeAction.Progress;

            _context.Entry(actionInDb).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
