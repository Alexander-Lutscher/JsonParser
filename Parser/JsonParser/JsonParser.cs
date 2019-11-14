using System.Collections.Generic;
using System.Collections.Immutable;
using Pidgin;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace JsonParser
{
    public static class JsonParser
    {
        public static readonly Parser<char, char> LBrace = Char('{');
        public static readonly Parser<char, char> RBrace = Char('}');
        public static readonly Parser<char, char> LBracket = Char('[');
        public static readonly Parser<char, char> RBracket = Char(']');
        public static readonly Parser<char, char> Quote = Char('"');
        public static readonly Parser<char, char> Colon = Char(':');
        public static readonly Parser<char, char> ColonWhitespace =
            Colon.Between(SkipWhitespaces);
        public static readonly Parser<char, char> Comma = Char(',');

        public static readonly Parser<char, string> String =
            Token(c => c != '"')
                .ManyString()
                .Between(Quote);
        public static readonly Parser<char, IJson> JsonString =
            String.Select<IJson>(s => new JsonString(s));
            
        public static readonly Parser<char, IJson> Json =
            JsonString.Or(Rec(() => JsonArray)).Or(Rec(() => JsonObject));

        public static readonly Parser<char, IJson> JsonArray = 
            Json.Between(SkipWhitespaces)
                .Separated(Comma)
                .Between(LBracket, RBracket)
                .Select<IJson>(els => new JsonArray(els.ToImmutableArray()));
        
        public static readonly Parser<char, KeyValuePair<string, IJson>> JsonMember =
            String
                .Before(ColonWhitespace)
                .Then(Json, (name, val) => new KeyValuePair<string, IJson>(name, val));

        public static readonly Parser<char, IJson> JsonObject = 
            JsonMember.Between(SkipWhitespaces)
                .Separated(Comma)
                .Between(LBrace, RBrace)
                .Select<IJson>(kvps => new JsonObject(kvps.ToImmutableDictionary()));
        
        public static Result<char, IJson> Parse(string input) => Json.Parse(input);
    }
}