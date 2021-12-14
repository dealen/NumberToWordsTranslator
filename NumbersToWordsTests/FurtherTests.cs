﻿using FluentAssertions;
using NumbersToWordsService;
using NUnit.Framework;

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
            var result5 = Service.Convert("3253254");
            var result6 = Service.Convert("213332334,12");

            result1.Should().Be("zero dollars");
            result2.Should().Be("one dollar and thirty-four cents");
            result3.Should().Be("twelve dollars and twelve cents");
            result4.Should().Be("one hundred eleven thousand dollars");
            result5.Should().Be("three million two hundred fifty-three thousand two hundred fifty-four dollars");
            result6.Should().Be("two hundred thirteen million three hundred thirty-two thousand three hundred thirty-four dollars and twelve cents");
        }
    }
}
