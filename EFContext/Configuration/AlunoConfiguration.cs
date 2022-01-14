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
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos");
            builder.Property(p => p.Nome)
                .HasColumnType("varchar2(60)")
                .IsRequired();

            builder.Property(p => p.Idade)
                .HasColumnType("smallint")
                .IsRequired();
            
            builder.HasOne(p => p.Turma)
                .WithMany(b => b.Alunos)
                .HasForeignKey("turma_id");

            builder.Property(p => p.Id).HasColumnType("varchar2(36)");
        }
    }
}
