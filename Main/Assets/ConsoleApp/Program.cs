using System;
using System.Collections.Generic;

namespace Assets
{
    public class Program
    {
        int foo;

        static void Main(string[] args)
        {
            /* Will NOT work because Main() is static. 
             * Globals must be static in this local scope */
            foo = 2;

            List<int> whitePieces = new List<int>();
            whitePieces.Add(3);

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        public void bar()
        {
            foo = 4;
        }
    }
}