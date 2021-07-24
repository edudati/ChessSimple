using System.Collections.Generic;
using board;

namespace chessMatch
{
    class ChessMatch
    {
        public Board b { get; private set; }
        public int shift { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool check { get; private set; }

        public ChessMatch()
        {
            b = new Board(8, 8);
            shift = 1;
            currentPlayer = Color.White;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            check = false;
            finished = false;
            putPiecesOnTheBoard();
        }

        public Piece performMov(Position origin, Position destination)
        {
            Piece p = b.removePiece(origin);
            p.addMovAmount();
            Piece capturedPiece = b.removePiece(destination);
            b.putPiece(p, destination);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
            return capturedPiece;
        }
        public void undoMovement(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = b.removePiece(destination);
            p.subtractMovAmount();
            if(capturedPiece != null)
            {
                b.putPiece(capturedPiece, destination);
                captured.Remove(capturedPiece);
            }
            b.putPiece(p, origin);
        }

        public void executeAllMove(Position origin, Position destination)
        {
            Piece capturedPiece = performMov(origin, destination);
            if (isInCheck(currentPlayer))
            {
                undoMovement(origin, destination, capturedPiece);
                throw new BoardException("You do not put yourself in Check!");
            }

            if (isInCheck(opponent(currentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (testCheckMate(opponent(currentPlayer)))
            {
                finished = true;
            }
            else
            {
                shift++;
                changePlayer(currentPlayer);
            }

            
        }

       

        public void validateOriginPosition(Position origin)
        {
            if (b.piece(origin) == null)
            {
                throw new BoardException("This place is empty!");
            }
            if (currentPlayer != b.piece(origin).color)
            {
                throw new BoardException("This is not your piece!");
            }
            if (!b.piece(origin).existPossibleMov())
            {
                throw new BoardException("There is no possible movements for the chossen piece!");
            }
        }

        public void validateDestinationPosition(Position origin, Position destination)
        {
            if (!b.piece(origin).canMoveTo(destination))
            {
                throw new BoardException("Invalid place of destination!");
            }
        }

        public void changePlayer(Color currentPlayer)
        {
            if (currentPlayer == Color.White)
            {
                this.currentPlayer = Color.Black;
            }
            else
            {
                this.currentPlayer = Color.White;
            }
        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in captured)
            {
                if (p.color == color)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }

        public HashSet<Piece> piecesInPlay(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in pieces)
            {
                if (p.color == color)
                {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        private Color opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            return Color.White;
        }

       // return the position of the King
        private Piece king(Color color)
        {
            foreach (Piece p in piecesInPlay(color))
            {
                if (p is King)
                {
                    return p;
                }
            }
            return null;
        }

        // verify if the King is in check. Set the position of the king and run through whole possibles moviments for all pieces in play. If true, the king is in check.
        public bool isInCheck(Color color)
        {
            foreach(Piece p in piecesInPlay(opponent(color)))
            {
                Piece k = king(color);
                bool[,] mat = p.possiblesMovs();
                if (mat[k.position.row, k.position.col])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testCheckMate(Color color)
        {
            if (!isInCheck(color))
            {
                return false;
            }
            foreach (Piece p in piecesInPlay(color))
            {
                bool[,] mat = p.possiblesMovs();
                for (int i = 0; i < b.rows; i++)
                {
                    for (int j = 0; j < b.cols; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = p.position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = performMov(origin, destination);
                            bool testCheck = isInCheck(color);
                            undoMovement(origin, destination, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }


        private void putNewPiece(Piece piece, char col, int row)
        {
            b.putPiece(piece, new PositionOfChess(col, row).ToPosition());
            pieces.Add(piece);
        }
        private void putPiecesOnTheBoard()
        {
            /*putNewPiece(new Rock(b, Color.Black), 'c', 8);
            putNewPiece(new Rock(b, Color.Black), 'c', 7);
            putNewPiece(new King(b, Color.Black), 'd', 8);
            putNewPiece(new Rock(b, Color.Black), 'd', 7);
            putNewPiece(new Rock(b, Color.Black), 'e', 8);
            putNewPiece(new Rock(b, Color.Black), 'e', 7);

            putNewPiece(new Rock(b, Color.White), 'c', 1);
            putNewPiece(new Rock(b, Color.White), 'c', 2);
            putNewPiece(new King(b, Color.White), 'd', 1);
            putNewPiece(new Rock(b, Color.White), 'd', 2);
            putNewPiece(new Rock(b, Color.White), 'e', 1);
            putNewPiece(new Rock(b, Color.White), 'e', 2);*/

            /*checkmate situation*/
            putNewPiece(new King(b, Color.Black), 'a', 8);
            putNewPiece(new Rock(b, Color.Black), 'b', 8);

            putNewPiece(new Rock(b, Color.White), 'h', 7);
            putNewPiece(new Rock(b, Color.White), 'b', 1);
            putNewPiece(new King(b, Color.White), 'd', 1);

        }
    }
}
