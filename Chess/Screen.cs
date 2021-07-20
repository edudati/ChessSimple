using System;
using board;
using chessMatch;

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
                        printPiece(b.piece(i, j));
                    } 
                }
                printInBlue((8 - i) + "  ");
                Console.WriteLine();
            }
            printInBlue("   a b c d e f g h");
            Console.WriteLine();
        }

        public static PositionOfChess readPositionChess()
        {
            string aux = Console.ReadLine();
            char col = aux[0];
            int row = int.Parse("" + aux[1]);
            return new PositionOfChess(col, row);
        }

        private static void printInBlue(string text)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(text);
            Console.ForegroundColor = aux;
        }

        // print piece on the screen with your respective color
        private static void printPiece(Piece piece)
        {
            if (piece.color == Color.White)
            {
                Console.Write(piece + " ");
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(piece + " ");
                Console.ForegroundColor = aux;
            }
        }
    }
}
