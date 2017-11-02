namespace Oss.Common.Interfaces
{
    public enum TokenType
    {
        Operator,
        Identifier,
        Integer,
        Decimal,
        StringLiteral,
        OpenParenthesis,
        ClosedParenthesis
    }

    public interface IToken
    {
        TokenType Type { get; }
        string Text { get; }
    }
}
