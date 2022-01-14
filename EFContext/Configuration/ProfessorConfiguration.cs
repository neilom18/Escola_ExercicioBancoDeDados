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
    public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.ToTable("Professores");

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(p => p.Idade)
                .HasColumnType("smallint")
                .IsRequired();

            builder.Property(p => p.Id).HasColumnType("varchar2(36)");

        }
    }
}
