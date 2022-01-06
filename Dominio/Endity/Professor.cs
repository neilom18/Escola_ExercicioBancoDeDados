using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class Professor : Base 
    {
        public Professor(Guid id) : base(id){}
        public Professor(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
        }

        public Professor(string nome, int idade, Guid id) : base(id)
        {
            Nome = nome;
            Idade = idade;
        }

        public List<Turma> Turma { get; private set; }
        public List<Materia> Materia { get; set; }
        public string Nome { get; private set; }
        public int Idade { get; private set; }
    }
}
