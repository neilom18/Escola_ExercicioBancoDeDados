using System;
using System.Collections.Generic;

namespace Dominio.Endity
{
    public class Turma : Base
    {
        public Turma(){}
        public Turma(Curso curso, Guid id) : base(id)
        {
            Curso = curso;
            Alunos = new List<Aluno>();
        }
        public Turma(Curso curso, List<Aluno> alunos, Guid id): base (id)
        {
            Curso = curso;
            Alunos = alunos;
        }

        public Turma(Curso curso)
        {
            Curso = curso;
            Alunos = new List<Aluno>();
        }

        public Curso Curso { get; private set; }
        public List<Aluno> Alunos { get; private set; }
    }
}
