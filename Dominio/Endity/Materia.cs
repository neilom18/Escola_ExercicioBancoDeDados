using System;

namespace Dominio.Endity
{
    public class Materia : Base
    {
        public Materia(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }

        public Materia(string nome, string descricao, Guid id) : base(id)
        {
            Nome = nome;
            Descricao = descricao;
        }
        public Materia(string nome, string descricao, Guid professor_id, Guid id) : base(id)
        {
            Nome = nome;
            Descricao = descricao;
            Professor_id = professor_id;
        }

        public string Nome { get; private set; }
        public string Descricao { get;private set; }
        public Guid Professor_id { get; private set; }

    }
}
