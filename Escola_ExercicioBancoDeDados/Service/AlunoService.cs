using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.DTO.QueryParametes;
using Escola_ExercicioBancoDeDados.Endity;
using Escola_ExercicioBancoDeDados.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Escola_ExercicioBancoDeDados.Service
{
    public class AlunoService
    {
        private readonly AlunoRepository _repository;
        private readonly TurmaRepository _turmaRepository;
        private readonly CursoRepository _cursoRepository;
        private readonly MateriaCursoRepository _materiaCursoRepository;
        private readonly AlunoMateriaRepository _alunoMateriaRepository;

        public AlunoService(AlunoRepository repository, TurmaRepository turmaRepository, CursoRepository cursoRepository, MateriaCursoRepository materiaCursoRepository, AlunoMateriaRepository alunoMateriaRepository)
        {
            _repository = repository;
            _turmaRepository = turmaRepository;
            _cursoRepository = cursoRepository;
            _materiaCursoRepository = materiaCursoRepository;
            _alunoMateriaRepository = alunoMateriaRepository;
        }

        public Aluno RegistraAluno(AlunoDTO alunoDTO)
        {
            var curso_id = _turmaRepository.GetCursoId(alunoDTO.Turma_id);
            if (curso_id == Guid.Empty)
                throw new Exception("O curso buscado não foi encontrado");
            var curso = _cursoRepository.SelectById(curso_id);
            var alunos = _turmaRepository.GetAlunos(alunoDTO.Turma_id);
            Turma turma;
            if(alunos.Any())
            {
                turma = new Turma(curso: curso, id: alunoDTO.Turma_id, alunos: alunos );
            }
            else
            {
                turma = new Turma(curso: curso, id:alunoDTO.Turma_id);
            }
            var aluno = new Aluno
                (
                nome: alunoDTO.Nome,
                idade: alunoDTO.Idade,
                turma: turma
                );
            aluno.Turma.Alunos.Add(new Aluno(aluno.Nome, aluno.Idade, aluno.Id));
            _repository.Insert(aluno);
            return aluno;
        }

        public Aluno UpdateMaterias(UpdateAlunoDTO updateAlunoDTO)
        {
            var aluno = _repository.SelectById(updateAlunoDTO.Aluno_id);
            List<Materia> materias = new List<Materia>();
            foreach (var id in updateAlunoDTO.Materias_id)
            {
                var m = _materiaCursoRepository.SelectById(curso_id: aluno.Turma.Curso.Id, materia_id: id);
                if (m is null) throw new Exception("Não foi possivel encontrar uma matéria!");
                materias.Add(m);
            }
            aluno.SetMaterias(materias);

            _alunoMateriaRepository.Insert(aluno);
            return aluno;
        }

        public IEnumerable<Aluno> GetAlunos(AlunoQuery alunoQuery)
        {
            var alunos = _repository.SelectByParam(alunoQuery);
            foreach(var aluno in alunos)
            {
                var m = _repository.GetMateriasFromAluno(aluno.Id);
                aluno.SetMaterias(m);
            }
            return alunos;
        }

        public void Delete(Guid id)
        {
            var result = _repository.Delete(id);
            if (result == 0) throw new Exception("Não foi encontrado o aluno");
        }
    }
}
