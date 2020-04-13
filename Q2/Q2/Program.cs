using System;
using System.Collections.Generic;

namespace Q2 {
    class Program {
        public static object HashMap { get; private set; }

        public static Dictionary<char, int> solve (string pattern) {
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            for (int i = 0; i < pattern.Length; ++i) {
                if (!dictionary.ContainsKey(pattern[i])) {
                    dictionary.Add(pattern[i], Math.Max(pattern.Length - i - 1, 1));
                } else {
                    dictionary[pattern[i]] = Math.Max(pattern.Length - i - 1, 1);
                }
            }
            return dictionary;
        }

        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
        }
    }
}
