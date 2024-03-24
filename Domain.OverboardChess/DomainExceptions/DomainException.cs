using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OverboardChess.DomainExceptions
{
    public class DomainException : Exception
    {
        public DomainExceptionType Type { get; }
        public Dictionary<string, List<string>> Errors { get; set; } = [];
    }

    public enum DomainExceptionType
    {
        BadRequest,
        Forbidden,
    }
}
