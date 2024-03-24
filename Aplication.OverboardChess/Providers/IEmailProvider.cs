using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Providers
{
    public interface IEmailProvider
    {
        public void Send(Email email); 
    }

    public readonly struct Email(string to, string subject, string body)
    {
        public string To { get; } = to;
        public string Subject { get; } = subject;
        public string Body { get; } = body;
    }
}
