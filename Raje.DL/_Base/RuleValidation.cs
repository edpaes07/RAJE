using System;
using System.Collections.Generic;
using System.Linq;

namespace Raje.DL._Base
{
    public class RuleValidation
    {
        private readonly List<string> _messageErrors;

        private RuleValidation()
        {
            _messageErrors = new List<string>();
        }

        public static RuleValidation New()
        {
            return new RuleValidation();
        }

        public RuleValidation When(bool temErro, string mensagemDeErro)
        {
            if (temErro)
                _messageErrors.Add(mensagemDeErro);

            return this;
        }

        public void ThrowException()
        {
            if (_messageErrors.Any())
                throw new DomainException(_messageErrors);
        }
    }

    public class DomainException : ArgumentException
    {
        public List<string> MessageErrors { get; set; }

        public DomainException(List<string> messageErrors)
        {
            MessageErrors = messageErrors;
        }
    }
}
