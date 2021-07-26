using board;

namespace chessMatch
{
    class Pawn : Piece
    {

        private ChessMatch match;

        public Pawn(Board b, Color color, ChessMatch match) : base(b, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        // verify if the space is empty or have an opponent piece

        private bool existOpponent(Position pos)
        {
            Piece p = b.piece(pos);
            return p != null && p.color != color;
        }

        private bool empty(Position pos)
        {
            return b.piece(pos) == null;
        }

        public override bool[,] possiblesMovs()
        {
            bool[,] mat = new bool[b.rows, b.cols];
            Position pos = new Position(0, 0);


            if(color == Color.White)
            {
                // 1 forward white
                pos.defineValuesPosition(position.row - 1, position.col);
                if (b.isValidPosition(pos) && empty(pos))
                {
                    mat[pos.row, pos.col] = true;
                }

                // 2 forward white
                pos.defineValuesPosition(position.row - 2, position.col);
                Position pos2 = new Position(position.row - 1, position.col);
                if (b.isValidPosition(pos2) && empty(pos2) && b.isValidPosition(pos) && empty(pos) && movAmount == 0)
                {
                    mat[pos.row, pos.col] = true;
                }

                // capture right white
                pos.defineValuesPosition(position.row - 1, position.col - 1);
                if (b.isValidPosition(pos) && existOpponent(pos))
                {
                    mat[pos.row, pos.col] = true;
                }

                // capture left white
                pos.defineValuesPosition(position.row - 1, position.col + 1);
                if (b.isValidPosition(pos) && existOpponent(pos))
                {
                    mat[pos.row, pos.col] = true;
                }

                // #special movement en passant white
                if (position.row == 3)
                {
                    Position posLeft = new Position(position.row, position.col - 1);
                    if (b.isValidPosition(posLeft) && existOpponent(posLeft) && b.piece(posLeft) == match.pieceVulnerableEnPassant)
                    {
                        mat[posLeft.row -1 , posLeft.col] = true;
                    }
                    Position posRight = new Position(position.row, position.col + 1);
                    if (b.isValidPosition(posRight) && existOpponent(posRight) && b.piece(posRight) == match.pieceVulnerableEnPassant)
                    {
                        mat[posRight.row - 1, posRight.col] = true;
                    }
                }
            }
            else
            {
                // 1 forward black
                pos.defineValuesPosition(position.row + 1, position.col);
                if (b.isValidPosition(pos) && empty(pos))
                {
                    mat[pos.row, pos.col] = true;
                }

                // 2 forward black
                pos.defineValuesPosition(position.row + 2, position.col);
                Position pos2 = new Position(position.row + 1, position.col);
                if (b.isValidPosition(pos2) && empty(pos2) && b.isValidPosition(pos) && empty(pos) && movAmount == 0)
                {
                    mat[pos.row, pos.col] = true;
                }

                // capture right black
                pos.defineValuesPosition(position.row + 1, position.col - 1);
                if (b.isValidPosition(pos) && existOpponent(pos))
                {
                    mat[pos.row, pos.col] = true;
                }

                // capture left black
                pos.defineValuesPosition(position.row + 1, position.col - 1);
                if (b.isValidPosition(pos) && existOpponent(pos))
                {
                    mat[pos.row, pos.col] = true;
                }

                // #special movement en passant black
                if (position.row == 4)
                {
                    Position posLeft = new Position(position.row, position.col + 1);
                    if (b.isValidPosition(posLeft) && existOpponent(posLeft) && b.piece(posLeft) == match.pieceVulnerableEnPassant)
                    {
                        mat[posLeft.row + 1, posLeft.col] = true;
                    }
                    Position posRight = new Position(position.row, position.col - 1);
                    if (b.isValidPosition(posRight) && existOpponent(posRight) && b.piece(posRight) == match.pieceVulnerableEnPassant)
                    {
                        mat[posRight.row + 1, posRight.col] = true;
                    }
                }
            }
            return mat;
        }
    }
}
