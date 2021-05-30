using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InputGenerator
{
    public static class InputGeneratorManual
    {
        private static string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static string charactersWithSymbols = characters + ",.;?! ";

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

            var pattRandom = new Random();
            var random = new Random();

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
                    var autoPattern = "n";
                    while(true)
                    {
                        Console.WriteLine("Czy wygenerować wzorzec automatycznie? [t|n]");
                        autoPattern = Console.ReadLine();
                        if (autoPattern == "t" || autoPattern == "n")
                            break;
                        else
                        {
                            Console.WriteLine("Niepoprawne dane - automatyczny wzorzec");
                            continue;
                        }
                    }

                    if(autoPattern == "n")
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
                    else if (autoPattern == "t")
                    {
                        var pattLength = pattRandom.Next(5, 16);
                        var stringChars = new char[pattLength];
                        for (int i = 0; i < pattLength; i++)
                            stringChars[i] = chars[pattRandom.Next(chars.Length)];
                        pattern = new String(stringChars);
                        break;
                    }
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
                                insertLocations[j] += j * pattLength;

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
    }
}
