using System;
using System.Collections.Generic;
using System.Text;

namespace MikeScriptInterpreter
{
    public static class Tokenizer
    {
        public static IEnumerable<IToken> TokenizeLine(string line)
        {
            bool inString = false;
            bool inKeyWord = false;
            bool inNumber = false;
            var sb = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                var character = line[i];

                // Parse string value token
                if (character == '\'')
                {
                    inString = !inString;

                    if (!inString)
                    {
                        yield return new StringValueToken(sb.ToString());
                        sb.Clear();
                    }

                    continue;
                }

                if (inString)
                {
                    sb.Append(character);
                    continue;
                }
                
                if (char.IsWhiteSpace(character))
                {
                    if(inKeyWord)
                    {
                        inKeyWord = false;
                        var text = sb.ToString();
                        if(Enum.TryParse<KeyWord>(text, out var keyWord))
                        {
                            yield return new KeyWordToken(keyWord);
                        }
                    }

                    if(inNumber)
                    {
                        inNumber = false;
                        var text = sb.ToString();
                        if(Int32.TryParse(text, out var number))
                        {
                            yield return new NumberValueToken(number);
                        }
                    }

                    sb.Clear();
                }

                if(char.IsNumber(character))
                {
                    inNumber = true;
                    sb.Append(character);
                }

                if (char.IsLetter(character))
                {
                    inKeyWord = true;
                    sb.Append(character);
                }
            }

            if (inKeyWord)
            {
                inKeyWord = false;
                var text = sb.ToString();
                if (Enum.TryParse<KeyWord>(text, out var keyWord))
                {
                    yield return new KeyWordToken(keyWord);
                }
            }

            if (inNumber)
            {
                inNumber = false;
                var text = sb.ToString();
                if (Int32.TryParse(text, out var number))
                {
                    yield return new NumberValueToken(number);
                }
            }

            sb.Clear();
        }
    }
}