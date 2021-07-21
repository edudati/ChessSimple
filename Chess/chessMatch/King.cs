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
            if(b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }
            
            // down
            pos.defineValuesPosition(position.row + 1, position.col);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // right
            pos.defineValuesPosition(position.row, position.col + 1);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // left
            pos.defineValuesPosition(position.row, position.col - 1);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // up right
            pos.defineValuesPosition(position.row - 1, position.col + 1);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // up left
            pos.defineValuesPosition(position.row - 1, position.col - 1);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // down right
            pos.defineValuesPosition(position.row + 1, position.col +1);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // down left
            pos.defineValuesPosition(position.row + 1, position.col - 1);
            if (b.isValidPosition(pos) && canMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            return mat;
        }
    }
}
