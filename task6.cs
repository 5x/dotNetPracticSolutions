using System;
using System.Collections.Generic;

namespace pr6 {
    struct Point {
        int x;
        int y;

        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Point a) {
            return a.x == this.x && a.y == this.y;
        }

        public string Print() {
            return string.Format("({0}, {1})", x, y);
        }
    };

    struct Node {
        Point prev;
        Point current;

        public Node(Point prev, Point current) {
            this.prev = prev;
            this.current = current;
        }

        public Point Prev {
            get { return prev; }
        }

        public Point Current {
            get { return current; }
        }
    };

    class Labirint {
        Stack<Node> stack;
        int[,] arr;

        public Labirint(int[,] arr) {
            stack = new Stack<Node>();
            this.arr = arr;
        }

        public void ViewPath() {
            if (fillStack()) {
                Console.WriteLine(stack.Peek().Current.Print());

                while (true) {
                    Point prevCell = stack.Pop().Prev;
                    Console.WriteLine(prevCell.Print());

                    if (stack.Count > 0) {
                        while (!stack.Peek().Current.Equals(prevCell)) {
                            stack.Pop();
                        }
                    } else {
                        return;
                    }
                }
            } else {
                Console.WriteLine("Dont have exit");
            }
        }

        bool fillStack() {
            if (stack.Count > 0) {
                stack.Clear();
            }

            const int OPEN_VALUE = 0;
            const int START_VALUE = -1;
            int maxRowNum = arr.GetLength(0) - 1;
            int maxColNum = arr.GetLength(1) - 1;
            int stackDepth = START_VALUE;

            for (int currDepth = START_VALUE; currDepth >= stackDepth; --currDepth) {
                for (int x = 0; x <= maxRowNum; ++x) {
                    for (int y = 0; y <= maxColNum; ++y) {
                        if (arr[x, y] != currDepth) {
                            continue;
                        }

                        for (int i = max(x - 1, 0); i <= min(x + 1, maxRowNum); ++i) {
                            for (int j = max(y - 1, 0); j <= min(y + 1, maxColNum); ++j) {
                                if ((i == x || j == y) && arr[i, j] == OPEN_VALUE) {
                                    stackDepth = currDepth - 1;
                                    arr[i, j] = stackDepth;

                                    Node currentPathNode = new Node(new Point(x, y), new Point(i, j));
                                    stack.Push(currentPathNode);

                                    if (i == 0 || i == maxRowNum || j == 0 || j == maxColNum) {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        int min(int x, int y) {
            return x < y ? x : y;
        }

        int max(int x, int y) {
            return x > y ? x : y;
        }
    }

    class Program {
        static void Main(string[] args) {
            // -1 Start | 0 Free | 1 Wall;
            Labirint labirint = new Labirint(
                new[,] { { 1, 1, 1, 1, 1, -1, 1, 1, 1},
                            {1, 0, 0, 0, 1, 0, 1, 0, 1},
                            {1, 0, 1, 0, 0, 0, 1, 0, 1},
                            {1, 0, 1, 0, 1, 0, 1, 0, 1},
                            {1, 0, 0, 0, 0, 0, 1, 0, 1},
                            {1, 0, 1, 1, 1, 1, 1, 0, 1},
                            {1, 0, 0, 1, 0, 0, 0, 0, 1},
                            {1, 1, 0, 0, 0, 1, 1, 1, 1},
                            {1, 1, 1, 1, 0, 1, 1, 1, 1},
                            {1, 1, 1, 1, 0, 1, 1, 1, 1}});

            labirint.ViewPath();

            Console.ReadKey();
        }
    }
}
