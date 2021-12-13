using FluentAssertions;
using NumbersToWordsService;
using NUnit.Framework;

namespace NumbersToWordsTests
{
    public class Tests
    {
        public NumberToWordsConverter Converter { get; set; }

        [SetUp]
        public void Setup()
        {
            Converter = new NumberToWordsConverter();
        }

        [Test]
        public void CanConvertNumbersToWords()
        {
            var result1 = Converter.Convert("0");
            var result2 = Converter.Convert("1");
            var result3 = Converter.Convert("25,1");
            var result4 = Converter.Convert("0,01");
            var result5 = Converter.Convert("45 100");
            var result6 = Converter.Convert("999 999 999,99");

            result1.Should().Be("zero dollars");
            result2.Should().Be("one dollar");
            result3.Should().Be("twenty-five dollars and ten cents");
            result4.Should().Be("zero dollars and one cent");
            result5.Should().Be("forty-five thousand one hundred dollars");
            result6.Should().Be("nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents");
        }
    }
}