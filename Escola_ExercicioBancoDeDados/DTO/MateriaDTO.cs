using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.DTO
{
    public class MateriaDTO : Validator
    {

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Guid Professor_id { get; set; }
        public override Dictionary<string, string> GetErrors()
        {
            return _errors;
        }

        public override void Validar()
        {
            Valido = true;
        }
    }
}
