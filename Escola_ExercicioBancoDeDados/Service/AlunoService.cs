using Dominio;
using Dominio.Endity;
using Dominio.IRepository.Dapper;
using Dominio.IRepository.EF;
using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.DTO.QueryParametes;
using Escola_ExercicioBancoDeDados.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Escola_ExercicioBancoDeDados.Service
{
    public class AlunoService
    {
        private readonly IAlunoRepositoryEF _alunoRepositoryEF;
        private readonly IAlunoRepositoryDapper _alunoRepositoryDapper;
        private readonly ITurmaRepositoryDapper _turmaRepositoryDapper;
        private readonly ICursoRepositoryDapper _cursoRepositoryDapper;
        private readonly IUnityOfWork _unityOfWork;

        public AlunoService(IAlunoRepositoryDapper alunoRepositoryDapper, IUnityOfWork unityOfWork, IAlunoRepositoryEF alunoRepositoryEF, ITurmaRepositoryDapper turmaRepositoryDapper, ICursoRepositoryDapper cursoRepositoryDapper)
        {
            _alunoRepositoryDapper = alunoRepositoryDapper;
            _unityOfWork = unityOfWork;
            _alunoRepositoryEF = alunoRepositoryEF;
            _turmaRepositoryDapper = turmaRepositoryDapper;
            _cursoRepositoryDapper = cursoRepositoryDapper;
        }

        public Aluno RegistraAluno(AlunoDTO alunoDTO)
        {
            var curso_id = _turmaRepositoryDapper.GetCursoId(alunoDTO.Turma_id);
            if (curso_id == Guid.Empty)
                throw new Exception("O curso buscado não foi encontrado");
            var curso = _cursoRepositoryDapper.Get(curso_id);
            var alunos = _turmaRepositoryDapper.GetAlunos(alunoDTO.Turma_id);
            Turma turma;
            if(alunos.Any())
            {
                turma = new Turma(curso: curso, id: alunoDTO.Turma_id, alunos: alunos.ToList());
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
        /*
        public Aluno UpdateAluno(AlunoDTO alunoDTO, Guid id)
        {
            var curso_id = _turmaRepository.GetCursoId(alunoDTO.Turma_id);
            if (curso_id == Guid.Empty)
                throw new Exception("O curso buscado não foi encontrado");
            var curso = _cursoRepository.SelectById(curso_id);
            var alunos = _turmaRepository.GetAlunos(alunoDTO.Turma_id);
            Turma turma;
            if (alunos.Any())
            {
                turma = new Turma(curso: curso, id: alunoDTO.Turma_id, alunos: alunos);
            }
            else
            {
                turma = new Turma(curso: curso, id: alunoDTO.Turma_id);
            }
            var aluno = new Aluno
                (
                nome: alunoDTO.Nome,
                idade: alunoDTO.Idade,
                turma: turma,
                id: id
                );
            aluno.Turma.Alunos.Add(new Aluno(aluno.Nome, aluno.Idade, aluno.Id));
            var n =_repository.Update(aluno);
            if (n == 0) throw new Exception("Não foi possível encontrar o aluno");
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
        }*/

        public IEnumerable<Aluno> GetAlunos(AlunoQuery alunoQuery)
        {
            var alunos = _alunoRepositoryDapper.GetAllByParameters(alunoQuery);
            foreach(var aluno in alunos)
            {
                var m = _alunoRepositoryDapper.GetMateriasFromAluno(aluno.Id);
                aluno.SetMaterias(m);
            }
            return alunos;
        }

        /*public void Delete(Guid id)
        {
            var result = _repository.Delete(id);
            if (result == 0) throw new Exception("Não foi encontrado o aluno");
        }*/
    }
}
