using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class Aluno : Base
    {
        public Aluno( Turma turma_id, List<Materia> materias, string nome, int idade)
        {
            Turma_id = turma_id;
            Materias = materias;
            Nome = nome;
            Idade = idade;
        }

        public Turma Turma_id { get;private set; } // Turma
        public List<Materia> Materias { get;private set; }
        public string Nome { get;private set; }
        public int Idade { get;private set; }
    }
}
