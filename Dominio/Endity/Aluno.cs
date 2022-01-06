using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class Aluno : Base
    {
        public Aluno(string nome, int idade, Guid id) : base(id)
        {
            Nome = nome;
            Idade = idade;
            Materias = new List<Materia>();
        }

        public Aluno(Turma turma, string nome, int idade)
        {
            Turma = turma;
            Nome = nome;
            Idade = idade;
            Materias = new List<Materia>();
        }

        public Aluno(Turma turma, string nome, int idade, Guid id) : base(id)
        {
            Turma = turma;
            Nome = nome;
            Idade = idade;
            Materias = new List<Materia>();
        }

        public Aluno( Turma turma, List<Materia> materias, string nome, int idade)
        {
            Turma = turma;
            Materias = materias;
            Nome = nome;
            Idade = idade;
        }

        public Turma Turma { get;private set; } // Turma
        public List<Materia> Materias { get;private set; }
        public string Nome { get;private set; }
        public int Idade { get;private set; }

        public void SetMaterias(List<Materia> materias) => Materias = materias;
    }
}
