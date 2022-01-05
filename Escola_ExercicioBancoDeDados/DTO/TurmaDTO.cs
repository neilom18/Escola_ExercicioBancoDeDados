using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.DTO
{
    public class TurmaDTO : Validator
    {
        public Guid Curso_id { get; set; }

        public override List<KeyValuePair<string, string>> GetErrors()
        {
            return _errors;
        }

        public override void Validar()
        {
            Valido = true;
            if(Curso_id == Guid.Empty)
            {
                Valido = false;
                _errors.Add(new KeyValuePair<string, string>(nameof(Curso_id), "O curso precisa ser informado"));
            }
        }
    }
}
