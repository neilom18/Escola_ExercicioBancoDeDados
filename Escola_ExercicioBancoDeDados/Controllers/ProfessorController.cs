using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.DTO.Result;
using Escola_ExercicioBancoDeDados.Service;
using Microsoft.AspNetCore.Mvc;

namespace Escola_ExercicioBancoDeDados.Controllers
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
            catch (System.Exception ex)
            {
                return BadRequest(new ExceptionResult { Message = ex.Message });
            }
        }
    }
}
