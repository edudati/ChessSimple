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
                    try
                    {
                        Console.Clear();
                        Screen.printMatch(match);

                        Console.Write("Origin: ");
                        Position origin = Screen.readPositionChess().ToPosition();
                        match.validateOriginPosition(origin);

                        bool[,] possiblePositionsBoard = match.b.piece(origin).possiblesMovs();
                        Console.Clear();
                        Screen.printBoard(match.b, possiblePositionsBoard);

                        Console.Write("Destination: ");
                        Position destination = Screen.readPositionChess().ToPosition();
                        match.validateDestinationPosition(origin, destination);

                        match.executeFullMov(origin, destination);
                    }
                    catch (BoardException exception)
                    {
                        Console.WriteLine(exception.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.printMatch(match);
            }
            catch (BoardException exception)
            {
                Console.WriteLine(exception.Message);
            }  
        }
    }
}
