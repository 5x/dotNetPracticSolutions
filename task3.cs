using System;
using System.Collections.Generic;

namespace task3 {

    public class A<T> {
        B b;
    }

    public class B  {
        public B() {

        }
    }


    class Program {
        static void Main(string[] args) {
            A<string> a = new A<string>();

            LinkedList<int> list = new LinkedList<int>();
            list.AddFirst(0);
            var i = list.AddAfter(list.First, 1);
            list.AddAfter(i, 2);

            foreach (var item in list) {
                Console.Write(item + " ");
            }

            Console.ReadKey();
        }
    }
}
