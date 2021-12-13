using FluentAssertions;
using NumbersToWordsService;
using NUnit.Framework;

namespace NumbersToWordsTests
{
    class NumbersServiceTests
    {
        private NumbersToWordsConverterService Service { get; set; }
        [SetUp]
        public void Setup()
        {
            Service = new NumbersToWordsConverterService();
        }

        [Test]
        public void CanServiceConvertNumbersToWords()
        {
            var result1 = Service.Convert("0");
            var result2 = Service.Convert("1");
            var result3 = Service.Convert("25,1");
            var result4 = Service.Convert("0,01");
            var result5 = Service.Convert("45 100");
            var result6 = Service.Convert("999 999 999,99");

            result1.Should().Be("zero dollars");
            result2.Should().Be("one dollar");
            result3.Should().Be("twenty-five dollars and ten cents");
            result4.Should().Be("zero dollars and one cent");
            result5.Should().Be("forty-five thousand one hundred dollars");
            result6.Should().Be("nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents");
        }
    }
}
