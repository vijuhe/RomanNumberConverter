using System;
using System.Collections.Generic;
using System.Linq;

namespace RomanNumberConverter
{
    public class NumberConverter
    {
        private readonly IDictionary<char, ushort> romanNumberCharacterDictionary;
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

        private bool SubtractPreviousCharacter(char? previousCharacter, char currentCharacter)
        {
            return previousCharacter.HasValue && 
                   romanNumberCharacters.IndexOf(previousCharacter.Value) < romanNumberCharacters.IndexOf(currentCharacter);
        }
    }
}
