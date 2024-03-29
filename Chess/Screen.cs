﻿using System;
using System.Collections.Generic;
using board;
using chessMatch;

namespace Chess
{
    class Screen
    {
        public static void printMatch(ChessMatch match)
        {
            printBoard(match.b);
            Console.WriteLine();
            Screen.printCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.shift);

            ConsoleColor aux = Console.ForegroundColor;
            if (match.currentPlayer == Color.Black)
            {  
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (!match.finished)
            {
                Console.WriteLine("Waintg " + match.currentPlayer);
                if (match.check)
                {
                    Console.WriteLine("=====================");
                    Console.WriteLine("       CHECK!");
                    Console.WriteLine("=====================");
                }
            }
            else
            {
                Console.WriteLine("=====================");
                Console.WriteLine(" !!!! CHECKMATE !!!!");
                Console.WriteLine("=====================");
                Console.WriteLine("Winner: " + match.currentPlayer);
            }
            Console.ForegroundColor = aux;
        }
        
        public static void printBoard(Board b)
        {
            printInBlue("   a b c d e f g h");
            Console.WriteLine();
            for (int i = 0; i < b.rows; i++)
            {   
                printInBlue((8 - i) + "  ");
                for (int j = 0; j < b.cols; j++)
                {
                    printPiece(b.piece(i, j));
                }
                printInBlue((8 - i) + "  ");
                Console.WriteLine();
            }
            printInBlue("   a b c d e f g h");
            Console.WriteLine();
        }

        public static void printBoard(Board b, bool[,] possiblePositionsBoard)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.DarkGray;
            
            printInBlue("   a b c d e f g h");
            Console.WriteLine();
            for (int i = 0; i < b.rows; i++)
            {
                printInBlue((8 - i) + "  ");
                for (int j = 0; j < b.cols; j++)
                {
                    // highlight possible movements on the screen
                    if (possiblePositionsBoard[i, j])
                    {
                        Console.BackgroundColor = newBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    printPiece(b.piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                printInBlue((8 - i) + "  ");
                Console.WriteLine();
            }
            printInBlue("   a b c d e f g h");
            Console.WriteLine();
            Console.BackgroundColor = originalBackground;
        }

        public static void printCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces: ");
            Console.Write("White: ");
            printSet(match.capturedPiecesByColor(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            printSet(match.capturedPiecesByColor(Color.Black));
            Console.WriteLine();
        }

        public static void printSet(HashSet<Piece> set) 
        {
            Console.Write("[ ");
            foreach(Piece p in set)
            {
                Console.Write(p + " ");
            }
            Console.Write("]");
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
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
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
}
