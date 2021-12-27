using System;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class Professor : Base 
    {
        public Turma Turma { get; private set; }
        public Curso Curso { get; private set; }
        public Materia Materia { get; set; }
        public string Nome { get; private set; }
        public int Idade { get; private set; }
    }
}
