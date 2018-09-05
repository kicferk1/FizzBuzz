using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz
{
    class Program
    {
        static string valuation1(int i)
        {
            string value = "";
            if (i % 3 == 0)
            {
                value = value + "Fizz";
            }

            if (i % 5 == 0)
            {
                value = value + "Buzz";
            }


            if (value == "")
            {
                value = i.ToString();
            }
            return value;
        }
        static void Main(string[] args)
        {
            for (int i = 1; i <= 100; i++)
            {
                Console.WriteLine(valuation1(i));
            }
            while (true) { }
        }
    }
}
