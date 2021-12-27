using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.Endity;
using Escola_ExercicioBancoDeDados.Repository;

namespace Escola_ExercicioBancoDeDados.Service
{
    public class MateriaService
    {
        public readonly MateriaRepository _repository;
        public MateriaService(MateriaRepository repository)
        {
            _repository = repository;
        }

        public Materia RegistraMateria(MateriaDTO materiaDTO)
        {
            
            var materia = new Materia(materiaDTO.Nome, materiaDTO.Descricao);
            return _repository.Insert(materia);
        }
    }
}
