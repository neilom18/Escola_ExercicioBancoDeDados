using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.DTO
{
    public class AlunoDTO : Validator
    {
        public Guid Turma_id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }

        public override Dictionary<string, string> GetErrors()
        {
            return _errors;
        }

        public override void Validar()
        {
            Valido = true;
            if(Idade <= 0)
            {
                Valido = false;
                _errors.Add(nameof(Idade), "Idade não pode ser nula ou negativa");
            }
            if (string.IsNullOrWhiteSpace(Nome))
            {
                Valido = false;
                _errors.Add(nameof(Nome), "O nome precisa ser informado!");
            }
            if(Turma_id == Guid.Empty)
            {
                Valido = false;
                _errors.Add(nameof(Turma_id), "A turma precisa ser informada");
            }
        }
    }
}
