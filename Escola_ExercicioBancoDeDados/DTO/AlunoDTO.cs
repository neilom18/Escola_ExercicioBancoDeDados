using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.DTO
{
    public class AlunoDTO : Validator
    {
        public Guid Professor_id { get; set; }
        public Guid Turma_id { get; set; }
        public List<Guid> Materias_id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }

        public override Dictionary<string, string> GetErrors()
        {
            return _errors;
        }

        public override void Validar()
        {
            if(Idade <= 0)
            {
                Valido = false;
                _errors.Add(nameof(Idade), "Idade não pode ser nula ou negativa");
            }
            if(Materias_id == null || Materias_id.Count > 3)
            {
                Valido = false;
                _errors.Add(nameof(Materias_id), "O Aluno precisa conter de 1 a 3 matérias");
            }
        }
    }
}
