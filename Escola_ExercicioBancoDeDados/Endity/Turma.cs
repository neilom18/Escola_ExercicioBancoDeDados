using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class Turma : Base
    {
        public Turma(Curso curso, Professor professor)
        {
            Curso = curso;
            Alunos = new List<Aluno>();
        }

        public Curso Curso { get; private set; }
        public List<Aluno> Alunos { get; private set; }
    }
}
