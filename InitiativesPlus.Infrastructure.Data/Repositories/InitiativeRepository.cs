using InitiativesPlus.Domain.Interfaces;
using InitiativesPlus.Domain.Models;
using InitiativesPlus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InitiativeStatus = InitiativesPlus.Infrastructure.Data.StaticClasses.InitiativeStatus;

namespace InitiativesPlus.Infrastructure.Data.Repositories
{
    public class InitiativeRepository : IInitiativeRepository
    {
        private InitiativesPlusDbContext _context;
        public InitiativeRepository(InitiativesPlusDbContext context)
        {
            _context = context;
        }
        public async Task<List<Initiative>> GetInitiativesAsync(int? userId)
        {
            if (userId != null)
            {
                List<int> initiativeIds = new List<int>();
                var myInitiatives = await _context.UserInitiatives.Where(x => x.UserId == userId).ToListAsync();
                if (myInitiatives != null)
                {
                    foreach (var userInitiative in myInitiatives)
                    {
                        initiativeIds.Add(userInitiative.InitiativeId);
                    }
                }

                return await _context.Initiatives.Where(i => initiativeIds.Contains(i.Id)).ToListAsync();
            }
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

        public async Task<bool> InitiativeExistsAsync(int id)
        {
            if (await _context.Initiatives.AnyAsync(x => x.Id == id))
                return true;
            return false;
        }        
        
        public async Task<bool> RemoveInitiativeAsync(int id)
        {
            var initiative = await _context.Initiatives.FirstOrDefaultAsync(x => x.Id == id);
            initiative.StatusId = (int)InitiativeStatus.Inactive;

            _context.Entry(initiative).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        } 
        
        public async Task<bool> UserExistsInInitiativeAsync(int id, int userId)
        {
            var initiativeUser = await _context.UserInitiatives.FirstOrDefaultAsync(x => x.InitiativeId == id && x.UserId == userId);
            return initiativeUser != null;
        }
        
        public async Task<bool> RemoveUserFromInitiativeAsync(int id, int userId)
        {
            var initiativeUser = await _context.UserInitiatives.FirstOrDefaultAsync(x => x.InitiativeId == id && x.UserId == userId);

            _context.UserInitiatives.Remove(initiativeUser);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
