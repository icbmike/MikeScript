using MikeScriptInterpreter.Expressions;
using MikeScriptInterpreter.Tokens;
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
            var scope = new Dictionary<string, object>();

            foreach (var line in lines)
            {
                var statement = ParseStatement(line, scope);

                statement.Execute();
            }
        }

        private IStatement ParseStatement(string line, Dictionary<string, object> scope)
        {
            var tokens = Tokenizer.TokenizeLine(line).ToList();

            (var statement, _) =  ParseStatement(new Queue<IToken>(tokens), scope);

            if(statement == null)
            {
                throw new Exception($"Can't parse line: {line}");
            }

            return statement;
        }

        private (IStatement statement, Queue<IToken> restOfQueue) ParseStatement(Queue<IToken> tokens, Dictionary<string, object> scope)
        {
            var token = tokens.Dequeue();

            if (token.As<KeyWordToken>(out var keyWordToken))
            {
                if (keyWordToken.Value == KeyWord.add)
                {
                    (var arg1, var restOfQueue) = ParseStatement(tokens, scope);
                    (var arg2, var restOfQueue2) = ParseStatement(restOfQueue, scope);

                    return (new AddExpression((ExpressionStatement)arg1, (ExpressionStatement)arg2), restOfQueue2);
                }

                if (keyWordToken.Value == KeyWord.mult)
                {
                    (var arg1, var restOfQueue) = ParseStatement(tokens, scope);
                    (var arg2, var restOfQueue2) = ParseStatement(restOfQueue, scope);

                    return (new MultiplyExpression((ExpressionStatement)arg1, (ExpressionStatement)arg2), restOfQueue2);
                }

                if (keyWordToken.Value == KeyWord.print)
                {
                    (var arg1, var restOfQueue) = ParseStatement(tokens, scope);
                    return (new PrintStatement((ExpressionStatement)arg1), restOfQueue);
                }

                if(keyWordToken.Value == KeyWord.assign)
                {
                    (var arg1, var restOfQueue) = ParseStatement(tokens, scope);
                    (var arg2, var restOfQueue2) = ParseStatement(restOfQueue, scope);

                    return (new AssignStatement(scope, ((SymbolExpression)arg1).Symbol, (ExpressionStatement)arg2), restOfQueue2);
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

            if (token.As<SymbolToken>(out var symbolToken))
            {
                return (new SymbolExpression(scope, symbolToken.SymbolText), tokens);
            }

            throw new Exception($"Unknown token type: {token.Type}");
        }
    }
}