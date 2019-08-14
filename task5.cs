using System;
using System.IO;

namespace task5 {
    class Program {
        static void view(string text, string word) {
            char[] sepparators = { '\n', '.', '!', '?', ';' };
            string[] lines = text.Split(sepparators);

            for (int i = 0; i < lines.Length; i++) {
                if (lines[i].Contains(word)) {
                    Console.WriteLine(lines[i]);
                }
            }
        }

        static void Main(string[] args) {
            string text;
            string word;
            string filepath;

            Console.Write("File path: ");
            filepath = Console.ReadLine();
            Console.Write("Search word: ");
            word = Console.ReadLine();

            if (File.Exists(filepath)) {
                text = File.ReadAllText(filepath);
                Console.WriteLine("Result");
                view(text, word);
            } else {
                Console.WriteLine("File not found.");
            }

            Console.ReadKey();
        }
    }
}
