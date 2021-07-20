using board;

namespace chessMatch
{
    class King : Piece
    {
        public King(Board b, Color color) : base(b, color)
        {
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
