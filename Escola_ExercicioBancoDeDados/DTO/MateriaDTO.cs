using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.DTO
{
    public class MateriaDTO : Validator
    {

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Guid Professor_id { get; set; }
        public override List<KeyValuePair<string, string>> GetErrors()
        {
            return _errors;
        }

        public override void Validar()
        {
            Valido = true;
            Nome = Nome.Trim();
            if(Nome.Length <= 1)
            {
                Valido = false;
                _errors.Add(new KeyValuePair<string, string>(nameof(Nome), "O nome precisa ter mais de 1 caracter"));
            }
            if(Descricao.Length > 120)
            {
                Valido = false;
                _errors.Add(new KeyValuePair<string, string>(nameof(Descricao), "A descrição não pode ter mais de 120 caracteres"));
            }
            if(Professor_id == Guid.Empty)
            {
                Valido = false;
                _errors.Add(new KeyValuePair<string, string>(nameof(Professor_id), "O id do professor precisa ser válido"));
            }
        }
    }
}
