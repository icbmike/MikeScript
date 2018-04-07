namespace MikeScriptInterpreter
{
    public class NumberLiteralExpression : ExpressionStatement
    {
        private NumberValueToken numberValueToken;

        public NumberLiteralExpression(NumberValueToken numberValueToken)
        {
            this.numberValueToken = numberValueToken;
        }

        public override object Value()
        {
            return numberValueToken.Value;
        }
    }
}