using board;

namespace chessMatch
{
    class King : Piece
    {
        private ChessMatch match;
        public King(Board b, Color color, ChessMatch match) : base(b, color)
        {
            this.match = match;
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

        public bool testRockForCastling(Position pos)
        {
            Piece p = b.piece(pos);
            return p != null && p is Rock && p.movAmount == 0 && p.color == color;
        }

        // Retur an array with all possible movements for the move
        public override bool[,] possiblesMovs()
        {
            bool[,] mat = new bool[b.rows, b.cols];
            Position pos = new Position(0, 0);

            // up
            pos.defineValuesPosition(position.row - 1, position.col);
            if (b.isValidPosition(pos) && canMoveHere(pos))
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
            pos.defineValuesPosition(position.row + 1, position.col + 1);
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

            // #Spetial movement Castling
            if (movAmount == 0 && !match.check)
            {
                // for short castling
                Position aux = new Position(position.row, position.col + 3);
                if (testRockForCastling(aux))
                {
                    Position p1 = new Position(position.row, position.col + 1);
                    Position p2 = new Position(position.row, position.col + 2);
                    if ( b.piece(p1) == null && b.piece(p2) == null)
                    {
                        mat[position.row, position.col + 2] = true;
                    }
                }

                // # for long castling
                aux = new Position(position.row, position.col - 4);
                if (testRockForCastling(aux))
                {
                    Position p1 = new Position(position.row, position.col - 1);
                    Position p2 = new Position(position.row, position.col - 2);
                    Position p3 = new Position(position.row, position.col - 3);
                    if ( b.piece(p1) == null && b.piece(p2) == null && b.piece(p3) == null)
                    {
                        mat[position.row, position.col - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}
