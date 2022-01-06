using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.DTO.Result;
using Escola_ExercicioBancoDeDados.Service;
using Microsoft.AspNetCore.Mvc;
using System;

/*namespace Escola_ExercicioBancoDeDados.Controllers
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
        public IActionResult Post(TurmaDTO turma)
        {
            turma.Validar();
            if (!turma.Valido) return BadRequest(turma.GetErrors());
            try
            {
                return Ok(_service.RegistraTurma(turma));
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Message = ex.Message});
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
                return BadRequest(new ExceptionResult { Message = ex.Message});
            }
        }
    }
}*/
