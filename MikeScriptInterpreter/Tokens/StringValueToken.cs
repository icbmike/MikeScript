namespace MikeScriptInterpreter
{
    public class StringValueToken : IToken
    {
        public TokenType Type => TokenType.StringValue;

        public string Value { get; }

        public StringValueToken(string value)
        {
            Value = value;
        }
    }
}