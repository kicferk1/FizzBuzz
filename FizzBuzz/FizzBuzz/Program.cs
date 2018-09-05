using System;
using System.Collections;
using System.Collections.Generic;

namespace FizzBuzz
{
    public class Rule
    {
        Func<List<string>, int, List<string>> Operation;
        private Func<int, bool> Condition;
        public string Description;
        public Rule(Func<List<string>, int, List<string>> newOperation, Func<int, bool> newCondition, string description="")
        {
            Operation = newOperation;
            Condition = newCondition;
            Description = description;
        }

        public List<string> ApplyRule(List<string> list, int i)
        {
            if (!Condition(i))
            {
                return list;
            }
            var result = new List<string>(list);
            return Operation(result, i);

        }
        public Rule FollowedBy(Rule rule)
        {
            List<string> NewApply(List<string> list, int i)
            {
                return rule.ApplyRule(ApplyRule(list, i), i);
            }
            return new Rule(NewApply, (i)=>true);
        }

        public static Rule UserRule(string conditionType, int conditionValue, string operationType, string operationValue)
        {
            return new Rule((list, k) =>
                {
                    switch (operationType[0])
                    {
                        case 'B':
                            list.Insert(0, operationValue);
                            return list;
                        case 'E':
                            list.Add(operationValue);
                            return list;
                        case 'R':
                            list.Reverse();
                            return list;
                        default:
                            Console.WriteLine("Operation not supported :(");
                            return list;
                    }
                }, (k) =>
                {
                    switch (conditionType[0])
                    {
                        case '>':
                            return k > conditionValue;
                        case '<':
                            return k < conditionValue;
                        case '=':
                            return k == conditionValue;
                        case '%':
                            return k % conditionValue == 0;
                        default:
                            Console.WriteLine("Condition not supported :(");
                            return false;
                    }
                },
                "If number " + conditionType[0].ToString() + " " + conditionValue + ", then " + operationType + " " + operationValue
            );
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

        public static string Valuation2(int i)
        {
            var output = "";
            foreach (var s in Part2Rule.ApplyRule(new List<string>(), i))
            {
                output = output + s;
            }
            return output;
        }

        private static List<Rule> AddRule(List<Rule> rules)
        {
            Console.WriteLine("Pick operation type(B - beginning insert, E - ending insert, R - reverse, O - original)");
            var operationType = Console.ReadLine();
            if (operationType != null && (!(operationType[0] == 'B' || operationType[0] == 'E' || operationType[0] == 'R' || operationType[0] == 'O')))
            {
                Console.WriteLine("Invalid operation type");
                return rules;
            }
            if (operationType != null && operationType[0] == 'O')
            {
                Part2Rule.Description = "Original FizzBuzzBangBongFezz rules";
                rules.Add(Part2Rule);
                return rules;
            }
            string operationValue = "";
            if (operationType != null && (operationType[0] == 'B' || operationType[0] == 'E'))
            {
                Console.WriteLine("Type phrase to insert");
                operationValue = Console.ReadLine();
            }

            Console.WriteLine("Pick condition type(>, <, =, %):");
            var conditionType = Console.ReadLine();
            if (conditionType != null && !(conditionType[0] == '<' || conditionType[0] == '>' || conditionType[0] == '=' || conditionType[0] == '%'))
            {
                Console.WriteLine("Invalid condition type");
                return rules;
            }
            Console.WriteLine("Type condition value(number on the right hand side):");
            var conditionValue = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var rule = Rule.UserRule(conditionType, conditionValue, operationType, operationValue);
            rules.Add(rule);
            return rules;
        }

        private static List<Rule> AddRules(List<Rule> rules)
        {
            bool addingRules = true;
            while (addingRules)
            {
                string rulesDescription = "";
                foreach (var rule in rules)
                {
                    rulesDescription = rulesDescription + rule.Description + "\n";
                }
                Console.WriteLine("Current rules: " + rulesDescription);
                Console.WriteLine("Do you want to add more rules? y/n");
                string j = Console.ReadLine();
                if (j == "n" || j == "N")
                {
                    addingRules = false;
                }
                else if (j == "y" || j == "Y")
                {
                    rules = AddRule(rules);
                }
            }

            return rules;
        }

        public static void Main()
        {
            
            var rules = new List<Rule>();
            while (true)
            {
                var fizzBuzzer = new MyClass();

                foreach (var value in fizzBuzzer)
                {
                    Console.WriteLine(value);
                }


                Console.WriteLine("What number do you want to evaluate?");
                var i = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                rules = AddRules(rules);

                var finalRule = new Rule((list, k) => list, (k) => true);
                foreach (var rule in rules)
                {
                    finalRule = finalRule.FollowedBy(rule);
                }

                var output = "";
                foreach (var s in finalRule.ApplyRule(new List<string>(), i))
                {
                    output = output + s;
                }
                Console.WriteLine(output);
            }
        }
    }

    public class MyClass : IEnumerable
    {
        public MyClass()
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum();
        }
    }

    public class PeopleEnum : IEnumerator
    {
        private int current = 10;
        private int final = 100;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.

        public PeopleEnum()
        {
        }

        public bool MoveNext()
        {
            current++;
            return (current < final);
        }

        public void Reset()
        {
            current = 10;
        }

        object IEnumerator.Current
        {
            get
            {
                return Program.Valuation2(current);
            }
        }

        public string Current
        {
            get
            {
                    return Program.Valuation2(current);
              
            }
        }
    }

}
