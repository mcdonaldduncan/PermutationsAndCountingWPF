using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermutationsAndCountingWPF.Services
{
    /// <summary>
    /// Used for common counting and calculations
    /// </summary>
    internal class CountingServices
    {
        Random random = new Random();

        /// <summary>
        /// Given inputLength and limit, calculates permutation count assuming a set of unique elements.
        /// Equation from class/pp :  n! / (n-r)!
        /// </summary>
        /// <param name="inputLength"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int PermutationCount(int inputLength, int limit)
        {
            return Factorial(inputLength) / Factorial(inputLength - limit);
        }

        /// <summary>
        /// Given character string, calculates ordered partition count
        /// Equation from class/pp : n! / ( r1!*r2!....*rk!) 
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        public int OrderedPartitionCount(string characters)
        {
            return Factorial(characters.Length) / OrderedPartitionDenominator(CommonalityCounts(characters));
        }

        /// <summary>
        /// Given inputLength and limit, calculates combnation count assuming a set of unique elements.
        /// Equation from class/pp : n! / r!(n−r)!
        /// </summary>
        /// <param name="inputLength"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int CombinationCount(int inputLength, int limit)
        {
            return Factorial(inputLength) / (Factorial(inputLength - limit) * Factorial(limit));
        }

        /// <summary>
        /// Generate a dictionary used to evaluate overlap of combinations
        /// given the small sample size, I believe it is almost impossible to get values assigned that cause issues
        /// but theoretically if the assigned values add up to the same total between more than a single pair of values
        /// this would be innacurate
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        public Dictionary<char, int> GenerateLookupDict(string characters)
        {
            Dictionary<char, int> lookupDict = new Dictionary<char, int>();

            for (int i = 0; i < characters.Length; i++)
            {
                if (!lookupDict.ContainsKey(characters[i]))
                {
                    lookupDict.Add(characters[i], i * random.Next());
                }
            }

            return lookupDict;
        }

        /// <summary>
        /// Uses a previously generated lookup dictionary to calculate a "score" for a given string
        /// </summary>
        /// <param name="permutation"></param>
        /// <param name="lookupDict"></param>
        /// <returns></returns>
        public int CalculatePermScore(string permutation, Dictionary<char, int> lookupDict)
        {
            int temp = 0;

            foreach (var ch in permutation)
            {
                temp += lookupDict[ch];
            }

            return temp;
        }

        /// <summary>
        /// Generates a dictionary of all characters in a given string, incrementing 
        /// the value of previously found characters to create a dictionary to help calculate the expected 
        /// Ordered partition count
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Dictionary<char, int> CommonalityCounts(string input)
        {
            Dictionary<char, int> commonalities = new Dictionary<char, int>();

            foreach (var ch in input)
            {
                if (commonalities.ContainsKey(ch))
                {
                    commonalities[ch]++;
                }
                else
                {
                    commonalities.Add(ch, 1);
                }
            }

            return commonalities;
        }

        /// <summary>
        /// Uses a previously generated dictionary of commonalities to calculate the denominator
        /// of the equation for ordered partitions r1!*r2!...*rk!
        /// </summary>
        /// <param name="commonalities"></param>
        /// <returns></returns>
        int OrderedPartitionDenominator(Dictionary<char, int> commonalities)
        {
            int temp = 1;
            foreach (var commonality in commonalities)
            {
                temp *= Factorial(commonality.Value);
            }
            return temp;
        }

        /// <summary>
        /// Calculates factorial of n by recursively multiplying n * Factorial(n - 1) until 0
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        int Factorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }

            return n * Factorial(n - 1);
        }

    }
}
