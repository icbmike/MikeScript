using MikeScriptInterpreter.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MikeScriptInterpreter
{
    public class Interpreter
    {
        public void Run(string scriptText)
        {
            var lines = scriptText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var statement = ParseStatement(line);

                statement.Execute();
            }
        }

        private IStatement ParseStatement(string line)
        {
            var tokens = Tokenizer.TokenizeLine(line).ToList();

            var statement =  ParseStatement(tokens);

            if(statement == null)
            {
                throw new Exception($"Can't parse line: {line}");
            }

            return statement;
        }

        private IStatement ParseStatement(IEnumerable<IToken> tokens)
        {
            var token = tokens.First();

            if(token.As<KeyWordToken>(out var keyWordToken))
            {
                if(keyWordToken.Value == KeyWord.print)
                {
                    var nextStatement = ParseStatement(tokens.Skip(1));

                    if(nextStatement is ExpressionStatement)
                    {
                        return new PrintStatement(nextStatement as ExpressionStatement);
                    }
                }

                if(keyWordToken.Value == KeyWord.add)
                {
                    var arg1 = ParseStatement(tokens.Skip(1));
                    var arg2 = ParseStatement(tokens.Skip(2));

                    if (arg1 is ExpressionStatement && arg2 is ExpressionStatement)
                    {
                        return new AddExpression(arg1 as ExpressionStatement, arg2 as ExpressionStatement);
                    }
                }
            }

            if(token.As<StringValueToken>(out var stringValueToken))
            {
                return new StringLiteralExpression(stringValueToken);
            }

            if (token.As<NumberValueToken>(out var numberValueToken))
            {
                return new NumberLiteralExpression(numberValueToken);
            }

            return null;
        }
    }
}