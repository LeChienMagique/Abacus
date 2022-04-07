using System;

namespace Abacus {
    public static class Program {
        public static int Main(string[] args) {
            Console.WriteLine("Hello, Abacus!");
            for (int i = 0; i < args.Length; ++i) {
                Console.WriteLine("argument {0}: {1}", i, args[i]);
            }

            // Console.WriteLine(Console.ReadLine());
            // Returns an error code of 0, everything went fine!
            return 0;
        }
    }
}