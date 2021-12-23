using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class Curso : Base
    {
        public List<Materia> Materias { get; private set; }
        public string Nome { get; private set; }
    }
}
