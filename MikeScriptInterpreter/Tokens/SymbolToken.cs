namespace MikeScriptInterpreter.Tokens
{
    public class SymbolToken : IToken
    {
        public TokenType Type => TokenType.Symbol;

        public string SymbolText { get; }

        public SymbolToken(string symbolText)
        {
            SymbolText = symbolText;
        }
    }
}
