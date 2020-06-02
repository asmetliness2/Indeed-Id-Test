using Microsoft.EntityFrameworkCore;
using PurseApi.Models;
using PurseApi.Repository.Context;
using PurseApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Repository
{
    public class PurseRepository : IPurseRepository
    {

        private readonly ApplicationDbContext _context;

        public PurseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Purse> GetOrCreatePurse(int userId)
        {
            var result = await _context.Purses
                .Include(p => p.Currencies)
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if(result == null)
            {
                result = new Purse(userId);

                await _context.Purses.AddAsync(result);

                await _context.SaveChangesAsync();
            }

            return result;
        }
    }
}
