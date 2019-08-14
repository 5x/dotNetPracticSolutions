using System;

namespace task2 {

    abstract class Factory {
        protected string name;
        protected string text;

        protected Factory() {
            this.name = "unnamed";
        }

        protected Factory(string name) {
            this.name = name;
        }

        protected Factory(string name, string text)
            : this(name) {
            this.text = text;
        }

        public string Name {
            get { return name; }
            set { this.name = value; }
        }

        public string Text {
            get { return text; }
            set { this.text = value; }
        }

        public string Show() {
            return name + "\n" + text;
        }

    }

    class SymbString : Factory {
        public SymbString() : base() { }
        public SymbString(string name) : base(name) { }
        public SymbString(string name, string text) : base(name, text) { }

        public SymbString(SymbString a)
            : base(a.name, a.text) { }

        public static SymbString operator +(SymbString x, SymbString y) {
            return new SymbString(x.name, x.text + y.text);
        }

        public static SymbString operator +(SymbString x, BinString y) {
            return new SymbString(x.name, x.text + y.Text);
        }

        public static SymbString operator +(SymbString x, string text) {
            return new SymbString(x.name, x.text + text);
        }
    }

    class BinString : Factory {
        public BinString() : base() { }
        public BinString(string name) : base(name) { }
        public BinString(string name, string text) : base(name, text) { }
        public BinString(string name, byte[] bytes)
            : base(name, System.Text.Encoding.UTF8.GetString(bytes)) { }

        public BinString(BinString a)
            : base(a.name, a.text) { }

        public static BinString operator +(BinString x, BinString y) {
            return new BinString(x.name, x.text + y.text);
        }

        public static BinString operator +(BinString x, SymbString y) {
            return new BinString(x.name, x.text + y.Text);
        }

        public static BinString operator +(BinString x, byte[] bytes) {
            string value = System.Text.Encoding.UTF8.GetString(bytes);
            return new BinString(x.name, x.text + value);
        }
    }

    class Program {
        static void Main(string[] args) {
            var T1 = new SymbString("T1", "Hello, word.");
            var T2 = new BinString("T2", new byte[] { 64, 48, 1, 24 });
            T1 += T2;
            Console.WriteLine(T1.Show());
            Console.ReadKey();
        }
    }
}
