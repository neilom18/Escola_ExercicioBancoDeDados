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
            // Estancia o curso e abre a conexão.
            var curso = new Curso(materias: materias, nome: cursoDTO.Nome);
            var conn = new OracleConnection(ConnectionStting);
            conn.Open();
            // Create a local transaction
            OracleCommand command = conn.CreateCommand();
            OracleTransaction transaction = conn.BeginTransaction();
            command.Transaction = transaction;

            try
            {
                command.Connection = conn;
                command.CommandText = @"INSERT INTO APPACADEMY.CURSO
                                            (NOME, ID)
                                            VALUES(:Nome, :Id)";
                _repository.Insert(curso, command);
                command.Parameters.Clear();
                command.CommandText = @"INSERT INTO APPACADEMY.MATERIA_CURSO
                                            (ID, MATERIA_ID, CURSO_ID)
                                            VALUES(:Id, :Materia_id, :Curso_id)";
                _materiaCursoRepository.Insert(curso, command);
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

            return curso;
        }
    }
}
