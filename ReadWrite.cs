using System;
using System.Collections.Generic;
using System.IO;

namespace AZ_KMP
{
    public static class ReadWrite
    {
        public static List<(string, string)> ReadFile()
        {
            string filename;
            Console.WriteLine("Proszę podać plik wejściowy (domyślnie input.txt)");
            string input = Console.ReadLine();
            if (input == "")
                filename = "input.txt";
            else
                filename = input;

            List<(string, string)> results = new List<(string, string)>();

            using(StreamReader sr = File.OpenText(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var split = line.Split(':');
                    results.Add((split[0], split[1]));
                }
            }

            return results;
        }

        public static void WriteFile(List<List<int>> results)
        {
            string filename;
            Console.WriteLine("Proszę podać plik wyjściowy (domyślnie output.txt)");
            string output = Console.ReadLine();
            if (output == "")
                filename = "output.txt";
            else
                filename = output;

            using (StreamWriter sw = File.CreateText(filename))
            {
                int count = 0;
                foreach (var instance in results)
                {
                    sw.Write($"{++count}. ");
                    foreach (var res in instance)
                        sw.Write($"{res}, ");
                    sw.WriteLine();
                }
            }

        }
    }
}
