using Pidgin;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;
namespace JsonParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = ComputerphileParser.Sentence;
            var parseResult = parser.ParseOrThrow("the potato stroked two furry dice.");
        }   
    }
}