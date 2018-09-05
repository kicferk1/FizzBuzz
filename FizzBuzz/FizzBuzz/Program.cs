using System;
using System.Collections.Generic;
using System.Linq;

namespace FizzBuzz
{
    class Program
    {
        static string Valuation2(int i)
        {
            var result = new List<string>();

            if (i % 3 == 0)
            {
                result.Add("Fizz");
            }

            if (i % 5 == 0)
            {
                result.Add("Buzz");
            }

            if (i % 7 == 0)
            {
                result.Add("Bang");
            }

            if (i % 11 == 0)
            {
                result.Clear();
                result.Add("Bong");
            }

            if (i % 13 == 0)
            {
                bool inserted = false;
                for (int j = 0; j < result.Count && !inserted; j++)
                {
                    if (result[j][0] == 'B')
                    {
                        result.Insert(j, "Fezz");
                        inserted = true;
                    }
                }
                if (!inserted) { result.Add("Fezz"); }
            }

            if (i % 17 == 0)
            {
                result.Reverse();
            }



            if (result.Count == 0)
            {
                result.Add(i.ToString());
            }

            string output = "";
            foreach (var s in result)
            {
                output = output + s;
            }

            return output;
        }

        static void Main(string[] args)
        {
            for (int i = 1; i <= 300; i++)
            {
                Console.WriteLine(Valuation2(i));
            }
            while (true) { }
        }
    }
}
