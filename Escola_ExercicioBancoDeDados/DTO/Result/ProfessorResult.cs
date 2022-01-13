using Dominio.Endity;
using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class ProfessorRuslt : Base 
    {
        public ProfessorRuslt(Guid id) : base(id){}
        public ProfessorRuslt(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
        }

        public ProfessorRuslt(string nome, int idade, Guid id) : base(id)
        {
            Nome = nome;
            Idade = idade;
        }
        public string Nome { get; private set; }
        public int Idade { get; private set; }
    }
}
