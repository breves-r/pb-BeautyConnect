using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Entities;

namespace RedeSocial.Infra.Context.Mapping
{
    public class PostMapping : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Descricao)
                   .IsRequired().HasMaxLength(500);
            builder.Property(x => x.CreatedDate);
            builder.Property(x => x.Imagem);
            builder.Property(x => x.Produto);
            builder.Property(x => x.Categoria);
        }
    }
}
