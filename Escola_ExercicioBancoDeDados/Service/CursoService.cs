using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.Endity;
using Escola_ExercicioBancoDeDados.Repository;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Service
{
    public class CursoService
    {
        private readonly CursoRepository _repository;
        private readonly MateriaCursoRepository _materiaCursoRepository;
        private readonly MateriaRepository _materiaRepository;

        public CursoService(CursoRepository repository,
            MateriaCursoRepository materiaCursoRepository,
            MateriaRepository materiaRepository)
        {
            _repository = repository;
            _materiaCursoRepository = materiaCursoRepository;
            _materiaRepository = materiaRepository;
        }

        public Curso RegistraCurso(CursoDTO cursoDTO)
        {
            // Busca das matérias
            List<Materia> materias = new List<Materia>();
            foreach(var materia_id in cursoDTO.Materias_id)
            {
                var materia = _materiaRepository.SelectById(materia_id);
                materias.Add(materia);
            }
            if (materias.Count != cursoDTO.Materias_id.Count) throw new System.Exception("Alguma matéria não foi encontrada");
            //
            var curso = new Curso(materias: materias, nome: cursoDTO.Nome);
            _materiaCursoRepository.Insert(curso);
            return _repository.Insert(curso);
        }
    }
}
