using System;

namespace MikeScriptInterpreter
{
    public class PrintStatement: IStatement
    {
        public ExpressionStatement Argument { get; }

        public PrintStatement(ExpressionStatement argument)
        {
            Argument = argument;
        }

        public void Execute()
        {
            Console.WriteLine(Argument.Value());
        }
    }
}