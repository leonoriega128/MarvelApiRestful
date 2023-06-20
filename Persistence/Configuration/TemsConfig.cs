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
    public class TemsConfig
    {
        public void Configure(EntityTypeBuilder<TeamsRescue> builder)
        {
            builder.ToTable("Teams");
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Name)
                .HasMaxLength(80)
                .IsRequired();
        }
    }
}
