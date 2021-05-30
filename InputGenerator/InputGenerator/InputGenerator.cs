using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InputGenerator
{
    public static class InputGenerator
    {
        private static string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static string charactersWithSymbols = characters + ",.;: ";

        public static List<(string, string)> GenerateInputManual()
        {
            var result = new List<(string, string)>();

            string filename;
            Console.WriteLine("Proszę podać ścieżkę do pliku wejściowego (domyślnie input.txt)");
            string input = Console.ReadLine();
            if (input == "")
                filename = "input.txt";
            else
                filename = input;

            string chars;
            var end = "n";
            while (end != "t")
            {
                var charPick = "n";
                while (true)
                {
                    Console.WriteLine("Czy korzystać ze znaków interpunkcyjnych? [t|n]");
                    charPick = Console.ReadLine();
                    if (charPick == "n")
                    {
                        chars = characters;
                        break;
                    }
                    else if (charPick == "t")
                    {
                        chars = charactersWithSymbols;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Niepoprawne dane - znaki interpunkcyjne");
                        continue;
                    }
                }

                string pattern;
                while (true)
                {
                    Console.WriteLine("Proszę podać wzorzec:");
                    pattern = Console.ReadLine();

                    foreach (var c in pattern)
                    {
                        bool patternCheck = true;
                        if (!chars.Contains($"{c}"))
                        {
                            Console.WriteLine("Niepoprawne dane - wzorzec");
                            patternCheck = false;
                        }
                        if (patternCheck)
                            break;
                        else
                            continue;
                    }
                    break;
                }

                string textRand = "n";
                string text = "";
                while (true)
                {
                    Console.WriteLine("Czy wygenerować tekst? [t - generacja tekstu | n - tekst podany ręcznie");
                    textRand = Console.ReadLine();
                    if (textRand == "t")
                    {
                        bool lengthCheck = false;
                        int length = 0;
                        while (!lengthCheck)
                        {
                            Console.WriteLine("Ile ma być znaków w tworzonym tekście?");
                            lengthCheck = Int32.TryParse(Console.ReadLine(), out length);
                            if (!lengthCheck || length < pattern.Length)
                                Console.WriteLine("Niepoprawne dane - długość tekstu");
                        }

                        bool repeatCheck = false;
                        int repeats = 0;
                        while (!repeatCheck)
                        {
                            Console.WriteLine("Ile razy ma wystąpić wzorzec w tekście?");
                            repeatCheck = Int32.TryParse(Console.ReadLine(), out repeats);
                            if (!repeatCheck)
                                Console.WriteLine("Niepoprawne dane - wystąpienia");
                        }

                        bool instanceCheck = false;
                        int instances = 0;
                        while (!instanceCheck)
                        {
                            Console.WriteLine("Ile instancji wygenerować?");
                            instanceCheck = Int32.TryParse(Console.ReadLine(), out instances);
                            if (!instanceCheck)
                                Console.WriteLine("Niepoprawne dane - instancje");
                        }

                        int pattLength = pattern.Length;
                        int wordLength = length - repeats * pattLength;

                        var stringChars = new char[wordLength];
                        var random = new Random();

                        for (int i = 0; i < instances; i++)
                        {
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
                                insertLocations[j] += pattLength;

                            foreach (var ins in insertLocations)
                                finalString = finalString.Insert(ins, pattern);

                            text = finalString;

                            result.Add((text, pattern));
                        }
                        break;
                    }
                    else if (textRand == "n")
                    {
                        Console.WriteLine("Proszę podać tekst do znalezienia wzorca:");
                        text = Console.ReadLine();
                        result.Add((text, pattern));
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Niepoprawne dane - generacja tekstu");
                        continue;
                    }
                }

                while (true)
                {
                    Console.WriteLine("Czy zakończyć generowanie? [t|n]");
                    end = Console.ReadLine();
                    if (end == "t" || end == "n")
                        break;
                    else
                    {
                        Console.WriteLine("Niepoprawne dane - koniec generacji");
                        continue;
                    }
                }
            }

            using (StreamWriter sw = File.CreateText(filename))
            {
                foreach (var res in result)
                    sw.WriteLine($"{res.Item1}:{res.Item2}");
            }

            return result;
        }

        public static List<(string, string)> GenerateInputAuto()
        {
            var result = new List<(string, string)>();

            string filename, fileout;
            Console.WriteLine("Proszę podać ścieżkę do pliku wejściowego (domyślnie generatorInput.txt)");
            string input = Console.ReadLine();
            if (input == "")
                filename = "generatorInput.txt";
            else
                filename = input;
            Console.WriteLine("Proszę podać ścieżkę do pliku wyjściowego (domyślnie generatorOutput.txt)");
            string output = Console.ReadLine();
            if (output == "")
                fileout = "generatorOutput.txt";
            else
                fileout = output;

            var chars = charactersWithSymbols;

            using (StreamReader sr = File.OpenText(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var split = line.Split(':');

                    if (split.Length != 4)
                    {
                        Console.WriteLine("Niepoprawne dane - zła ilość argumentów");
                        Console.Read();
                        return null;
                    }

                    var pattern = split[0];
                    var repeats = Int32.Parse(split[1]);
                    var length = Int32.Parse(split[2]);
                    var instances = Int32.Parse(split[3]);

                    if(pattern.Length > length)
                    {
                        Console.WriteLine("Niepoprawne dane - wzorzec dłuższy niż tekst");
                        Console.Read();
                        return null;
                    }

                    string text = string.Empty;

                    int pattLength = pattern.Length;
                    int wordLength = length - repeats * pattLength;

                    var stringChars = new char[wordLength];
                    var random = new Random();

                    for (int i = 0; i < instances; i++)
                    {
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
                            insertLocations[j] += pattLength;

                        foreach (var ins in insertLocations)
                            finalString = finalString.Insert(ins, pattern);

                        text = finalString;

                        result.Add((text, pattern));
                    }
                }
            }

            using (StreamWriter sw = File.CreateText(fileout))
            {
                foreach (var res in result)
                    sw.WriteLine($"{res.Item1}:{res.Item2}");
            }

            return result;
        }

        public static List<(string, string)> GenerateInputAuto(string pattern, int repeats, int length, int instances)
        {
            if (pattern.Length > length)
            {
                Console.WriteLine("Niepoprawne dane - wzorzec dłuższy niż tekst");
                Console.Read();
                return null;
            }

            var result = new List<(string, string)>();
            var chars = charactersWithSymbols;

            string text = string.Empty;

            int pattLength = pattern.Length;
            int wordLength = length - repeats * pattLength;

            var stringChars = new char[wordLength];
            var random = new Random();

            for (int i = 0; i < instances; i++)
            {
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
                    insertLocations[j] += pattLength;

                foreach (var ins in insertLocations)
                    finalString = finalString.Insert(ins, pattern);

                text = finalString;

                result.Add((text, pattern));
            }

            var filename = "generatorOutput.txt";

            using (StreamWriter sw = File.CreateText(filename))
            {
                foreach (var res in result)
                    sw.WriteLine($"{res.Item1}:{res.Item2}");
            }

            return result;
        }
    }
}
