namespace MikeScriptInterpreter
{
    internal class StringLiteralExpression : ExpressionStatement
    {
        private StringValueToken stringValueToken;

        public StringLiteralExpression(StringValueToken stringValueToken)
        {
            this.stringValueToken = stringValueToken;
        }

        public override object Value()
        {
            return stringValueToken.Value;
        }
    }
}