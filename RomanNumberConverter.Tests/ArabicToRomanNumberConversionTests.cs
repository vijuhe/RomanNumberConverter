using System;
using NUnit.Framework;

namespace RomanNumberConverter.Tests
{
    public class ArabicToRomanNumberConversionTests
    {
        private NumberConverter sut;

        [SetUp]
        public void Setup()
        {
            sut = new NumberConverter();
        }

        [Test]
        public void Zero()
        {
            Assert.That(sut.ToRomanNumber(0), Is.Empty);
        }

        [Test]
        public void TooBigNumber()
        {
            Assert.Throws<ArgumentException>(() => sut.ToRomanNumber(4000));
        }

        [Test]
        public void One()
        {
            Assert.That(sut.ToRomanNumber(1), Is.EqualTo("I"));
        }

        [Test]
        public void Five()
        {
            Assert.That(sut.ToRomanNumber(5), Is.EqualTo("V"));
        }

        [Test]
        public void Two()
        {
            Assert.That(sut.ToRomanNumber(2), Is.EqualTo("II"));
        }

        [Test]
        public void Three()
        {
            Assert.That(sut.ToRomanNumber(3), Is.EqualTo("III"));
        }

        [Test]
        public void Twenty()
        {
            Assert.That(sut.ToRomanNumber(20), Is.EqualTo("XX"));
        }

        [Test]
        public void ManyDifferentRomanCharacters()
        {
            Assert.That(sut.ToRomanNumber(132), Is.EqualTo("CXXXII"));
        }

        [Test]
        public void Four()
        {
            Assert.That(sut.ToRomanNumber(4), Is.EqualTo("IV"));
        }

        [Test]
        public void Fourty()
        {
            Assert.That(sut.ToRomanNumber(40), Is.EqualTo("XL"));
        }

        [Test]
        public void SubtractedAndAddedPartsTogether()
        {
            Assert.That(sut.ToRomanNumber(92), Is.EqualTo("XCII"));
        }

        [Test]
        public void LargestNumber()
        {
            Assert.That(sut.ToRomanNumber(3999), Is.EqualTo("MMMCMXCIX"));
        }
    }
}
