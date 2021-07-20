using board;

namespace chessMatch
{
    class Rock : Piece
    {
        public Rock(Board b, Color color) : base(b, color)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}