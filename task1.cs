using System;
using System.Collections.Generic;
using System.Linq;

namespace task1 {

    class BinaryTree {
        SortedDictionary<string, string> dictionary;
        int currentId;

        public BinaryTree() {
            dictionary = new SortedDictionary<string, string>();
        }

        public void Add() {
            Console.Write("Enter english word: ");
            string en = Console.ReadLine();
            Console.Write("Russian translate: ");
            this.Add(en, Console.ReadLine());
        }

        public void Add(string key, string value) {
            if (!dictionary.ContainsKey(key)) {
                dictionary.Add(key, value);
            } else {
                Console.WriteLine("Key alredy consist");

                if (dictionary[key] != value) {
                    Console.WriteLine("Replase value(Y\\n)?");
                    string answer = Console.ReadLine();

                    if (answer[0] == 'Y') {
                        dictionary[key] = value;
                    }
                }
            }
        }

        public void Delete() {
            Console.Write("Enter key to delete: ");
            string key = Console.ReadLine();

            if (dictionary.ContainsKey(key)) {
                dictionary.Remove(key);
                Console.WriteLine("Key - Value pair successful delete");
            } else {
                Console.WriteLine("Key dont consist in dictionary");
            }
        }

        public void View() {
            foreach (KeyValuePair<string, string> item in dictionary) {
                Console.WriteLine(item.Key + " - " + item.Value);
            }
        }

        public string View(string key) {
            if (dictionary.ContainsKey(key)) {
                return key + " - " + dictionary[key];
            }

            return "Dictionary dont consist this key";
        }

        public void ViewNext() {
            Console.WriteLine(dictionary.Count > 0 && currentId < dictionary.Count - 1 ?
                View(dictionary.ElementAt(currentId++).Key) :
                "Dictionary is empty or dont have next value");
        }

        public void ViewPrev() {
            Console.WriteLine(dictionary.Count > 0 && currentId > 1 ?
                View(dictionary.ElementAt(currentId--).Key) :
                "Dictionary is empty or dont have prev value");
        }

        public void Search() {
            Console.Write("Enter search query value: ");
            string result = Console.ReadLine();

            if (result != null) {
                Console.WriteLine(View(result));
            }
        }

        private void loadFromFile(string fileName) {
            using (var file = System.IO.File.OpenText(fileName)) {
                string line;
                while ((line = file.ReadLine()) != null) {
                    var values = line.Trim().Split('-', ' ');
                    if (values != null && values.Length > 1) {
                        Add(values[0], values[1]);
                    }
                }
            }
        }

        public void LoadFromFile() {
            Console.Write("Enter file name: ");
            loadFromFile(Console.ReadLine());
        }
    }

    class Program {
        static void Main(string[] args) {
            BinaryTree tree = new BinaryTree();

            tree.Add("tree", "дерево");
            tree.Add("leaf", "листок");
            tree.Add("ball", "мяч");
            tree.Add("program", "програма");
            tree.Add("good", "добре");
            tree.Add("bad", "погано");

            int n;
            while (true) {
                Console.WriteLine("1. Add");
                Console.WriteLine("2. Delete");
                Console.WriteLine("3. Search");
                Console.WriteLine("4. Add from file");
                Console.WriteLine("5. View all");
                Console.WriteLine("6. View next");
                Console.WriteLine("7. View prev");
                Console.WriteLine("8. Exit");
                Console.Write("Select function: ");

                int.TryParse(Console.ReadLine(), out n);

                switch (n) {
                    case 1:
                        tree.Add();
                        break;
                    case 2:
                        tree.Delete();
                        break;
                    case 3:
                        tree.Search();
                        break;
                    case 4:
                        tree.LoadFromFile();
                        break;
                    case 5:
                        tree.View();
                        break;
                    case 6:
                        tree.ViewNext();
                        break;
                    case 7:
                        tree.ViewPrev();
                        break;
                    case 8:
                        return;
                }
            }
        }
    }
}
