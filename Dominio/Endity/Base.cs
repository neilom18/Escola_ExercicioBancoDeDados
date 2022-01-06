using System;

namespace Escola_ExercicioBancoDeDados.Endity
{
    public class Base
    {
        public Guid Id { get;private set; }
        public Base()
        {
            Id = Guid.NewGuid();
        }
        public Base(Guid id)
        {
            Id = id;
        }
    }
}
