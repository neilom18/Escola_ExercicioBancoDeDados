using System;
using System.Collections.Generic;

namespace Dominio.Endity
{
    public class Aluno : Base
    {
        public Aluno(string nome, int idade, Guid id) : base(id)
        {
            Nome = nome;
            Idade = idade;
        }
        public Aluno(string nome, int idade, Guid turma_id, Guid id) : base(id)
        {
            Nome = nome;
            Idade = idade;
            Turma_id = turma_id;
        }
        public string Nome { get;private set; }
        public int Idade { get;private set; }
        public Guid Turma_id { get; private set; }

    }
}
