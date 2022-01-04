namespace Escola_ExercicioBancoDeDados.DTO.QueryParameters
{
    public class CursoQuery
    {
        public string? Nome { get; set; }
        public string? Professor_Nome { get; set; }
        public int Page { get; set; } = 1;
        public int? Size { get; set; } = 20;
    }
}
