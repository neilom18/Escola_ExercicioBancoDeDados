using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.DTO.QueryParametes;
using Escola_ExercicioBancoDeDados.DTO.Result;
using Escola_ExercicioBancoDeDados.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Escola_ExercicioBancoDeDados.Controllers
{
    [ApiController, Route("[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoService _service;
        public AlunoController(AlunoService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post(AlunoDTO alunoDTO)
        {
            alunoDTO.Validar();
            if (!alunoDTO.Valido) return BadRequest(alunoDTO.GetErrors());
            try
            {
                return Ok(_service.RegistraAluno(alunoDTO));
            }
            catch(Exception ex)
            {
                return BadRequest(new ExceptionResult { Message = ex.Message});
            }
        }
        [HttpPost, Route("materia")]
        public IActionResult PostMateria(UpdateAlunoDTO updateAlunoDTO)
        {
            updateAlunoDTO.Validar();
            if (!updateAlunoDTO.Valido) return BadRequest(updateAlunoDTO.GetErrors());
            try
            {
                return Ok(_service.UpdateMaterias(updateAlunoDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Message = ex.Message});
            }
        }
        [HttpGet]
        public IActionResult Get([FromQuery]AlunoQuery aluno)
        {
            try
            {
                return Ok(_service.GetAlunos(aluno));
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Message = ex.Message });
            }
        }
        [HttpDelete, Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest( new ExceptionResult { Message = ex.Message} );
            }
        }
    }
}
