using Dominio.Endity;
using Escola_ExercicioBancoDeDados.Endity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFContext
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos");
            builder.Property(p => p.Nome)
                .HasMaxLength(60)
                .IsRequired(true);
            builder.Property(p => p.Idade)
                .HasColumnType("UInt16")
                .IsRequired(true);
        }
    }
}
