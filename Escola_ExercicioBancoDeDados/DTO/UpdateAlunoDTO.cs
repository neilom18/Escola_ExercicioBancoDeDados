using System;
using System.Collections.Generic;
using System.Linq;

namespace Escola_ExercicioBancoDeDados.DTO
{
    public class UpdateAlunoDTO : Validator
    {
        public Guid Aluno_id { get; set; }
        public List<Guid> Materias_id { get; set; }

        public override List<KeyValuePair<string, string>> GetErrors()
        {
            return _errors;
        }

        public override void Validar()
        {
            Valido = true;
            if(Aluno_id == Guid.Empty)
            {
                Valido = false;
                _errors.Add(new KeyValuePair<string, string>(nameof(Aluno_id), "O aluno precisa ser informado"));
            }
            if(Materias_id.Count() > 3 || Materias_id.Count() < 1)
            {
                Valido = false;
                _errors.Add(new KeyValuePair<string, string>(nameof(Materias_id), "Deve ter entre uma a três matérias"));
            }
            foreach(var id in Materias_id)
            {
                if (id == Guid.Empty) 
                {
                    Valido = false;
                    _errors.Add(new KeyValuePair<string, string>(nameof(Materias_id), "A materia precisa ser informada no formato correto"));
                }
                if (Materias_id.FindAll(x => x == id).Count > 1)
                {
                    Valido = false;
                    _errors.Add(new KeyValuePair<string, string>(nameof(Materias_id), "Não pode ter matéiras repetidas"));
                }
            }
        }
    }
}
