using Pidgin;
using static Pidgin.Parser;

namespace JsonParser
{
    public static class CommandLineParser
    {
        public static readonly Parser<char, char> commandLineParser = Char('/');
    }
}
