using Oss.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.BuisinessLayer
{
    class Token : IToken
    {
        public string Text { get; }
        public TokenType Type { get; }

        private Token(TokenType type, string text) { Type = type; Text = text; }

        public static IToken CreateToken(TokenType type, string text) => new Token(type, text);

        public override string ToString()
        {
            return $"{Type.ToString()}: '{Text}'";
        }
    }
}
