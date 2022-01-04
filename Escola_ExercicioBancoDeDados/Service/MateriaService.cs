using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.Endity;
using Escola_ExercicioBancoDeDados.Repository;
using System;

namespace Escola_ExercicioBancoDeDados.Service
{
    public class MateriaService
    {
        public readonly MateriaRepository _repository;
        public readonly ProfessorRepository _professorRepository;
        public MateriaService(MateriaRepository repository, ProfessorRepository professorRepository)
        {
            _repository = repository;
            _professorRepository = professorRepository;
        }

        public Materia RegistraMateria(MateriaDTO materiaDTO)
        {
            var professor = _professorRepository.SelectById(materiaDTO.Professor_id);
            Materia materia;
            if (professor is null)
                throw new System.Exception("Não foi possivel encontrar esse professor");
            else
                materia = new Materia(nome: materiaDTO.Nome, descricao: materiaDTO.Descricao, professor: professor);
                
            return _repository.Insert(materia);
        }

        public void Delete(Guid id)
        {
            var n = _repository.Delete(id);
            if (n == 0) throw new Exception("Não foi possível encontrar a matéria");
        }
    }
}
