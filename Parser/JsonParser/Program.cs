using NUnit.Framework;
using Pidgin;

namespace JsonParser
{
    internal class Program
    {
        private static void Main(string[] args)
        {
        }
    }

    [TestFixture]
    public class Test
    {
        [Test]
        public void Test123()
        {
            Assert.DoesNotThrow(() => CommandLineParser.commandLineParser.ParseOrThrow((string)null));
        }
    }
}
