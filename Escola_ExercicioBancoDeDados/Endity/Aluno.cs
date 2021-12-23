using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class Aluno : Base
    {
        public Aluno(Guid turma_id, List<Materia> materias, string nome, int idade)
        {
            Turma_id = turma_id;
            Materias = materias;
            Nome = nome;
            Idade = idade;
        }

        public Guid Turma_id { get;private set; }
        public List<Materia> Materias { get;private set; }
        public string Nome { get;private set; }
        public int Idade { get;private set; }
    }
}
