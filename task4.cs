using System;
using System.Collections.Generic;

namespace task4 {
    public class Student {
        string fio;
        string group;
        int[] evaluation;
        double average;

        public Student(string fio, string group, params int[] evaluation) {
            this.fio = fio;
            this.group = group;
            this.evaluation = evaluation;

            average = averageValue();
        }

        public string Fio {
            get { return fio; }
            set { fio = value; }
        }

        public string Group {
            get { return group; }
            set { group = value; }
        }

        public int[] Evaluation {
            get { return evaluation; }
            set { evaluation = value; }
        }

        public double Average { get { return average; } }

        private double averageValue() {
            if (evaluation == null || evaluation.Length <= 0) {
                return 0;
            }

            double sum = 0;
            for (int i = 0; i < evaluation.Length; i++) {
                sum += evaluation[i];
            }

            return sum / evaluation.Length;
        }
    }


    public static class Cfg {
        public static int maxEvaluation { get { return 5; } }
        public static int evaluationLen { get { return 5; } }
        public static int evaluationLimitValue { get { return 4; } }
    }

    public class Students : List<Student> {
        public bool Add() {
            string fio, group;
            int[] evaluation;

            Console.Write("FIO: ");
            fio = Console.ReadLine();
            Console.Write("Group: ");
            group = Console.ReadLine();

            evaluation = new int[Cfg.evaluationLen];
            int i = 0;
            while (i < evaluation.Length) {
                Console.Write("Evaluation {0}: ", i + 1);
                if (int.TryParse(Console.ReadLine(), out evaluation[i]) &&
                    evaluation[i] > 0 && evaluation[i] <= Cfg.maxEvaluation) {
                   i++;
                }
            }

            bool isDataValid = fio != null && fio.Length > 0 &&
                               group != null && group.Length > 0 &&
                               evaluation != null &&
                               evaluation.Length == Cfg.evaluationLen;

            if (isDataValid) {
                this.Add(new Student(fio, group, evaluation));
            }

            return isDataValid;
        }

        public new string this[int index] {
            get {
                string result = "Student undefined";
                if (IsContain(index)) {
                    var item = base[index];
                    if (item != null) {
                        string evaluation = "";
                        for (int i = 0; i < item.Evaluation.Length; i++) {
                            evaluation += item.Evaluation[i] + " ";                        
                        }

                        result = string.Format("FIO: {0}\nGroup: {1}\n" +
                            "Evaluation(Average: {2}):\n[ {3}]\n",
                            item.Fio, item.Group, item.Average, evaluation);
                    }
                }

                return result;
            }
        }

        public bool CheckAverage(int index) {
            var a = base[index];

            for (int i = 0; i < a.Evaluation.Length; i++) {
                if (a.Evaluation[i] < Cfg.evaluationLimitValue) {
                    return false;                            
                }
            }

            return true;
        }

        public bool IsContain(int index) {
            return index >= 0 && index < this.Count;
        }

        public new void Sort() {
            this.Sort(delegate(Student a, Student b) {
                int compareDate = b.Average.CompareTo(a.Average);
                return compareDate == 0 ? a.Fio.CompareTo(b.Fio) : compareDate;
            });
        }
    }

    class Program {
        static void Main(string[] args) {
            Students students = new Students();
            const int NUM_OF_STUDENTS = 2;
            int i = 0;

            while (i < NUM_OF_STUDENTS) {
                if (students.Add()) {
                    i++;
                } else {
                    Console.WriteLine("Try again");             
                }
            }

            students.Sort();

            Console.Write("View students whith average more that 4?(Y/n): ");
            string answer = Console.ReadLine();
            bool filtered = answer[0] == 'Y' || answer[0] == 'y';
            bool isEmpty = true;

            for (i = 0; i < students.Count; i++) {
                if (!filtered || students.CheckAverage(i)) {
                    isEmpty = false;
                    Console.WriteLine(students[i]);
                }
            }

            if (isEmpty) {
                Console.WriteLine("No one found.");
            }

            Console.ReadKey();
        }
    }
}
