namespace MikeScriptInterpreter
{
    public class NumberValueToken : IToken
    {
        public TokenType Type => TokenType.NumberValue;

        public int Value { get; }

        public NumberValueToken(int value)
        {
            Value = value;
        }
    }
}