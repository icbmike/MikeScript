using System;
using System.IO;

namespace MikeScriptInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = args[0];

            var scriptText = File.ReadAllText(fileName);

            new Interpreter().Run(scriptText);
        }
    }
}
