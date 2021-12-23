using Escola_ExercicioBancoDeDados.Endity;
using Escola_ExercicioBancoDeDados.Repository;
using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Service
{
    public class AlunoService
    {
        private readonly AlunoRepository _repository;
        public AlunoService(AlunoRepository repository)
        {
            _repository = repository;
        }
        
        public void RegistraAluno(Aluno aluno)
        {
            if (ValidarMaterias(aluno.Materias))
            {
                _repository.Insert(aluno);
            }
            else throw new Exception();
        }

        public bool ValidarMaterias(List<Materia> materias)
        {
            if(materias.Count > 3)
            {
                return false;
            }
            // Verificar se as matérias existem
            return true;
        }
    }
}
