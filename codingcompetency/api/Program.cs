using System;
using System.Collections.Generic; 

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