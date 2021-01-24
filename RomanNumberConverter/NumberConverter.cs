using System;
using System.Collections.Generic;
using System.Linq;

namespace RomanNumberConverter
{
    public class NumberConverter
    {
        private readonly IDictionary<char, ushort> romanNumberCharacterDictionary;
        private readonly IDictionary<ushort, string> numbersToRomanNumberCharacter;
        private readonly IList<char> romanNumberCharacters;

        public NumberConverter()
        {
            romanNumberCharacterDictionary = new Dictionary<char, ushort>
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 }
            };
            romanNumberCharacters = romanNumberCharacterDictionary.Keys.ToList();
            numbersToRomanNumberCharacter = romanNumberCharacterDictionary
                .ToDictionary(keyValuePair => keyValuePair.Value, keyValuePair => keyValuePair.Key.ToString());
        }

        public string ToRomanNumber(ushort arabicNumber)
        {
            if (arabicNumber > 3999)
            {
                throw new ArgumentException("Too large arabic number for a roman number.");
            }

            if (arabicNumber == 0)
            {
                return string.Empty;
            }

            if (numbersToRomanNumberCharacter.ContainsKey(arabicNumber))
            {
                return numbersToRomanNumberCharacter[arabicNumber];
            }

            ushort remainingArabicNumber;
            if (IsBiggerThanAnyRomanNumberCharacter(arabicNumber))
            {
                ushort largestSingleCharacterNumber = numbersToRomanNumberCharacter.Keys.Max();
                remainingArabicNumber = Convert.ToUInt16(arabicNumber - largestSingleCharacterNumber);
                return numbersToRomanNumberCharacter[largestSingleCharacterNumber] + ToRomanNumber(remainingArabicNumber);
            }

            ushort smallestBiggerNumber = numbersToRomanNumberCharacter.Keys.Where(num => num > arabicNumber).Min();
            remainingArabicNumber = Convert.ToUInt16(smallestBiggerNumber - arabicNumber);
            if (IsSubtractionNeeded(arabicNumber, remainingArabicNumber))
            {
                ushort deducted = numbersToRomanNumberCharacter.Keys
                    .Where(num => num < smallestBiggerNumber && num.ToString().StartsWith("1")).Max();
                remainingArabicNumber = Convert.ToUInt16(arabicNumber - (smallestBiggerNumber - deducted));
                return numbersToRomanNumberCharacter[deducted] + 
                       numbersToRomanNumberCharacter[smallestBiggerNumber] + 
                       ToRomanNumber(remainingArabicNumber);
            }

            ushort biggestSmallerNumber = numbersToRomanNumberCharacter.Keys.Where(num => num < arabicNumber).Max();
            remainingArabicNumber = Convert.ToUInt16(arabicNumber - biggestSmallerNumber);
            return numbersToRomanNumberCharacter[biggestSmallerNumber] + ToRomanNumber(remainingArabicNumber);
        }

        public int ToArabicNumber(string romanNumber)
        {
            if (string.IsNullOrWhiteSpace(romanNumber))
            {
                throw new ArgumentNullException(nameof(romanNumber));
            }

            string normalizedRomanNumber = romanNumber.ToUpper().Trim();
            int result = 0;
            byte consecutiveSameCharacterCount = 0;
            char? previousCharacter = null;
            foreach (char c in normalizedRomanNumber)
            {
                if (!romanNumberCharacterDictionary.ContainsKey(c))
                {
                    throw new ArgumentException($"Roman number contained an invalid character '{c}'.");
                }

                if (previousCharacter == c)
                {
                    consecutiveSameCharacterCount++;
                }
                else
                {
                    consecutiveSameCharacterCount = 1;
                }

                if (consecutiveSameCharacterCount == 4)
                {
                    throw new ArgumentException($"There were four characters '{c}' in a row which is not allowed.");
                }

                if (SubtractPreviousCharacter(previousCharacter, c))
                {
                    result -= romanNumberCharacterDictionary[previousCharacter.Value] * 2;
                }

                result += romanNumberCharacterDictionary[c];
                previousCharacter = c;
            }

            return result;
        }

        private bool IsBiggerThanAnyRomanNumberCharacter(ushort arabicNumber)
        {
            return !numbersToRomanNumberCharacter.Keys.Any(num => num > arabicNumber);
        }

        private static bool IsSubtractionNeeded(ushort arabicNumber, ushort remainingArabicNumber)
        {
            return remainingArabicNumber <= Math.Pow(10, GetNumberOfDigits(arabicNumber) - 1);
        }

        private static int GetNumberOfDigits(ushort arabicNumber)
        {
            return arabicNumber.ToString().Length;
        }
        
        private bool SubtractPreviousCharacter(char? previousCharacter, char currentCharacter)
        {
            return previousCharacter.HasValue && 
                   romanNumberCharacters.IndexOf(previousCharacter.Value) < romanNumberCharacters.IndexOf(currentCharacter);
        }
    }
}
