namespace MikeScriptInterpreter
{
    public interface IToken
    {
        TokenType Type { get; }
    }

    public enum TokenType
    {
        KeyWord,
        StringValue,
        NumberValue
    }

    public static class ITokenExtensions
    {
        public static bool As<T>(this IToken token, out T @out) where T : class, IToken 
        {
            if (token is T)
            {
                @out = token as T;
                return true;
            }

            @out = null;
            return false;
        }
    }
}