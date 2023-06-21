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
    public class CharacterConfig : IEntityTypeConfiguration<CharacterHero>
    {
        public void Configure(EntityTypeBuilder<CharacterHero> builder)
        {
            builder.ToTable("Character");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(256);
            builder.Property(x => x.UrlImage)
                .HasMaxLength(256); 
            builder.Property(x => x.ModifiedDate)
                .HasMaxLength(256);
            builder.Property(x => x.Agility)
                .IsRequired();
            builder.Property(x => x.Force)
                .IsRequired();
            builder.Property(x => x.Intelligence)
                .IsRequired();
            builder.Property(x => x.MarvelID)
                .IsRequired();

        }
    }
}
