using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;

namespace FizzBuzz
{
    public class Rule
    {
        Func<List<string>, int, List<string>> Operation;
        private Func<int, bool> Condition;
        public string Description()
        {
            return "todo";
        }
        public Rule(Func<List<string>, int, List<string>> newOperation, Func<int, bool> newCondition)
        {
            Operation = newOperation;
            Condition = newCondition;
        }

        public List<string> applyRule(List<string> list, int i)
        {
            if (!Condition(i))
            {
                return list;
            }
            List<string> result = new List<string>(list);
            return Operation(result, i);

        }
        public Rule FollowedBy(Rule rule)
        {
            List<string> NewApply(List<string> list, int i)
            {
                return rule.applyRule(this.applyRule(list, i), i);
            }
            return new Rule(NewApply, (i)=>true);
        }
    }
    class Program
    {
        static Rule FizzRule = new Rule(
            (list, i) => {
                list.Add("Fizz");
                return list;
            },
            (i) => i % 3 == 0
        );

        static Rule BuzzRule = new Rule(
            (list, i) => {
                list.Add("Buzz");
                return list;
            },
            (i) => i % 5 == 0);

        static Rule BangRule = new Rule(
            (list, i) => {
                list.Add("Bang");
                return list;
            },
            (i) => i % 7 == 0);

        static Rule BongRule = new Rule(
            (list, i) => {
                list.Clear();
                list.Add("Bong");
                return list;
            },
            (i) => i % 11 == 0);

        static Rule FezzRule = new Rule(
            (list, i) => {
                bool inserted = false;
                for (int j = 0; j < list.Count && !inserted; j++)
                {
                    if (list[j][0] == 'B')
                    {
                        list.Insert(j, "Fezz");
                        inserted = true;
                    }
                }
                if (!inserted) { list.Add("Fezz"); }
                return list;
            }, (i) => i % 13 == 0);

        private static Rule ReverseRule = new Rule(
            (list, i) =>
            {
                list.Reverse();
                return list;
            }, (i) => i % 17 == 0);

        static Rule EmptyRule = new Rule(
            (list, i) =>
            {
                if (list.Count == 0)
                {
                    list.Add(i.ToString());
                }
                return list;
            }, (i) => true);

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
            string output = "";
            foreach (var s in Part2Rule.applyRule(new List<string>(), i))
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
