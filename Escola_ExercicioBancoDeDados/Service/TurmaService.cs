using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.Endity;
using Escola_ExercicioBancoDeDados.Repository;

namespace Escola_ExercicioBancoDeDados.Service
{
    public class TurmaService
    {
        private readonly TurmaRepository _repository;

        public TurmaService(TurmaRepository repository)
        {
            _repository = repository;
        }

        /*public Turma RegistraTurma(TurmaDTO turmaDTO)
        {

        }*/
    }
}
