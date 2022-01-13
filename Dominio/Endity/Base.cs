using System;

namespace Dominio.Endity
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
        public Base(string id)
        {
            Id = Guid.Parse(id);
        }
    }
}
