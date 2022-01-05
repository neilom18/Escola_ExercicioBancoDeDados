using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.DTO
{
    public abstract class Validator
    {
        protected readonly List<KeyValuePair<string, string>> _errors;
        public Validator()
        {
            _errors = new List<KeyValuePair<string, string>>();
        }
        public bool Valido { get; protected set; }
        public abstract void Validar();

        public abstract List<KeyValuePair<string, string>> GetErrors();
    }
}
