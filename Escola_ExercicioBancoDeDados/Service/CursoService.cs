using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.Endity;
using Escola_ExercicioBancoDeDados.Repository;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Service
{
    public class CursoService
    {
        private readonly CursoRepository _repository;
        private readonly MateriaCursoRepository _materiaCursoRepository;
        private readonly MateriaRepository _materiaRepository;
        private readonly IConfiguration _configuration;
        private string ConnectionStting => _configuration.GetConnectionString("AppAcademy");

        public CursoService(CursoRepository repository,
            MateriaCursoRepository materiaCursoRepository,
            MateriaRepository materiaRepository,
            IConfiguration configuration)
        {
            _repository = repository;
            _materiaCursoRepository = materiaCursoRepository;
            _materiaRepository = materiaRepository;
            _configuration = configuration;
        }

        public Curso RegistraCurso(CursoDTO cursoDTO)
        {
            // Busca das matérias
            List<Materia> materias = new List<Materia>();
            foreach (var materia_id in cursoDTO.Materias_id)
            {
                var materia = _materiaRepository.SelectById(materia_id);
                materias.Add(materia);
            }
            if (materias.Count != cursoDTO.Materias_id.Count) throw new System.Exception("Alguma matéria não foi encontrada");
            // Estancia o Curso e abre a conexão.
            var curso = new Curso(materias: materias, nome: cursoDTO.Nome);

            try
            {
                using var conn = new OracleConnection(ConnectionStting);
                conn.Open();
                // Create a local transaction
                OracleTransaction transaction = conn.BeginTransaction();
                var command = new OracleCommand(@"INSERT INTO APPACADEMY.CURSO
                                            (NOME, ID)
                                            VALUES(:Nome, :Id)", conn);
                command.Transaction = transaction;
                _repository.Insert(curso, command);
                var command2 = new OracleCommand(@"INSERT INTO APPACADEMY.MATERIA_CURSO
                                            (ID, MATERIA_ID, CURSO_ID)
                                            VALUES(:Id, :Materia_id, :Curso_id)", conn);
                command2.Transaction = transaction;
                _materiaCursoRepository.Insert(curso, command2);
                transaction.Commit();
            }
            catch (Exception)
            {
                throw;
            }
            return curso;
        }
    }
}
