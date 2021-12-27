﻿using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.DTO
{
    public abstract class Validator
    {
        protected readonly Dictionary<string, string> _errors;
        public Validator()
        {
            _errors = new Dictionary<string, string>();
        }
        public bool Valido { get; protected set; }
        public abstract void Validar();

        public abstract Dictionary<string, string> GetErrors();
    }
}
