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

            (var statement, _) =  ParseStatement(new Queue<IToken>(tokens));

            if(statement == null)
            {
                throw new Exception($"Can't parse line: {line}");
            }

            return statement;
        }

        private (IStatement statement, Queue<IToken> restOfQueue) ParseStatement(Queue<IToken> tokens)
        {
            var token = tokens.Dequeue();

            if (token.As<KeyWordToken>(out var keyWordToken))
            {
                if (keyWordToken.Value == KeyWord.add)
                {
                    (var arg1, var restOfQueue) = ParseStatement(tokens);
                    (var arg2, var restOfQueue2) = ParseStatement(restOfQueue);

                    return (new AddExpression((ExpressionStatement)arg1, (ExpressionStatement)arg2), restOfQueue2);
                }

                if (keyWordToken.Value == KeyWord.mult)
                {
                    (var arg1, var restOfQueue) = ParseStatement(tokens);
                    (var arg2, var restOfQueue2) = ParseStatement(restOfQueue);

                    return (new MultiplyExpression((ExpressionStatement)arg1, (ExpressionStatement)arg2), restOfQueue2);
                }

                if (keyWordToken.Value == KeyWord.print)
                {
                    (var arg1, var restOfQueue) = ParseStatement(tokens);
                    return (new PrintStatement((ExpressionStatement)arg1), restOfQueue);
                }
            }

            if (token.As<StringValueToken>(out var stringToken))
            {
                return (new StringLiteralExpression(stringToken), tokens);
            }

            if (token.As<NumberValueToken>(out var numberToken))
            {
                return (new NumberLiteralExpression(numberToken), tokens);
            }

            throw new Exception($"Unknown token type: {token.Type}");
        }
    }
}