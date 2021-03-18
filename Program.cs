using System;

namespace AZ_KMP
{
    class Program
    {
        static void Main(string[] args)
        {
            //string text = "Ala ma kota, a kot ma Ale";
            //string pattern = "kota";
            string text = "abcdefg rightleft efgleftpowko";
            string pattern = "efgleft";
            var result = Algorithms.KMP(text, pattern);    
            if(result!= -1)
            {
                Console.WriteLine($"Found pattern at index {result}.");
            }
            else
            {
                Console.WriteLine("Pattern not found.");
            }
            Console.ReadKey();
        }
    }
}
