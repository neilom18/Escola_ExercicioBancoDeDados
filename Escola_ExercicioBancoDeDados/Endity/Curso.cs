using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class Curso : Base
    {
        public Curso(List<Materia> materias, string nome)
        {
            Materias = materias;
            Nome = nome;
        }

        public List<Materia> Materias { get; private set; }
        public string Nome { get; private set; }
    }
}
