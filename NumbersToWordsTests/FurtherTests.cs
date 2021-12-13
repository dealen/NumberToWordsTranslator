using FluentAssertions;
using NumbersToWordsService;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersToWordsTests
{
    public class FurtherTests
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
            var result2 = Service.Convert("1,34");
            var result3 = Service.Convert("12,12");
            var result4 = Service.Convert("111 000");

            result1.Should().Be("zero dollars");
            result2.Should().Be("one dollar and thirty-four cents");
            result3.Should().Be("twelve dollars and twelve cents");
            result4.Should().Be("one hundred eleven thousand dollars");
        }
    }
}
