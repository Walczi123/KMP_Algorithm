using System;
using System.Collections.Generic;
using System.IO;

namespace AZ_KMP
{
    public static class ReadWrite
    {
        public static List<(string, string, int)> ReadFile()
        {
            string filename;
            Console.WriteLine("Proszę podać plik wejściowy (domyślnie input.txt)");
            string input = Console.ReadLine();
            if (input == "")
                filename = "input.txt";
            else
                filename = input;

            List<(string, string, int)> results = new List<(string, string, int)>();

            using(StreamReader sr = File.OpenText(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var split = line.Split(':');
                    if(split.Length == 3)
                        results.Add((split[0], split[1], Int32.Parse(split[2])));
                    else if (split.Length == 2)
                        results.Add((split[0], split[1], -1));
                }
            }

            return results;
        }

        public static void WriteFile(List<(List<int>, int)> results)
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
                    foreach (var res in instance.Item1)
                        sw.Write($"{res}, ");
                    sw.WriteLine();
                }
            }

        }

        public static List<(string, string, int)> ReadFile(string filePath)
        {
            List<(string, string, int)> results = new List<(string, string, int)>();

            using (StreamReader sr = File.OpenText(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var split = line.Split(':');
                    if(split.Length == 3)
                        results.Add((split[0], split[1], Int32.Parse(split[2])));
                    else if (split.Length == 2)
                        results.Add((split[0], split[1], -1));
                }
            }

            return results;
        }
    }
}
