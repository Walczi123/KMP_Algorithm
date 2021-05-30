using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputGeneratorAuto
{
    public static class InputGeneratorAuto
    {
        private static string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static string charactersWithSymbols = characters + ",.; ";
        public static List<(string, string, int)> GenerateInputAuto(int iterations)
        {
            var result = new List<(string, string, int)>();

            string fileout;
            Console.WriteLine("Proszę podać ścieżkę do pliku wyjściowego (domyślnie generatorOutput.txt)");
            string output = Console.ReadLine();
            if (output == "")
                fileout = "generatorOutput.txt";
            else
                fileout = output;

            if(iterations == 0)
            {
                bool iterCheck = false;
                while(!iterCheck)
                {
                    Console.WriteLine("Ile instancji wygenerować?");
                    iterCheck = Int32.TryParse(Console.ReadLine(), out iterations);
                    if (!iterCheck)
                        Console.WriteLine("Niepoprawne dane - ilość iteracji");
                }
            }

            var chars = charactersWithSymbols;
            var random = new Random();

            for (int i = 0; i < iterations; i++)
            {
                string pattern;
                int repeats, length;

                var pattLength = random.Next(5, 16);
                var pattChars = new char[pattLength];
                for (int j = 0; j < pattLength; j++)
                    pattChars[j] = chars[random.Next(chars.Length)];
                pattern = new String(pattChars);

                repeats = random.Next(2, 5);
                length = random.Next(5, 11) * pattLength;

                if (pattern.Length > length)
                {
                    Console.WriteLine("Niepoprawne dane - wzorzec dłuższy niż tekst");
                    Console.Read();
                    return null;
                }

                string text = string.Empty;
                int wordLength = length - repeats * pattLength;
                var stringChars = new char[wordLength];

                for (int j = 0; j < stringChars.Length; j++)
                {
                    stringChars[j] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);

                List<int> insertLocations = new List<int>();
                for (int j = 0; j < repeats; j++)
                    insertLocations.Add(random.Next(finalString.Length));

                insertLocations.Sort();
                for (int j = 1; j < repeats; j++)
                    insertLocations[j] += j * pattLength;

                foreach (var ins in insertLocations)
                    finalString = finalString.Insert(ins, pattern);

                text = finalString;

                result.Add((text, pattern, repeats));
            }

            using (StreamWriter sw = File.CreateText(fileout))
            {
                foreach (var res in result)
                    sw.WriteLine($"{res.Item1}:{res.Item2}:{res.Item3}");
            }

            return result;
        }
    }
}
