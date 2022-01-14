using System;
using System.Collections.Generic;

namespace Dominio.Endity
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
        public string Nome { get; private set; }
        public int Idade { get; private set; }
    }
}
