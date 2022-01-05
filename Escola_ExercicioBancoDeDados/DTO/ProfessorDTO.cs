using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.DTO
{
    public class ProfessorDTO : Validator
    {
        public string Nome { get; set; }
        public int Idade { get; set; }

        public override List<KeyValuePair<string, string>> GetErrors()
        {
            return _errors;
        }

        public override void Validar()
        {
            Valido = true;
            if (string.IsNullOrWhiteSpace(Nome))
            {
                Valido = false;
                _errors.Add(new KeyValuePair<string, string>(nameof(Nome), "O nome do professor precisa ser informado"));
            }
            if(Idade < 0)
            {
                Valido = false;
                _errors.Add(new KeyValuePair<string, string>(nameof(Idade), "A idade não pode ser negativa"));
            }
        }
    }
}
