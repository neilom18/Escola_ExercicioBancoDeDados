using Escola_ExercicioBancoDeDados.DTO;
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
        public AlunoService(AlunoRepository repository, TurmaRepository turmaRepository, CursoRepository cursoRepository)
        {
            _repository = repository;
            _turmaRepository = turmaRepository;
            _cursoRepository = cursoRepository;
        }

        public Aluno RegistraAluno(AlunoDTO alunoDTO)
        {
            var curso_id = _turmaRepository.GetCursoId(alunoDTO.Turma_id);
            var curso = _cursoRepository.SelectById(curso_id);
            var alunos = _turmaRepository.GetAlunos(curso_id);
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
            _repository.Insert(aluno);
            return aluno;
        }
    }
}
