using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class TeamsHeroConfig
    {
        public void Configure(EntityTypeBuilder<Teams_Hero> builder)
        {
            builder.ToTable("Teamshero");
            builder.HasKey(c => c.Id);
            builder.Property(p => p.IdTeam)
            .HasConversion(
              v => v.ToString(),
              v => int.Parse(v)
             )
            .HasMaxLength(80);
            builder.Property(p => p.IdHero)
           .HasConversion(
             v => v.ToString(),
             v => int.Parse(v)
            )
           .HasMaxLength(80);
        }
    }
}
