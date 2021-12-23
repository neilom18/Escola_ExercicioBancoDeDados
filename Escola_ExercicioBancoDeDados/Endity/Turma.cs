using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class Turma : Base
    {
        public Guid Curso_id { get; private set; }
        public List<Aluno> Alunos { get; private set; }
        public int NumeroAlunos { get; private set; }
    }
}
