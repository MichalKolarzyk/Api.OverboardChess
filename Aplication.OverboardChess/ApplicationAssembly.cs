using System.Reflection;

namespace Aplication.OverboardChess
{
    public class ApplicationAssembly
    {
        public static Assembly Assembly => typeof(ApplicationAssembly).GetTypeInfo().Assembly;
    }
}
