using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputGeneratorAuto
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                InputGeneratorAuto.GenerateInputAuto(0);
            else
                InputGeneratorAuto.GenerateInputAuto(Int32.Parse(args[0]));
        }
    }
}
