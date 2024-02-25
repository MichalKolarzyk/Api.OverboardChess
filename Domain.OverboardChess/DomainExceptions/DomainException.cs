using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OverboardChess.DomainExceptions
{
    public class DomainException
    {
        public DomainExceptionType Type { get; }
        public string Message { get; set; } =string.Empty;
        public Dictionary<string, string> Errors { get; set; } = [];
    }

    public enum DomainExceptionType
    {
        BadRequest,
        Forbidden,
    }
}
