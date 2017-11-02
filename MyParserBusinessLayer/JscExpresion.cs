using Oss.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Oss.BuisinessLayer
{
    public class JscExpresion : IExpressionService
    {
        private const string UNTERMINATED_STRING = "Unterminated string";
        private const string UNRECOGNIZED_OPERATOR = "Unrecognized Operator";
        private const string INVALID_NUMERIC_FORMAT = "Invalid Numeric Format";
        private const string UNRECOGNIZED_CHARACTER = "Unrecognized Character";

        private static readonly char[] SYMBOLS = { '+', '-', '*', '/', '=', '<', '>', '!' };
        private static readonly Regex IDENITIFIER_START_PATTERN = new Regex("[A-Z|a-z|_]");
        private static readonly Regex IDENITIFIER_CHAR_PATTERN = new Regex("[A-Z|a-z|0-9|_]");
        private static readonly Regex BAD_LEADING_ZERO_PATTERN = new Regex("^0[^.]");

        public IEnumerable<IToken> GetTokens(string expression)
        {
            return BuildTokenList(expression);
        }

        public bool IsValid(string expression)
        {
            return !expression.ToCharArray().Any(c => c != 'A');
        }

        private static List<IToken> BuildTokenList(string expression)
        {
            var idx = 0;
            var lastIdx = -1;
            var result = new List<IToken>();

            while (idx < expression.Length && idx != lastIdx)
            {
                lastIdx = idx;

                EatWhiteSpace(expression, ref idx);
                if (idx >= expression.Length) break;
                var currChar = expression[idx];

                if (currChar == '"')
                {
                    idx++;
                    if (idx >= expression.Length) throw new ExpressionParsingException(UNTERMINATED_STRING);
                    var str = ExtractStringLiteralToken(expression, ref idx);

                    result.Add(Token.CreateToken(TokenType.StringLiteral, str));
                }
                else if (IsSymbol(currChar))
                {
                    var operatorText = ExtractOperatorText(expression, ref idx);
                    result.Add(Token.CreateToken(TokenType.Operator, operatorText));

                    idx++;
                }
                else if (char.IsNumber(currChar))
                {
                    var numericToken = ExtractNumericToken(expression, ref idx);
                    result.Add(numericToken);
                }
                else if (IDENITIFIER_START_PATTERN.IsMatch(currChar.ToString()))
                {
                    var identifierText = ExtractIdentifierText(expression, ref idx);
                    result.Add(Token.CreateToken(TokenType.Identifier, identifierText));
                }
                else if (currChar == '(')
                {
                    idx++;
                    result.Add(Token.CreateToken(TokenType.OpenParenthesis, "("));
                }
                else if (currChar == ')')
                {
                    idx++;
                    result.Add(Token.CreateToken(TokenType.ClosedParenthesis, ")"));
                }
                else
                {
                    throw new ExpressionParsingException(UNRECOGNIZED_CHARACTER);
                }
            }

            return result;
        }

        private static string ExtractIdentifierText(string expression, ref int idx)
        {
            var result = string.Empty;
            while (idx < expression.Length && IDENITIFIER_CHAR_PATTERN.IsMatch(expression[idx].ToString())) result += expression[idx++];

            return result;
        }

        private static IToken ExtractNumericToken(string expression, ref int idx)
        {
            var currChar = expression[idx];
            var wasDecimalFound = false;
            var numericText = string.Empty;

            while (idx < expression.Length && (char.IsNumber(currChar) || (currChar == '.' && !wasDecimalFound)))
            {
                if (currChar == '.') wasDecimalFound = true;
                numericText += currChar;

                if (++idx < expression.Length)
                {
                    currChar = expression[idx];
                }
            }

            if (numericText.EndsWith(".")) throw new ExpressionParsingException(INVALID_NUMERIC_FORMAT);
            if (BAD_LEADING_ZERO_PATTERN.IsMatch(numericText)) throw new ExpressionParsingException(INVALID_NUMERIC_FORMAT);

            TokenType tokenType;
            if (wasDecimalFound)
            {
                tokenType = TokenType.Decimal;
            }
            else
            {
                tokenType = TokenType.Integer;
            }

            return Token.CreateToken(tokenType, numericText);
        }

        private static string ExtractOperatorText(string expression, ref int idx)
        {
            string result;

            var currChar = expression[idx];
            if (new char[] { '+', '-', '*', '/' }.Any(c => c == currChar))
            {
                result = currChar.ToString();
            }
            else
            {
                char nextChar = (idx + 1 < expression.Length) ? expression[idx + 1] : '\0';
                if (currChar == '=' && nextChar != '=') throw new ExpressionParsingException(UNRECOGNIZED_OPERATOR);
                if (nextChar == '=')
                {
                    result = currChar.ToString() + nextChar;
                    idx++;
                }
                else
                {
                    result = currChar.ToString();
                }
            }

            return result;
        }

        private static string ExtractStringLiteralToken(string expression, ref int idx)
        {
            var result = string.Empty;

            var stringTerminatorFound = false;

            while (idx < expression.Length && !stringTerminatorFound)
            {
                var currChar = expression[idx];
                if (currChar == '"')
                {
                    stringTerminatorFound = true;
                }
                else if (currChar == '\\')
                {
                    idx++;
                    if (idx >= expression.Length) throw new ExpressionParsingException(UNTERMINATED_STRING);
                    currChar = expression[idx];
                }

                if (!stringTerminatorFound)
                {
                    result += currChar;
                }

                idx++;
            }

            if (!stringTerminatorFound)
            {
                throw new ExpressionParsingException(UNTERMINATED_STRING);
            }

            return result;
        }

        private static void EatWhiteSpace(string expression, ref int idx)
        {
            while (idx < expression.Length && char.IsWhiteSpace(expression[idx])) idx++;
        }

        private static bool IsSymbol(char chr) => SYMBOLS.Any(c => c == chr);
    }
}
