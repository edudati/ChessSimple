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

                    Console.WriteLine("Origin: ");
                    Position origin = Screen.readPositionChess().ToPosition();

                    Console.WriteLine("Destination: ");
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
