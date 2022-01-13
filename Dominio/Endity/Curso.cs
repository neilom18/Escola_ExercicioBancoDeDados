using System;
using System.Collections.Generic;

namespace Dominio.Endity
{
    public class Curso : Base
    {
        public Curso(string nome, Guid id) : base(id)
        {
            Nome = nome;
        }
        public string Nome { get; private set; }

    }
}
