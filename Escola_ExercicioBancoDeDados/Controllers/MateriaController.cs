﻿using Escola_ExercicioBancoDeDados.DTO;
using Escola_ExercicioBancoDeDados.DTO.Result;
using Escola_ExercicioBancoDeDados.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Escola_ExercicioBancoDeDados.Controllers
{
    [ApiController, Route("[controller]")]
    public class MateriaController : ControllerBase
    {
        private readonly MateriaService _service;
        public MateriaController(MateriaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult PostMateria(MateriaDTO materiaDTO)
        {
            materiaDTO.Validar();
            if (!materiaDTO.Valido) return BadRequest(materiaDTO.GetErrors());
            try
            {
                return Ok(_service.RegistraMateria(materiaDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Success = false, Message = ex.Message});
            }
        }
    }
}
