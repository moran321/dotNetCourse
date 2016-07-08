using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace AttribDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyAnalyzer analyzer = new AssemblyAnalyzer();
            bool IsAllApproved = analyzer.AnalayzeAssembly(Assembly.GetEntryAssembly());
            Console.WriteLine("Is all approved: " + IsAllApproved);
        }
    }
}
