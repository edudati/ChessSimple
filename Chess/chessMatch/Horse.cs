using board;

namespace chessMatch
{
    class Horse : Piece
    {
        public Horse(Board b, Color color) : base(b, color)
        {
        }

        public override string ToString()
        {
            return "H";
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

            // 2 up 1 left
            pos.defineValuesPosition(position.row - 2, position.col - 1);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // 2 up 1 right
            pos.defineValuesPosition(position.row - 2, position.col + 1);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // 2 down 1 right
            pos.defineValuesPosition(position.row + 2, position.col + 1);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // 2 down 1 left
            pos.defineValuesPosition(position.row + 2, position.col - 1);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // 2 right 1 up
            pos.defineValuesPosition(position.row - 1, position.col + 2);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // 2 right 1 down
            pos.defineValuesPosition(position.row + 1, position.col + 2);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // 2 left 1 up
            pos.defineValuesPosition(position.row - 1, position.col - 2);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // 2 left 1 down
            pos.defineValuesPosition(position.row + 1, position.col - 2);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            return mat;
        }
    }
}
