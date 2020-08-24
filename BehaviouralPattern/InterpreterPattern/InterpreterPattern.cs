using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.BehaviouralPattern.InterpreterPattern
{
    public class Interpreter
    {
        public static List<Token> Lex(string input)
        {
            List < Token > result = new List<Token>();
            for (int i = 0; i < input.Length; i++)
            {
                switch(input[i])
                {
                    case '+':
                        result.Add(new Token(TokenType.Plus, "+"));
                        break;
                    case '-':
                        result.Add(new Token(TokenType.Minus, "-"));
                        break;
                    case '(':
                        result.Add(new Token(TokenType.LParen, "("));
                        break;
                    case ')':
                        result.Add(new Token(TokenType.RParen, ")"));
                        break;
                    default:
                        StringBuilder builder = new StringBuilder(input[i].ToString());
                        for (int j = i+1; j < input.Length; j++)
                        {
                            if (char.IsDigit(input[j]))
                            {
                                builder.Append(input[j]);
                                ++i;
                            }
                            else
                            {
                                result.Add(new Token(TokenType.Integer, builder.ToString()));
                                break;
                            }
                        }
                        break;
                }
            }
            return result;
        }

        public static BinaryOperation Parse(IReadOnlyList<Token> tokens)
        {
            var result = new BinaryOperation();
            bool havelHS = false;

            for (int i = 0; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                switch (token.Type)
                {
                    case TokenType.Integer:
                        {
                            Integer integer = new Integer(int.Parse(token.Text));
                            if (!havelHS)
                            {
                                result.Left = integer;
                                havelHS = true;
                            }
                            else
                            {
                                result.Right = integer;
                            }
                        }

                        break;
                    case TokenType.Plus:
                        result.Type = BinaryOperationType.Addition;
                        break;
                    case TokenType.Minus:
                        result.Type = BinaryOperationType.Substraction;

                        break;
                    case TokenType.LParen:
                        {
                            int j = i;

                            for (; j < tokens.Count; j++)
                            {
                                if (tokens[j].Type == TokenType.RParen)
                                    break;   
                            }

                            var subExpression = tokens.Skip(i + 1).Take(j - i - 1).ToList();
                            IElement element = Parse(subExpression);
                            if (!havelHS)
                            {
                                result.Left = element;
                                havelHS = true;
                            }
                            else
                            {
                                result.Right = element;
                            }
                            i = j;
                        }
                        break;
                   
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return result;
        }
    }
      

    public enum TokenType
    {
        Integer=1 ,Plus ,Minus ,LParen ,RParen
    }


    public class Token
    {
        public TokenType Type;
        public string Text;

        public Token(TokenType type, string text)
        {
            Type = type;
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public override string ToString()
        {
            return $"'{Text}'";
        }
    }

    public interface IElement
    {
        int Value { get; }
    }

    public class Integer : IElement
    {
        public int Value { get; }

        public Integer(int value)
        {
            Value = value;
        }
    }

    public class BinaryOperation : IElement
    {

        public int Value { get {

                switch (Type)
                {
                    case BinaryOperationType.Addition:
                        return Left.Value + Right.Value;
                        break;
                    case BinaryOperationType.Substraction:
                        return Left.Value - Right.Value;

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            } }
        public BinaryOperationType Type { set; get; }
        public IElement Left, Right;
       
    }

    public enum BinaryOperationType
    {
        Addition=1,
        Substraction
    }


}
