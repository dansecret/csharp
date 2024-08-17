using System;
using System.Collections.Generic; 

namespace Coding
{
    class NumberRuleGenerator
    {
        private Dictionary<int, string> rules = new Dictionary<int, string>();
        public void AddRule(int NumberKey, string output)
        {
            if (!rules.ContainsKey(NumberKey))
            {
                rules.Add(NumberKey, output);
            }
        }
        public void ProcessNumbers(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                string result = "";
                foreach (var rule in rules)
                {
                    if (i % rule.Key == 0)
                    {
                        result += rule.Value;
                    }
                }

                if (result == "")
                {
                    Console.WriteLine(i);
                }
                else
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Value Number : "); 
            int number = int.Parse(Console.ReadLine());

            NumberRuleGenerator myGenerator = new NumberRuleGenerator();

            myGenerator.AddRule(3, "foo");
            myGenerator.AddRule(5, "bar");
            myGenerator.AddRule(7, "jazz");
            myGenerator.AddRule(9, "huzz");
            myGenerator.ProcessNumbers(number);
        }
    }
}
