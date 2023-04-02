using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Infra.Context.Mapping
{
    internal class ProfileMapping : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(x => x.IdProfile);
            builder.Property(x => x.Nome)
                   .IsRequired().HasMaxLength(100);
            builder.Property(x => x.Foto);

            builder.HasMany(x => x.Posts).WithOne(x => x.Profile).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
