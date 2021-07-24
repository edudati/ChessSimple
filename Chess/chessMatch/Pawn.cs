using board;

namespace chessMatch
{
    class Pawn : Piece
    {
        public Pawn(Board b, Color color) : base(b, color)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        // verify if the space is empty or have an opponent piece
        private bool canPawnMoveHere(Position pos)
        {
            Piece p = b.piece(pos);
            return p == null;
        }

        private bool canPawnCaptureHere(Position pos)
        {
            Piece p = b.piece(pos);
            return p != null && p.color != this.color;
        }

        public override bool[,] possiblesMovs()
        {
            bool[,] mat = new bool[b.rows, b.cols];
            Position pos = new Position(0, 0);
            
            // 2 up
            pos.defineValuesPosition(position.row - 2, position.col);
            if (b.isValidPosition(pos) && canPawnMoveHere(pos) && b.piece(pos).movAmount == 0)
            {
                mat[pos.row, pos.col] = true;
            }

            // up
            pos.defineValuesPosition(position.row - 1, position.col);
            if (b.isValidPosition(pos) && canPawnMoveHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // capture right
            pos.defineValuesPosition(position.row - 1, position.col + 1);
            if (b.isValidPosition(pos) && canPawnCaptureHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // capture left
            pos.defineValuesPosition(position.row - 1, position.col - 1);
            if (b.isValidPosition(pos) && canPawnCaptureHere(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            return mat;
        }
    }
}
