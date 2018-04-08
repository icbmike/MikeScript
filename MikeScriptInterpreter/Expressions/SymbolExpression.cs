using System.Collections.Generic;

namespace MikeScriptInterpreter.Expressions
{
    public class SymbolExpression : ExpressionStatement
    {
        public SymbolExpression(IReadOnlyDictionary<string, object> scope, string symbol)
        {
            Scope = scope;
            Symbol = symbol;
        }

        public IReadOnlyDictionary<string, object> Scope { get; }
        public string Symbol { get; }

        public override object Value()
        {
            return Scope[Symbol];
        }
    }
}
