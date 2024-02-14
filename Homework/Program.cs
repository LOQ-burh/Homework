using System;
namespace _BY_LOQ_BURH
{
    class ApproachByUsingBadCharacterHeuristic
    {
        static int maxCharacters = 256;
        static void badCharacter(string p, int n, int[] tablebadCharacter)
        {
            for (int i = 0; i < maxCharacters; i++)
                tablebadCharacter[i] = -1;
            for (int i = 0; i < n; i++)
                tablebadCharacter[(int)p[i]] = i;
        }
        public static void BM_matching(string text, string pattern)
        {
            int m = pattern.Length;
            int n = text.Length;
            int[] badChars = new int[maxCharacters];

            badCharacter(pattern, m, badChars);

            int shift = 0;
            while (shift <= n - m)
            {
                int j = m - 1;
                while (j >= 0 && pattern[j] == text[shift + j])
                {
                    j--;
                }
                if (j < 0)
                {
                    Console.WriteLine("Patterns occur at shift = " + shift);
                    //if ((shift + m < n))
                    //    shift += m - badChars[text[shift + m]];
                    //else
                    //    shift += 1;
                    shift += (shift + m < n) ? m - badChars[text[shift + m]] : 1;
                }
                else
                {
                    shift += Math.Max(1, j - badChars[text[shift + j]]);
                }
            }
        }
    }
    class ApproachByUsingGoodSuffixHeuristic
    {
        //handle the suffix case.
        static void suffixCase(int[] shift, int[] borderPosition, string pattern, int m)
        {
            int i = m, j = m + 1;
            borderPosition[i] = j;
            while (i > 0)
            {
                while (j <= m && pattern[i - 1] != pattern[j - 1])
                {
                    if (shift[j] == 0)
                    {
                        shift[j] = j - i;
                    }
                    j = borderPosition[j];
                }
                i--;
                j--;
                borderPosition[i] = j;
            }
        }
        //handle the prefix case.
        static void prefixCase(int[] shift, int[] borderPosition, string pattern, int m)
        {
            int i, j = borderPosition[0];
            for (i = 0; i <= m; i++)
            {
                if (shift[i] == 0)
                {
                    shift[i] = j;
                }
                if (i == j)
                {
                    j = borderPosition[j];
                }
            }
        }
        public static void BM_matching(string text, string pattern)
        {
            int j, shift = 0;
            int m = pattern.Length;
            int n = text.Length;

            int[] borderPosition = new int[m + 1];
            int[] toStoreShift = new int[m + 1];

            for (int i = 0; i < m + 1; i++)
            {
                toStoreShift[i] = 0;
            }

            suffixCase(toStoreShift, borderPosition, pattern, m);
            prefixCase(toStoreShift, borderPosition, pattern, m);

            while (shift <= n - m)
            {
                j = m - 1;
                while (j >= 0 && pattern[j] == text[shift + j])
                {
                    j--;
                }
                if (j < 0)
                {
                    Console.Write("Pattern occurs at position {0}\n", shift);
                    //move pattern to the right by the maximum shift.
                    shift += toStoreShift[0];
                }
                else
                {
                    shift += toStoreShift[j + 1];
                }
            }
        }
    }
    class program
    {
        static void Main(string[] args)
        {
            string text = "Scaler Topics";
            string pattern = "Topics";
            //string text = "0123456789";
            //string pattern = "789";
            ApproachByUsingBadCharacterHeuristic.BM_matching(text, pattern);
            ApproachByUsingGoodSuffixHeuristic.BM_matching(text, pattern);
            Console.ReadKey();
        }
    }
}
