using board;

namespace chessMatch
{
    class PositionOfChess
    {
        public char col { get; set; }
        public int row { get; set; }

        public PositionOfChess(char col, int row)
        {
            this.col = col;
            this.row = row;
        }

        public Position ToPosition()
        {
            return new Position(8 - row, col - 'a');
        }

        public override string ToString()
        {
            return ""
                + col
                + row;
        }
    }
}
