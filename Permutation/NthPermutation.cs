using System.Collections.Generic;

namespace Permutation
{
    public class NthPermutation
    {
        #region Code Related to Factorial Generation
        private List<long> _factList = new List<long>();

        private void ConfigureFactorialList(int n)
        {
            if (_factList.Count >= n)
                return;

            if (_factList.Count == 0)
                _factList.Add(1);

            do
            {
                _factList.Add(_factList[_factList.Count - 1] * (_factList.Count + 1));
            }
            while (_factList.Count < n);
        }

        private long GetFactorial(int n)
        {
            if (n <= 0) return 1;

            if (_factList.Count <= n)
                this.ConfigureFactorialList(n);

            return _factList[n - 1];
        }

        private long GetFactorialForDenominator(char[] s, int start, int exceptPosition)
        {
            Dictionary<char, int> d = new Dictionary<char, int>();

            for (int i = start; i < s.Length; i++)
            {
                if (i == exceptPosition)
                    continue;

                char c = s[i];

                if (d.ContainsKey(c))
                    d[c] = d[c] + 1;
                else
                    d[c] = 1;
            }

            long factorialDenominator = 1;

            foreach (char c in d.Keys)
            {
                if (d[c] != 1)
                    factorialDenominator *= this.GetFactorial(d[c]);
            }

            return factorialDenominator;
        }

        #endregion Code Related to Factorial Generation

        #region Sorting Relatd
        private void swap<T>(ref T a, ref T b) { T t = a; a = b; b = t; }

        private void SortCharArray(char[] s, int startFrom = 0)
        {
            for (int i = startFrom; i < s.Length; i++)
            {
                for (int j = i + 1; j < s.Length; j++)
                {
                    if (s[i] > s[j])
                        swap(ref s[i], ref s[j]);
                }
            }
        }
        #endregion Sorting Relatd

        public string GetNthPermutation(string str, long k)
        {
            char[] s = str.ToCharArray();

            int n = str.Length;

            this.SortCharArray(s);

            this.ConfigureFactorialList(n);

            string partialString = str;

            long k_ZeroBasedIndex = k - 1;

            long temp_k = k_ZeroBasedIndex;

            for (int i = 0; i < n; i++)
            {
                long curBlockSize = 0;

                int indexOfChar = this.GetRelevantCharIndex(s, i, temp_k, ref curBlockSize);

                temp_k = temp_k - curBlockSize;

                this.ShiftCharToRight(s, i, i + indexOfChar);

                //partialString = new string(s);//For Debug
            }

            partialString = new string(s);

            return partialString;
        }

        private void ShiftCharToRight(char[] s, int startPos, int endPos)
        {
            if (startPos == endPos)
                return;

            char bakChar = s[endPos];

            int i = endPos;
            while (i > startPos && i <= endPos)
            {
                s[i] = s[i - 1];
                i--;
            }

            s[startPos] = bakChar;
        }

        private int GetRelevantCharIndex(char[] s, int startIndex_ZeroBased, long k_ZeroBasedIndex, ref long totalItemToSkip)
        {
            int actualN = s.Length;

            int currentN = s.Length - startIndex_ZeroBased;

            HashSet<char> processedCharList = new HashSet<char>();
            //This will tell how much elements to skip
            long[] skipList = new long[currentN];

            for (int i = startIndex_ZeroBased; i < actualN; i++)
            {
                char curChar = s[i];

                if (processedCharList.Contains(curChar))
                {
                    skipList[i - startIndex_ZeroBased] = 0;
                    continue;
                }
                processedCharList.Add(curChar);

                long den = this.GetFactorialForDenominator(s, startIndex_ZeroBased, i);

                skipList[i - startIndex_ZeroBased] = (long)(this.GetFactorial(currentN - 1) / den);
            }

            long[] cumulativeTotal = new long[currentN + 1];

            cumulativeTotal[0] = 0;

            int lexicalBlk_ZeroBasedIndex = 0;

            for (int i = 1; i < currentN + 1; i++)
            {
                cumulativeTotal[i] = cumulativeTotal[i - 1] + skipList[i - 1];

                if (k_ZeroBasedIndex >= cumulativeTotal[i - 1]
                    && k_ZeroBasedIndex < cumulativeTotal[i])
                {
                    lexicalBlk_ZeroBasedIndex = i - 1;
                    break;
                }
            }

            totalItemToSkip = cumulativeTotal[lexicalBlk_ZeroBasedIndex];

            return lexicalBlk_ZeroBasedIndex;
        }
    }
}
