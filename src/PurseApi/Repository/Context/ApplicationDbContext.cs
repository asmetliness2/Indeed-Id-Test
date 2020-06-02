using Microsoft.EntityFrameworkCore;
using PurseApi.Models;
using PurseApi.Repository.Context.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Repository.Context
{
    public class ApplicationDbContext: DbContext
    {

        public DbSet<Purse> Purses { get; set; }

        public DbSet<PurseCurrency> PurseCurrencies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyAllConfigurations();
        }

    }
}
