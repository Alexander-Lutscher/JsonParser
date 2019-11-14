using System.Collections.Generic;
using System.Linq;
using Pidgin;
using static Pidgin.Parser<char>;
using static Pidgin.Parser;


namespace JsonParser
{
    public class ComputerphileParser
    {
        /*
         *     <sentence>    ::= <subject> <verb> <object>
         *     <subject>     ::= <article> <noun> | the robot
         *     <verb>        ::= bit | kicked | stroked
         *     <object>      ::= <article> <noun> | two furry dice
         *     <article>     ::= the | a
         *     <noun>        ::= dog | cat | man | woman | robot
         */

        private static readonly Parser<char, string> Space = String(" ").Or(String("."));

        
        public static readonly Parser<char, string> Article = OneOf(String("the"),String("a"));
        public static readonly Parser<char, string> Noun = OneOf(String("dog"),String("cat"),String("man"), String("woman"), String("robot")).Labelled("a Noun");
        public static readonly Parser<char, string> Verb = OneOf(String("bit"),String("kicked"),String("stroked")).Labelled("a Verb");
        
        public static readonly Parser<char, string> ArticleAndNoun = Map(x => string.Join(' ', x), Sequence(Article, Noun).Between(Space));
        
        public static readonly Parser<char, string> Subject = OneOf(ArticleAndNoun, String("the robot")).Labelled("a Subject");
        public static readonly Parser<char, string> Object = OneOf(ArticleAndNoun, String("two furry dice")).Labelled("an Object");

        public static readonly Parser<char, IEnumerable<string>> Sentence =
            Sequence(Subject.Before(Space), Verb.Before(Space), Object.Before(Space));
    }
}