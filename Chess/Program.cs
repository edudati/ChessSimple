using System;
using board;
using chessMatch;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.finished)
                {
                    Console.Clear();
                    Screen.printBoard(match.b);

                    Console.Write("Origin: ");
                    Position origin = Screen.readPositionChess().ToPosition();

                    bool[,] possiblePositionsBoard = match.b.piece(origin).possiblesMovs();
                    Console.Clear();
                    Screen.printBoard(match.b, possiblePositionsBoard);

                    Console.Write("Destination: ");
                    Position destination = Screen.readPositionChess().ToPosition();

                    match.performMov(origin, destination);
                }
                
                
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }





            
        }
    }
}
