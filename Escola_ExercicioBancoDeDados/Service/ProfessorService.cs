using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.Endity;
using Escola_ExercicioBancoDeDados.Repository;

namespace Escola_ExercicioBancoDeDados.Service
{
    public class ProfessorService
    {
        private readonly ProfessorRepository _repository;

        public ProfessorService(ProfessorRepository repository)
        {
            _repository = repository;
        }

        public Professor RegistraProfessor(ProfessorDTO professorDTO) 
        {
            var professor = new Professor(nome: professorDTO.Nome, idade: professorDTO.Idade);
            _repository.Insert(professor);
            return professor;
        }
    }
}
