using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.Endity;
using Escola_ExercicioBancoDeDados.Repository;
using System;

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

        public Professor UpdateProfessor(ProfessorDTO professorDTO, Guid id)
        {
            var professor = new Professor(nome: professorDTO.Nome, idade: professorDTO.Idade, id: id);
            var n = _repository.Update(professor);
            if (n == 0) throw new Exception("Não foi possível encontrar o professor");
            return professor;
        }
        public void Delete(Guid id)
        {
            var n = _repository.Delete(id);
            if (n == 0) throw new Exception("Não foi possível encontrar a matéria");
        }
    }
}
