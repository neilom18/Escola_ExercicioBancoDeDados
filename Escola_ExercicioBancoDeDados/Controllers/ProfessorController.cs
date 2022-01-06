using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.DTO.Result;
using Escola_ExercicioBancoDeDados.Service;
using Microsoft.AspNetCore.Mvc;
using System;

/*namespace Escola_ExercicioBancoDeDados.Controllers
{
    [ApiController, Route("[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly ProfessorService _service;

        public ProfessorController(ProfessorService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post(ProfessorDTO professor)
        {
            professor.Validar();
            if (!professor.Valido) return BadRequest(professor.GetErrors());
            try
            {
                return Ok(_service.RegistraProfessor(professor));
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Message = ex.Message });
            }
        }
        [HttpPut, Route("{id}")]
        public IActionResult Put(ProfessorDTO professor, Guid id)
        {
            professor.Validar();
            if (!professor.Valido) return BadRequest(professor.GetErrors());
            try
            {
                return Ok(_service.UpdateProfessor(professor, id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Message = ex.Message });
            }
        }
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Message = ex.Message });
            }
        }
    }
}*/
