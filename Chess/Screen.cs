using System;
using board;

namespace Chess
{
    class Screen
    {
        public static void printBoard(Board b)
        {
            printInBlue("   a b c d e f g h");
            Console.WriteLine();
            
            for (int i = 0; i < b.rows; i++)
            {
                
                printInBlue((8 - i) + "  ");
                for (int j = 0; j < b.cols; j++)
                {
                    if (b.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(b.piece(i, j) + " ");
                    } 
                }
                printInBlue((8 - i) + "  ");
                Console.WriteLine();
            }
            printInBlue("   a b c d e f g h");
            Console.WriteLine();
        }

        private static void printInBlue(string text)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(text);
            Console.ForegroundColor = aux;
        }
    }
}
