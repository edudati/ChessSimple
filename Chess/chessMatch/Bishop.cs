using board;

namespace chessMatch
{
    class Bishop : Piece
    {
        public Bishop(Board b, Color color) : base(b, color)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        // verify if the space is empty or have an opponent piece
        private bool canMoveHere(Position pos)
        {
            Piece p = b.piece(pos);
            return p == null || p.color != this.color;
        }

        // Retur an array with all possible movements for the move
        public override bool[,] possiblesMovs()
        {
            bool[,] mat = new bool[b.rows, b.cols];
            Position pos = new Position(0, 0);

            // up left
            pos.defineValuesPosition(position.row -1, position.col - 1);
            while (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
                if (b.piece(pos) != null && b.piece(pos).color != color)
                {
                    break;
                }
                pos.row -= 1;
                pos.col -= 1;
            }

            // up right
            pos.defineValuesPosition(position.row - 1, position.col + 1);
            while (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
                if (b.piece(pos) != null && b.piece(pos).color != color)
                {
                    break;
                }
                pos.row -= 1;
                pos.col += 1;
            }

            // down left
            pos.defineValuesPosition(position.row + 1, position.col - 1);
            while (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
                if (b.piece(pos) != null && b.piece(pos).color != color)
                {
                    break;
                }
                pos.row += 1;
                pos.col -= 1;
            }

            // down right
            pos.defineValuesPosition(position.row + 1, position.col + 1);
            while (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
                if (b.piece(pos) != null && b.piece(pos).color != color)
                {
                    break;
                }
                pos.row += 1;
                pos.col += 1;
            }
            return mat;
        }
    }
}
