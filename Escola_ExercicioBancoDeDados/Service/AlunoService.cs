using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.Endity;
using Escola_ExercicioBancoDeDados.Repository;
using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Service
{
    public class AlunoService
    {
        private readonly AlunoRepository _repository;
        private readonly MateriaRepository _materiaRepository;
        public AlunoService(AlunoRepository repository, MateriaRepository materiaRepository)
        {
            _repository = repository;
            _materiaRepository = materiaRepository;
        }
        
        /*public void RegistraAluno(AlunoDTO aluno)
        {
            *//*aluno.Validar();
            if(!aluno.Valido) return aluno.GetErrors();*//*
            List<Materia> materias = new List<Materia>();
            foreach (var id in aluno.Materias_id)
                materias.Add(_materiaRepository.SelectById(id));

            if(materias.Count > 3 || materias.Count < 1)
                throw new Exception("Precisa se cadastrar no mínimo em 1 matéria e no máximo em 3");
            
            _repository.Insert(new Aluno
                (
                nome: aluno.Nome,
                materias: materias,
                idade: aluno.Idade,
                turma_id: aluno.Turma_id
                ));
            
        }*/
    }
}
