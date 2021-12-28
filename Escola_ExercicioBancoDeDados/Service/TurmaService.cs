using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.Endity;
using Escola_ExercicioBancoDeDados.Repository;

namespace Escola_ExercicioBancoDeDados.Service
{
    public class TurmaService
    {
        private readonly TurmaRepository _repository;
        private readonly CursoRepository _cursoRepository;

        public TurmaService(TurmaRepository repository, CursoRepository cursoRepository)
        {
            _repository = repository;
            _cursoRepository = cursoRepository;
        }

        public Turma RegistraTurma(TurmaDTO turmaDTO)
        {
            var curso = _cursoRepository.SelectById(turmaDTO.Curso_id);
            return _repository.Insert(new Turma(curso: curso));
        }
    }
}
