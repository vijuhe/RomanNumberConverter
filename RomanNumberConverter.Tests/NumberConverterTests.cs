using System;
using NUnit.Framework;

namespace RomanNumberConverter.Tests
{
    public class NumberConverterTests
    {
        private NumberConverter sut;

        [SetUp]
        public void Setup()
        {
            sut = new NumberConverter();
        }

        [Test]
        public void One()
        {
            int result = sut.ToArabicNumber("I");
            Assert.AreEqual(1, result);
        }

        [Test]
        public void OneInLowerCase()
        {
            int result = sut.ToArabicNumber("i");
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Three()
        {
            int result = sut.ToArabicNumber("III");
            Assert.AreEqual(3, result);
        }

        [Test]
        public void ThreeWithSurroundingWhiteSpaces()
        {
            int result = sut.ToArabicNumber(" III ");
            Assert.AreEqual(3, result);
        }

        [Test]
        public void Five()
        {
            int result = sut.ToArabicNumber("V");
            Assert.AreEqual(5, result);
        }

        [Test]
        public void SevenInLowerCase()
        {
            int result = sut.ToArabicNumber("vii");
            Assert.AreEqual(7, result);
        }

        [Test]
        public void Four()
        {
            int result = sut.ToArabicNumber("IV");
            Assert.AreEqual(4, result);
        }

        [Test]
        public void Ten()
        {
            int result = sut.ToArabicNumber("X");
            Assert.AreEqual(10, result);
        }

        [Test]
        public void Nine()
        {
            int result = sut.ToArabicNumber("IX");
            Assert.AreEqual(9, result);
        }

        [Test]
        public void Fourteen()
        {
            int result = sut.ToArabicNumber("XIV");
            Assert.AreEqual(14, result);
        }

        [Test]
        public void Twelve()
        {
            int result = sut.ToArabicNumber("XII");
            Assert.AreEqual(12, result);
        }

        [Test]
        public void Sixteen()
        {
            int result = sut.ToArabicNumber("XVI");
            Assert.AreEqual(16, result);
        }

        [Test]
        public void Nineteen()
        {
            int result = sut.ToArabicNumber("XIX");
            Assert.AreEqual(19, result);
        }

        [Test]
        public void Fifty()
        {
            int result = sut.ToArabicNumber("L");
            Assert.AreEqual(50, result);
        }

        [Test]
        public void Hundred()
        {
            int result = sut.ToArabicNumber("C");
            Assert.AreEqual(100, result);
        }

        [Test]
        public void FiveHundred()
        {
            int result = sut.ToArabicNumber("D");
            Assert.AreEqual(500, result);
        }

        [Test]
        public void Thousand()
        {
            int result = sut.ToArabicNumber("M");
            Assert.AreEqual(1000, result);
        }

        [Test]
        public void LargestNumber()
        {
            int result = sut.ToArabicNumber("MMMCMXCIX");
            Assert.AreEqual(3999, result);
        }

        [Test]
        public void FourConsecutiveSameCharacters()
        {
            Assert.Throws<ArgumentException>(() => sut.ToArabicNumber("XIIII"));
        }

        [Test]
        public void InvalidCharacters()
        {
            Assert.Throws<ArgumentException>(() => sut.ToArabicNumber("IAVY"));
        }

        [Test]
        public void NullInput()
        {
            Assert.Throws<ArgumentNullException>(() => sut.ToArabicNumber(null));
        }

        [Test]
        public void EmptyInput()
        {
            Assert.Throws<ArgumentNullException>(() => sut.ToArabicNumber(string.Empty));
        }
    }
}