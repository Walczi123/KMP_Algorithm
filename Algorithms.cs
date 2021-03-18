using System;

namespace AZ_KMP
{
    static class Algorithms
    {
        private static int[] ComputeTable(string pattern)
        {
            int n = pattern.Length, m = 0, i = 1;
            int[] kmpTable = new int[n];
            while(i < n)
            {
                if(pattern[i] == pattern[m])
                {
                    kmpTable[i] = m + 1;
                    m++;
                    i++;
                }
                else
                {
                    if (m != 0)
                    {
                        m = kmpTable[m - 1];
                    }
                    else
                    {
                        kmpTable[i] = m;
                        i++;
                    }
                }
            }
            return kmpTable;
        }

        public static int KMP(string text, string pattern)
        {
            int n = text.Length, m = pattern.Length;
            int[] kmpTable = ComputeTable(pattern);
            int i = 0, j = 0;
            while (i < n)
            {
                if (pattern[j] == text[i])
                {
                    j++;
                    i++;
                }
                if (j == m)
                {
                    //Console.WriteLine("Found pattern "
                    //              + "at index " + (i - j));
                    //j = kmpTable[j - 1];
                    return (i - j);
                }
                else if (i < n && pattern[j] != text[i])
                {
                    if (j != 0)
                    {
                        j = kmpTable[j - 1];
                    }
                    else
                    {
                        i = i + 1;
                    }
                }
            }
            return -1;
        }
    }
}
