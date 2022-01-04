using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.DTO.QueryParameters;
using Escola_ExercicioBancoDeDados.DTO.Result;
using Escola_ExercicioBancoDeDados.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Escola_ExercicioBancoDeDados.Controllers
{
    [ApiController, Route("[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly CursoService _service;

        public CursoController(CursoService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post(CursoDTO curso)
        {
            curso.Validar();
            if (!curso.Valido) return BadRequest(curso.GetErrors());
            try 
            {
                return Ok(_service.RegistraCurso(curso));
            }
            catch(Exception ex) 
            {
                return BadRequest(new ExceptionResult { Message = ex.Message});
            }
        }
        [HttpGet]
        public IActionResult Get([FromQuery]CursoQuery cursoQuery)
        {
            try
            {
                return Ok(_service.GetCursos(cursoQuery));
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
                return BadRequest( new ExceptionResult { Message = ex.Message });
            }
        }
    }
}
