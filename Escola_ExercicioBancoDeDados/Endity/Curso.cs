using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class Curso : Base
    {
        public Curso(string nome, Guid id) : base(id)
        {
            Nome = nome;
        }

        public Curso(List<Materia> materias, string nome)
        {
            Materias = materias;
            Nome = nome;
        }

        public Curso(List<Materia> materias, string nome, Guid id) : base(id)
        {
            Materias = materias;
            Nome = nome;
        }

        public List<Materia> Materias { get; private set; }
        public string Nome { get; private set; }

        public void SetMaterias(List<Materia> materias) => Materias = materias;
    }
}
