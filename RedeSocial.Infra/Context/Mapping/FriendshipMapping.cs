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
    public class FriendshipMapping : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(x => new {x.IdProfileA, x.IdProfileB});
            builder.HasOne(fr => fr.ProfileA)
                .WithMany(f => f.FriendshipsB)
                .HasForeignKey(f => f.IdProfileA)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(fr => fr.ProfileB)
                .WithMany(f => f.FriendshipsA)
                .HasForeignKey(f => f.IdProfileB)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
