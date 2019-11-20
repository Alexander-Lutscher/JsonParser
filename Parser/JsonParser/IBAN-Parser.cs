using Pidgin;

using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace JsonParser
{
    public class IBAN
    {
        public string Country;
        public int Checksum;
        public string BankInfo;

        public IBAN(string country, int checksum, string bankInfo)
        {
            Country = country;
            Checksum = checksum;
            BankInfo = bankInfo;
        }
    }

    public static class IBAN_Parser
    {
        /*
         * IBAN Structure: The IBAN structure is defined in ISO 13616-1 and consists of a two-letter ISO 3166-1
         * country code, followed by two check digits and up to thirty alphanumeric characters for a BBAN
         * (Basic Bank Account Number) which has a fixed length per country and, included within it, a bank
         * identifier with a fixed position and a fixed length per country.
         *
         * DE75512108001245126199
         */
        public static readonly Parser<char, string> CountryCodeParser = Letter.RepeatString(2);
        public static readonly Parser<char, int> Checksum = Map(y => int.Parse(string.Join("", y)), Digit.Repeat(2));
        public static readonly Parser<char, string> BankInfoParser = Token(char.IsLetterOrDigit).ManyString();

        public static readonly Parser<char, IBAN> IBANParser =
            Map((a, b, c) => new IBAN(a, b, c), CountryCodeParser, Checksum, BankInfoParser);

        public static readonly Parser<char, IBAN> IBANParserLinq =
            from a in CountryCodeParser
            from b in Checksum
            from c in BankInfoParser
            select new IBAN(a, b, c);

        public static readonly Parser<char, IBAN> IBANParserTrim = IBANParser.Between(Whitespaces);
    }
}
