using System;
using System.Collections.Generic;

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
            System.Console.WriteLine("Tabela kmp:");
            foreach(var val in kmpTable)
            {
                Console.Write(val + " ");
            }
            Console.Write("\n");
            return kmpTable;
        }

        public static List<int> KMP(string text, string pattern)
        {
            int n = text.Length, m = pattern.Length;
            int[] kmpTable = ComputeTable(pattern);
            List<int> results = new List<int>();
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
                    results.Add(i - j);
                    i = i + 1;
                    j = 0;
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
            return results;
        }
    }
}
