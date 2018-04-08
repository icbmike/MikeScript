namespace MikeScriptInterpreter.Expressions
{
    public class MultiplyExpression : ExpressionStatement
    {
        public MultiplyExpression(ExpressionStatement arg1, ExpressionStatement arg2)
        {
            Arg1 = arg1;
            Arg2 = arg2;
        }

        public ExpressionStatement Arg1 { get; }
        public ExpressionStatement Arg2 { get; }

        public override object Value()
        {
            return (int)Arg1.Value() * (int)Arg2.Value();
        }
    }
}
