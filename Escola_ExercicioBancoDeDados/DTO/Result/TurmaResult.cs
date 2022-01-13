using Dominio.Endity;
using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class TurmaResult : Base
    {
        public TurmaResult(Curso curso, Guid id) : base(id)
        {
            Curso = curso;
            Alunos = new List<Aluno>();
        }
        public TurmaResult(Curso curso, List<Aluno> alunos, Guid id): base (id)
        {
            Curso = curso;
            Alunos = alunos;
        }

        public TurmaResult(Curso curso)
        {
            Curso = curso;
            Alunos = new List<Aluno>();
        }

        public Curso Curso { get; private set; }
        public List<Aluno> Alunos { get; private set; }
    }
}
