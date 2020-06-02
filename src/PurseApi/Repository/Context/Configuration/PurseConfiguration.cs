using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurseApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Repository.Context.Configuration
{
    public class PurseConfiguration : IEntityTypeConfiguration<Purse>
    {
        public void Configure(EntityTypeBuilder<Purse> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasMany(b => b.Currencies)
                .WithOne()
                .HasForeignKey(c => c.PurseId);
        }
    }
}
