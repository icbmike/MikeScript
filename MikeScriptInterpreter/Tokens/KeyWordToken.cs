namespace MikeScriptInterpreter
{
    public class KeyWordToken: IToken
    {
        public TokenType Type => TokenType.KeyWord;

        public KeyWord Value { get; }

        public KeyWordToken(KeyWord value)
        {
            Value = value;
        }
    }

    public enum KeyWord
    {
        print,
        add,
        mult,
        assign
    }
}