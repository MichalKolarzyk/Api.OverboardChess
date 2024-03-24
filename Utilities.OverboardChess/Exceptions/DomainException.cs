using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.OverboardChess.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(DomainExceptionType type, Dictionary<string, List<string>> errors)
        {
            Type = type;
            Errors = errors;
        }

        public DomainExceptionType Type { get; }
        public Dictionary<string, List<string>> Errors { get; } = [];
    }

    public enum DomainExceptionType
    {
        BadRequest,
        Forbidden,
    }
}
