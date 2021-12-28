using Escola_ExercicioBancoDeDados.DTO;
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
        public IActionResult PostAluno(AlunoDTO alunoDTO)
        {
            alunoDTO.Validar();
            if (!alunoDTO.Valido) return BadRequest(alunoDTO.GetErrors());
            try
            {
                return Ok(_service.RegistraAluno(alunoDTO));
            }
            catch(Exception ex)
            {
                return BadRequest(new ExceptionResult { Success = false, Message = ex.Message});
            }
        }
    }
}
