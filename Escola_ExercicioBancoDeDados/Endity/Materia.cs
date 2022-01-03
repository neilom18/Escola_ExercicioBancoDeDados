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

        public Materia(string nome, string descricao, Professor professor)
        {
            Nome = nome;
            Descricao = descricao;
            Professor = professor;
        }

        public Materia(string nome, string descricao, Professor professor, Guid id) : base(id)
        {
            Nome = nome;
            Descricao = descricao;
            Professor = professor;
        }

        public string Nome { get; private set; }
        public string Descricao { get;private set; }
        public Professor Professor { get; private set; }

        public void SetProfessor(Professor professor) => Professor = professor;
    }
}
