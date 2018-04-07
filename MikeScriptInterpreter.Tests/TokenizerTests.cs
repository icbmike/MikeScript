using System.Linq;
using Xunit;

namespace MikeScriptInterpreter.Tests
{
    public class TokenizerTests
    {
        [Fact]
        public void print_keyword_and_string()
        {
            // Arrange
            var line = "print 'Hello there'";

            // Act
            var tokens = Tokenizer.TokenizeLine(line).ToArray();

            // Assert
            Assert.Equal(2, tokens.Count());

            var token1 = tokens[0];
            var token2 = tokens[1];

            Assert.Equal(TokenType.KeyWord, token1.Type);
            Assert.Equal(KeyWord.print, ((KeyWordToken)token1).Value);

            Assert.Equal(TokenType.StringValue, token2.Type);
            Assert.Equal("Hello there", ((StringValueToken)token2).Value);
        }

        [Fact]
        public void string_with_symbol()
        {
            // Arrange
            var line = "'What is up guys?'";

            // Act
            var tokens = Tokenizer.TokenizeLine(line).ToArray();

            // Assert
            Assert.Single(tokens);

            var token1 = tokens[0];

            Assert.Equal(TokenType.StringValue, token1.Type);
            Assert.Equal("What is up guys?", ((StringValueToken)token1).Value);
        }

        [Fact]
        public void numbers()
        {
            // Arrange
            var line = "23 5 6788373";

            // Act
            var tokens = Tokenizer.TokenizeLine(line).ToArray();

            // Assert
            Assert.Equal(3, tokens.Length);

            var token1 = tokens[0];
            var token2 = tokens[1];
            var token3 = tokens[2];

            Assert.Equal(TokenType.NumberValue, token1.Type);
            Assert.Equal(TokenType.NumberValue, token2.Type);
            Assert.Equal(TokenType.NumberValue, token3.Type);

            Assert.Equal(23, ((NumberValueToken)token1).Value);
            Assert.Equal(5, ((NumberValueToken)token2).Value);
            Assert.Equal(6788373, ((NumberValueToken)token3).Value);
        }
    }
}
