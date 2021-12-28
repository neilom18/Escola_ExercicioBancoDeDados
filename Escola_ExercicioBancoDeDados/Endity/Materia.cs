using System;

namespace Escola_ExercicioBancoDeDados.Endity
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
        public string Nome { get; private set; }
        public string Descricao { get;private set; }

    }
}
