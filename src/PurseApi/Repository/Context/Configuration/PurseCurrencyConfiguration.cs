using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurseApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Repository.Context.Configuration
{
    public class PurseCurrencyConfiguration : IEntityTypeConfiguration<PurseCurrency>
    {
        public void Configure(EntityTypeBuilder<PurseCurrency> builder)
        {
            builder.HasKey(c => new { c.PurseId, c.Currency });
        }
    }
}
