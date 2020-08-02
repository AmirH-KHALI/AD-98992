using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace A10 {
    public class Contact {
        public string Name;
        public int Number;

        public Contact(string name, int number) {
            Name = name;
            Number = number;
        }

    }

    public class Q1PhoneBook {
        
        protected static Dictionary<int, string> pb;

        public static void Main() {
            long sz = long.Parse(Console.ReadLine());
            string[] input = new string[sz];
            for (int i = 0; i < sz; ++i) {
                input[i] = Console.ReadLine();
            }
            string[] ans = Solve(input);
            for (int i = 0; i < ans.Length; ++i) {
                Console.WriteLine(ans[i]);
            }
        }

        public static string[] Solve(string[] commands) {
            pb = new Dictionary<int, string>();

            List<string> result = new List<string>();
            foreach (var cmd in commands) {
                var toks = cmd.Split();
                var cmdType = toks[0];
                var args = toks.Skip(1).ToArray();
                int number = int.Parse(args[0]);
                switch (cmdType) {
                    case "add":
                    Add(args[1], number);
                    break;
                    case "del":
                    Delete(number);
                    break;
                    case "find":
                    result.Add(Find(number));
                    break;
                }
            }
            return result.ToArray();
        }

        public static void Add(string name, int number) {
            pb[number] = name;
        }


        public static string Find(int number) {
            if (pb.ContainsKey(number)) {
                return pb[number];
            }

            return "not found";
        }

        public static void Delete(int number) {
            pb.Remove(number);
        }
    }
}
