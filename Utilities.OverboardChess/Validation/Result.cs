using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.OverboardChess.Exceptions;

namespace Utilities.OverboardChess.Validation
{
    public class Result
    {
        public static Result Ok => new();
        public static Result FromError(string field, string error)
        {
            var result = new Result();
            result.AddError(field, error);
            return result;
        }

        private readonly Dictionary<string, List<string>> _errors = new();
        public bool HasErrors() => _errors.Count > 0;
        public void AddError(string field, string error)
        {
            var containsFieldErrors = _errors.ContainsKey(field);
            if(!containsFieldErrors)
                _errors.Add(field, []);

            _errors[field].Add(error);
        }


        public DomainException ToDomainException(DomainExceptionType domainExceptionType)
        {
            return new DomainException(domainExceptionType, _errors);
        }
    }
}
