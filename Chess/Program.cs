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
                b.putPiece(new King(b, Color.Black), new Position(0, 4));
                b.putPiece(new Rock(b, Color.Black), new Position(0, 0));
                b.putPiece(new Rock(b, Color.Black), new Position(0, 7));
                
                /* test for same position exception: 
                b.putPiece(new Rock(b, Color.Black), new Position(0, 7));
                b.putPiece(new Rock(b, Color.Black), new Position(0, 7));
                */
                /* test for invalid position exception:
                b.putPiece(new Rock(b, Color.Black), new Position(0, 9));
                */

                b.putPiece(new King(b, Color.White), new Position(7, 4));
                b.putPiece(new Rock(b, Color.White), new Position(7, 0));
                b.putPiece(new Rock(b, Color.White), new Position(7, 7));

                Screen.printBoard(b);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }


            
        }
    }
}
