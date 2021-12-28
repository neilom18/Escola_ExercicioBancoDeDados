using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.DTO.Result;
using Escola_ExercicioBancoDeDados.Service;
using Microsoft.AspNetCore.Mvc;

namespace Escola_ExercicioBancoDeDados.Controllers
{
    [ApiController, Route("[controller]")]
    public class TurmaController : ControllerBase
    {
        private readonly TurmaService _service;
        public TurmaController(TurmaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult PostTurma(TurmaDTO turma)
        {
            turma.Validar();
            if (!turma.Valido) return BadRequest(turma.GetErrors());
            try
            {
                return Ok(_service.RegistraTurma(turma));
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ExceptionResult {  Success = false, Message = ex.Message});
            }
        }
    }
}
