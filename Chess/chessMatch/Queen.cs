using board;

namespace chessMatch
{
    class Queen : Piece
    {
        public Queen(Board b, Color color) : base(b, color)
        {
        }

        public override string ToString()
        {
            return "Q";
        }

        // verify if the space is empty or have an opponent piece
        private bool canMoveHere(Position pos)
        {
            Piece p = b.piece(pos);
            return p == null || p.color != this.color;
        }

        public override bool[,] possiblesMovs()
        {
            bool[,] mat = new bool[b.rows, b.cols];
            Position pos = new Position(0, 0);

            // up
            pos.defineValuesPosition(position.row - 1, position.col);
            while (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
                if (b.piece(pos) != null && b.piece(pos).color != color)
                {
                    break;
                }
                pos.row -= 1;
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

            // right
            pos.defineValuesPosition(position.row, position.col + 1);
            while (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
                if (b.piece(pos) != null && b.piece(pos).color != color)
                {
                    break;
                }
                pos.col += 1;
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

            // down
            pos.defineValuesPosition(position.row + 1, position.col);
            while (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
                if (b.piece(pos) != null && b.piece(pos).color != color)
                {
                    break;
                }
                pos.row += 1;
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

            // left
            pos.defineValuesPosition(position.row, position.col - 1);
            while (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
                if (b.piece(pos) != null && b.piece(pos).color != color)
                {
                    break;
                }
                pos.col -= 1;
            }

            // up left
            pos.defineValuesPosition(position.row - 1, position.col - 1);
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

            return mat;
        }
    }
}
