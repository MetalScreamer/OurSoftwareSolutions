using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.BuisinessLayer.SyntaxHelpers
{
    public abstract class DataTypeSyntax
    {
        private class IntegerSyntax : DataTypeSyntax
        {
            public override string GetCSharpDataType()
            {
                return "int";
            }

            public override string GetCSharpLiteral(object value)
            {
                return ((int)value).ToString();
            }
        }
        private class BooleanSyntax : DataTypeSyntax
        {
            public override string GetCSharpDataType()
            {
                return "bool";
            }

            public override string GetCSharpLiteral(object value)
            {
                return (bool)value ? "true" : "false";
            }
        }
        private class DoubleSyntax : DataTypeSyntax
        {
            public override string GetCSharpDataType()
            {
                return "double";
            }

            public override string GetCSharpLiteral(object value)
            {
                return ((double)value).ToString();
            }
        }
        private class StringSyntax : DataTypeSyntax
        {
            public override string GetCSharpDataType()
            {
                return "string";
            }

            public override string GetCSharpLiteral(object value)
            {
                var str = (string)value;
                if (string.IsNullOrEmpty(str))
                {
                    return "string.Empty";
                }

                return
                    "\"" +
                    str
                    .Replace("\\", "\\\\")
                    .Replace("\"", "\\\"")
                    .Replace("\'", "\\\'")
                    .Replace("\n", "\\\n")
                    .Replace("\t", "\\\t")
                    .Replace("\0", "\\\0")
                    .Replace("\a", "\\\a")
                    .Replace("\b", "\\\b")
                    .Replace("\f", "\\\f")
                    .Replace("\r", "\\\r")
                    .Replace("\v", "\\\v") +
                    "\"";
            }
        }
        private class LongSyntax : DataTypeSyntax
        {
            public override string GetCSharpDataType()
            {
                return "long";
            }

            public override string GetCSharpLiteral(object value)
            {
                return ((long)value).ToString();
            }
        }
        private class DateTimeSyntax : DataTypeSyntax
        {
            public override string GetCSharpDataType()
            {
                return "System.DateTime";
            }

            public override string GetCSharpLiteral(object value)
            {
                var dt = (DateTime)value;
                return $"new DateTime({dt.Ticks})";
            }
        }

        private static readonly IntegerSyntax integerSyntax = new IntegerSyntax();
        private static readonly BooleanSyntax booleanSyntax = new BooleanSyntax();
        private static readonly DoubleSyntax doubleSyntax = new DoubleSyntax();
        private static readonly StringSyntax stringSyntax = new StringSyntax();
        private static readonly LongSyntax longSyntax = new LongSyntax();
        private static readonly DateTimeSyntax dateTimeSyntax = new DateTimeSyntax();

        private static readonly Dictionary<Type, DataTypeSyntax> syntaxDictionary =
            new Dictionary<Type, DataTypeSyntax>()
            {
                {
                    typeof(int),
                    integerSyntax
                },
                {
                    typeof(bool),
                    booleanSyntax
                },
                {
                    typeof(double),
                    doubleSyntax
                },
                {
                    typeof(string),
                    stringSyntax
                },
                {
                    typeof(long),
                    longSyntax
                },
                {
                    typeof(DateTime),
                    dateTimeSyntax
                }
            };

        public abstract string GetCSharpDataType();
        public abstract string GetCSharpLiteral(object value);

        public static DataTypeSyntax GetSyntaxForType(Type type) => syntaxDictionary[type];
        public static DataTypeSyntax GetSyntaxForType<T>() => syntaxDictionary[typeof(T)];
    }
}
