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
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(120);
            builder.Property(p => p.Id).HasColumnType("varchar2(36)");

        }
    }
}
