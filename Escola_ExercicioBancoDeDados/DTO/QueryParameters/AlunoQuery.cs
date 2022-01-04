using System;

namespace Escola_ExercicioBancoDeDados.DTO.QueryParametes
{
    public class AlunoQuery
    {
        public string? Nome { get; set; }
        public int? Idade { get; set; }
        public Guid? Curso_Id { get; set; }
        public Guid? Turma_Id { get; set; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
    }
}
