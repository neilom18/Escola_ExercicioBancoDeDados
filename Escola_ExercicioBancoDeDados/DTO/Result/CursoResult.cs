using Dominio.Endity;
using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class CursoResult : Base
    {
        public CursoResult(string nome, Guid id) : base(id)
        {
            Nome = nome;
        }

        public CursoResult(List<Materia> materias, string nome)
        {
            Materias = materias;
            Nome = nome;
        }

        public CursoResult(List<Materia> materias, string nome, Guid id) : base(id)
        {
            Materias = materias;
            Nome = nome;
        }

        public List<Materia> Materias { get; private set; }
        public string Nome { get; private set; }

        public void SetMaterias(List<Materia> materias) => Materias = materias;
    }
}
