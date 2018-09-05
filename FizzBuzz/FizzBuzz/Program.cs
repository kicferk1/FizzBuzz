using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;

namespace FizzBuzz
{
    public class Rule
    {
        public Func<List<string>, int, List<string>> apply;
        public Rule(Func<List<string>, int, List<string>> newApply)
        {
            apply = newApply;
        }

        public Rule FollowedBy(Rule rule)
        {
            List<string> NewApply(List<string> list, int i)
            {
                return rule.apply(this.apply(list, i), i);
            }
            return new Rule(NewApply);
        }
    }
    class Program
    {
        static List<string> FizzFunction(List<string> list, int i)
        {
            List<string> result = new List<string>(list);
            if (i % 3 == 0)
            {
                result.Add("Fizz");
            }
            return result;
        }
        static Rule FizzRule = new Rule(FizzFunction);

        static List<string> BuzzFunction(List<string> list, int i)
        {
            List<string> result = new List<string>(list);
            if (i % 5 == 0)
            {
                result.Add("Buzz");
            }
            return result;
        }
        static Rule BuzzRule = new Rule(BuzzFunction);

        static List<string> BangFunction(List<string> list, int i)
        {
            List<string> result = new List<string>(list);
            if (i % 7 == 0)
            {
                result.Add("Bang");
            }
            return result;
        }
        static Rule BangRule = new Rule(BangFunction);

        static List<string> BongFunction(List<string> list, int i)
        {
            List<string> result = new List<string>(list);
            if (i % 11 == 0)
            {
                result.Clear();
                result.Add("Bong");
            }
            return result;
        }
        static Rule BongRule = new Rule(BongFunction);

        static List<string> FezzFunction(List<string> list, int i)
        {
            List<string> result = new List<string>(list);
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
            return result;
        }
        static Rule FezzRule = new Rule(FezzFunction);

        static List<string> ReverseFunction(List<string> list, int i)
        {
            List<string> result = new List<string>(list);
            if (i % 17 == 0)
            {
                result.Reverse();
            }
            return result;
        }
        static Rule ReverseRule = new Rule(ReverseFunction);

        static List<string> EmptyFunction(List<string> list, int i)
        {
            List<string> result = new List<string>(list);
            if (result.Count==0)
            {
                result.Add(i.ToString());
            }
            return result;
        }
        static Rule EmptyRule = new Rule(EmptyFunction);

        static private Rule Part2Rule = 
            FizzRule.FollowedBy(
                BuzzRule.FollowedBy(
                    BangRule.FollowedBy(
                        BongRule.FollowedBy(
                            FezzRule.FollowedBy(
                                ReverseRule).FollowedBy(
                                EmptyRule)))));

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

            string output2 = "";
            foreach (var s in Part2Rule.apply(new List<string>(), i))
            {
                output2 = output2 + s;
            }

            if (output2 != output)
            {
                return "ERROR, " + i + " " + output + " " + output2;
            }
            return output;
        }

        static void Main(string[] args)
        {
            for (int i = 1; i <= 300; i++)
            {
                Console.WriteLine(Valuation2(i));
            }

            while (true)
            {
                var rules = new List<String>();
                Console.WriteLine("What number do you want to evaluate?");
                string i = Console.ReadLine();

                bool addingRules = true;
                while (addingRules)
                {
                    Console.WriteLine("Current rules: " + rules.ToArray());
                    Console.WriteLine("Do you want to add more rules? y/n");
                    string j = Console.ReadLine();
                    if (j == "n" || j == "N")
                    {
                        addingRules = false;
                    }else if (j == "y" || j == "Y")
                    {

                    }
                }
                
                Console.WriteLine(Valuation2(int.Parse(i)));
            }
        }
    }
}
