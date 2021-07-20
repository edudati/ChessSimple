using System;
using board;
using chessMatch;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board b = new Board(8, 8);

            try
            {
                b.putPiece(new Rock(b, Color.Black), new PositionOfChess('a', 8).ToPosition());
                b.putPiece(new King(b, Color.Black), new PositionOfChess('e', 8).ToPosition());
                b.putPiece(new Rock(b, Color.Black), new PositionOfChess('h', 8).ToPosition());
                
                /* test for same position exception: 
                b.putPiece(new Rock(b, Color.Black), new Position(0, 7));
                b.putPiece(new Rock(b, Color.Black), new Position(0, 7));
                */
                /* test for invalid position exception:
                b.putPiece(new Rock(b, Color.Black), new Position(0, 9));
                */

                b.putPiece(new Rock(b, Color.White), new PositionOfChess('a', 1).ToPosition());
                b.putPiece(new King(b, Color.White), new PositionOfChess('e', 1).ToPosition());
                b.putPiece(new Rock(b, Color.White), new PositionOfChess('h', 1).ToPosition());

                Screen.printBoard(b);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            
            //test position of chess to position
            PositionOfChess pChess = new PositionOfChess('a', 3);
            Console.WriteLine("Position of chess: " + pChess);
            Console.WriteLine("Position of the array: " + pChess.ToPosition());


            
        }
    }
}
