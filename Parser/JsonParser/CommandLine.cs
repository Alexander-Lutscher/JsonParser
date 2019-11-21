using System;
using System.Collections.Generic;
using Pidgin;
using System.Linq;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace JsonParser
{
    public static class CommandLine
    {
        public static readonly Parser<char, KeyValuePair<string, string>> NullParser = End.ThenReturn(new KeyValuePair<string, string>("",""));
        public static readonly Parser<char, char> SeparationCharParser = Char('=');
        public static readonly Parser<char, char> StartingCharParser = Try(Char('/').Or(Try(Char('-'))));
        
        public static readonly Parser<char, KeyValuePair<string,string>> KeyOnlyCommandParser =
        (
            Map(
                x => new KeyValuePair<string, string>( x , "") , 
                Token(t => t != '=' ).ManyString().Before(End))
        );
        
        public static readonly Parser<char, KeyValuePair<string,string>> KeyAndValueCommandParser =
        (
            Map
            (
                (x) =>
                {
                    return new KeyValuePair<string, string>(x.ElementAt(0), x.ElementAt(1));
                },
                LetterOrDigit.AtLeastOnceString().Separated(SeparationCharParser)
            )
        );
        
        public static readonly Parser<char, KeyValuePair<string,string>> CommandLineParser = 
            NullParser.Or(
                StartingCharParser.Then(Try(KeyOnlyCommandParser).Or(KeyAndValueCommandParser)));
    }
}
