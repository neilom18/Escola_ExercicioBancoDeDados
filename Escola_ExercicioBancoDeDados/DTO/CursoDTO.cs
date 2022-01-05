using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.DTO
{
    public class CursoDTO : Validator
    {
        public List<Guid> Materias_id { get; set; }
        public string Nome { get; set; }

        public override List<KeyValuePair<string, string>>GetErrors()
        {
            return _errors;
        }

        public override void Validar()
        {
            Valido = true;
            if (string.IsNullOrWhiteSpace(Nome))
            {
                Valido = false;
                _errors.Add(new KeyValuePair<string, string>(nameof(Nome), "O nome precisa ser informado"));
            }
            if(Materias_id is not null)
            {
                foreach(var materia in Materias_id)
                {
                    if (materia == Guid.Empty)
                    {
                        Valido = false;
                        _errors.Add(new KeyValuePair<string, string>(nameof(Materias_id), "Todos os ids precisam ser válidos"));
                    }
                    if(materia.ToString().Length != 36) 
                    {
                        Valido = false;
                        _errors.Add(new KeyValuePair<string, string>(nameof(Materias_id), "Todos os ids precisam estar no formato GUID"));
                    }
                    if(Materias_id.FindAll(x => x == materia).Count > 1)
                    {
                        Valido = false;
                        _errors.Add(new KeyValuePair<string, string>(nameof(Materias_id), "Não pode ter matéiras repetidas"));
                    }
                }
            }
        }
    }
}
