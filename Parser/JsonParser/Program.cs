using System;
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
        public void TestNull()
        {
            Assert.DoesNotThrow(() => CommandLine.CommandLineParser.ParseOrThrow((string)null));
        }
        
        [Test]
        public void TestNoCommand()
        {
            Assert.DoesNotThrow(() => CommandLine.CommandLineParser.ParseOrThrow(""));
        }
        
        [Test]
        public void TestBeginsWithSpecialCharacter()
        {
            Assert.DoesNotThrow(() => CommandLine.CommandLineParser.ParseOrThrow("/"));
        }
        
        [Test]
        public void TestCanParseAnyInput()
        {
            var aString = CommandLine.CommandLineParser.ParseOrThrow("/").Key;
            Assert.True(string.IsNullOrEmpty(aString), "No input was set");
        }
        
        [TestCase("test1")]
        public void TestCanParseAnyInputAndWriteIntoKey(string key)
        {
            var aString = CommandLine.CommandLineParser.ParseOrThrow($"/{key}").Key;
            Assert.AreEqual(key, aString);
        }
        
        [TestCase("test1","test2")]
        [TestCase("tessdft1","tesdft2")]
        [TestCase("tesaffaat1","testafdaf2")]
        [TestCase("abc","def")]
        public void TestCanParseKeyValueEntry(string key, string value)
        {
            var result = CommandLine.CommandLineParser.ParseOrThrow($"/{key}={value}");
            var keyResult = result.Key;
            var valueResult = result.Value;
            Assert.AreEqual(key, keyResult);
            Assert.AreEqual(value, valueResult);
        }
        
        [TestCase("","test2")]
        [TestCase("tessdft1","")]
        public void TestCannotParseEmptyKeyOrValue(string key, string value)
        {
            Assert.Throws<ParseException>(() => CommandLine.CommandLineParser.ParseOrThrow($"/{key}={value}"));
        }
    }
}
