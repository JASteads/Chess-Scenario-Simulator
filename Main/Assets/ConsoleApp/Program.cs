using System;
using System.Collections.Generic;
using ConsoleApp.Code;

namespace Assets
{
    public class Program
    {
        int foo;

        static void Main(string[] args)
        {
            /* Will NOT work because Main() is static. 
             * Globals must be static in this local scope */
            // foo = 2;

            //List<int> whitePieces = new List<int>();
            //whitePieces.Add(3);

            Information pawn = new Information();
            pawn.setPosition(51);
            pawn.setPieceType(2);
            pawn.setTeamColor(1);
            pawn.printInformation();

            //Console.WriteLine("Hello World!");
            //Console.ReadLine();
        }

        public void bar()
        {
            foo = 4;
        }
    }
}