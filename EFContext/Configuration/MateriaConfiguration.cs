using Dominio.Endity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFContext
{
    public class MateriaConfiguration : IEntityTypeConfiguration<Materia>
    {
        public void Configure(EntityTypeBuilder<Materia> builder)
        {
            builder.ToTable("Materias");

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(120);

            builder.HasOne(p => p.Professor)
                .WithMany()
                .HasForeignKey("professor_id");

            builder.Property(p => p.Descricao)
                .HasMaxLength(160);

            builder.Property(p => p.Id).HasColumnType("varchar2(36)");

        }
    }
}
