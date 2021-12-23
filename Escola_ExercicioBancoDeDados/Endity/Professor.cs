using System;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class Professor : Base 
    {
        public Guid Turma_id { get; private set; }
        public string Nome { get; private set; }
        public int Idade { get; private set; }
    }
}
