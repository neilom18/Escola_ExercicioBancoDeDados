using System;
using System.Collections.Generic;

namespace Dominio.Endity
{
    public class Turma : Base
    {
        public Guid Curso_id { get; set; }
        public Turma(Guid curso_id) : base()
        {
            Curso_id = curso_id;
        }
        public Turma(Guid curso_id, Guid id) : base(id)
        {
            Curso_id = curso_id;
        }
    }
}
