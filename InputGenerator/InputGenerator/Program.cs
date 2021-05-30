using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //InputGenerator.GenerateInputManual();
            if(args.Length == 0)
                InputGenerator.GenerateInputAuto();
            else
            {
                if (args.Length != 4)
                    return;
                InputGenerator.GenerateInputAuto(args[0], Int32.Parse(args[1]), Int32.Parse(args[2]), Int32.Parse(args[3]));
            }
        }
    }
}
