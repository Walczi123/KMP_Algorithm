using System;

namespace AZ_KMP
{
    class Program
    {
        static void Main(string[] args)
        {
            //string text = "Ala ma kota, a kot ma Ale";
            //string pattern = "kota";
            string text = "blablacblblacblacblcablacblc";
            string pattern = "blacblc";
            var result = Algorithms.KMP(text, pattern);
            if (result != null && result.Count>0)
            {
                foreach(var res in result)
                    Console.WriteLine($"Found pattern at index {res}.");
            }
            else
            {
                Console.WriteLine("Pattern not found.");
            }
            Console.ReadKey();
        }
    }
}
