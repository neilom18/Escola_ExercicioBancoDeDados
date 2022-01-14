using System;
using Dominio;
using Dominio.Endity;
using Microsoft.EntityFrameworkCore;

namespace EFContext
{
    public class Context : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Turma> Turma { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Professor> Professor { get; set;}

        public Context(DbContextOptions<EFContext.Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFContext.Context).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
