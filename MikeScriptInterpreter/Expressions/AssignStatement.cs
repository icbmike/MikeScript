using System.Collections.Generic;

namespace MikeScriptInterpreter.Expressions
{
    public class AssignStatement : IStatement
    {
        public AssignStatement(Dictionary<string, object> scope, string symbol, ExpressionStatement expression)
        {
            Scope = scope;
            Symbol = symbol;
            Expression = expression;
        }

        public Dictionary<string, object> Scope { get; }
        public string Symbol { get; }
        public ExpressionStatement Expression { get; }

        public void Execute()
        {
            Scope[Symbol] = Expression.Value();
        }
    }
}
