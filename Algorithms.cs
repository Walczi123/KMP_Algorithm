using System;
using System.Collections.Generic;

namespace AZ_KMP
{
    static class Algorithms
    {
        //private static int[] ComputeTable(string pattern)
        //{
        //    int n = pattern.Length, m = 0, i = 1;
        //    int[] kmpTable = new int[n];
        //    while(i < n)
        //    {
        //        if(pattern[i] == pattern[m])
        //        {
        //            kmpTable[i] = m + 1;
        //            m++;
        //            i++;
        //        }
        //        else
        //        {
        //            if (m != 0)
        //            {
        //                m = kmpTable[m - 1];
        //            }
        //            else
        //            {
        //                kmpTable[i] = m;
        //                i++;
        //            }
        //        }
        //    }
        //    System.Console.WriteLine("Tabela kmp:");
        //    foreach(var val in kmpTable)
        //    {
        //        Console.Write(val + " ");
        //    }
        //    Console.Write("\n");
        //    return kmpTable;
        //}

        //public static List<int> KMP(string text, string pattern)
        //{
        //    int n = text.Length, m = pattern.Length;
        //    int[] kmpTable = ComputeTable(pattern);
        //    List<int> results = new List<int>();
        //    int i = 0, j = 0;
        //    while (i < n)
        //    {
        //        if (pattern[j] == text[i])
        //        {
        //            j++;
        //            i++;
        //        }
        //        if (j == m)
        //        {
        //            //Console.WriteLine("Found pattern "
        //            //              + "at index " + (i - j));
        //            //j = kmpTable[j - 1];
        //            results.Add(i - j);
        //            i = i + 1;
        //            j = 0;
        //        }
        //        else if (i < n && pattern[j] != text[i])
        //        {
        //            if (j != 0)
        //            {
        //                j = kmpTable[j - 1];
        //            }
        //            else
        //            {
        //                i = i + 1;
        //            }
        //        }
        //    }
        //    return results;
        //}

        private static int[] ComputeTable(string pattern, ref int comparisons)
        {
            int m = pattern.Length, k = 0;
            int[] kmpTable = new int[m];
            kmpTable[0] = 0;
            for(int q=1; q<m; q++)
            {
                comparisons++;
                while (k > 0 && pattern[k] != pattern[q] && ++comparisons != 0)
                {
                    comparisons++;
                    k = kmpTable[k];
                }
                   

                if (pattern[k] == pattern[q] && ++comparisons != 0)
                    k = k + 1;

                kmpTable[q] = k;
            }
            Console.WriteLine("Tabela kmp:");
            foreach (var val in kmpTable)
            {
                Console.Write(val + " ");
            }
            Console.Write("\n");
            return kmpTable;
        }

        public static (List<int>, int) KMP(string text, string pattern)
        {
            int comparisons = 0;
            int n = text.Length, m = pattern.Length;
            int[] kmpTable = ComputeTable(pattern, ref comparisons);
            List<int> results = new List<int>();
            int q = 0;
            for (int i = 0; i < n; i++)
            {
                while (q > 0 && pattern[q] != text[i] && ++comparisons != 0)
                    q = kmpTable[q-1];

                if (pattern[q] == text[i] && ++comparisons != 0)
                    q = q + 1;

                if (q == m)
                {
                    results.Add(i - m + 1);
                    q = kmpTable[q-1];
                }
            }
            return (results, comparisons);
        }

        public static (List<int>, int) NaiveAglorithm(string text, string pattern)
        {
            int comparisons = 0;
            int n = text.Length, m = pattern.Length, j;
            bool pattern_match;
            List<int> results = new List<int>();
            for (int i = 0; i <= n - m; i++)
            {
                pattern_match = true;

                for (j = 0; j < m; j++)
                {
                    if (text[j + i] != pattern[j] && ++comparisons != 0)
                    {
                        pattern_match = false;
                        break;
                    }
                }

                if (pattern_match)
                {
                    results.Add(i);
                }
            }
            return (results, comparisons);
        }
    }
}
