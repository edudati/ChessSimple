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
        private HashSet<Piece> captureds;
        public bool check { get; private set; }
        public Piece pieceVulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            b = new Board(8, 8);
            shift = 1;
            currentPlayer = Color.White;
            pieces = new HashSet<Piece>();
            captureds = new HashSet<Piece>();
            check = false;
            finished = false;
            pieceVulnerableEnPassant = null;
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
                captureds.Add(capturedPiece);
            }

            // #special movement Castling SHORT
            if (p is King && destination.col == origin.col + 2)
            {
                Position originRock = new Position(origin.row, origin.col + 3);
                Position destinationRock = new Position(origin.row, origin.col + 1);
                Piece pRock = b.removePiece(originRock);
                pRock.addMovAmount();
                b.putPiece(pRock, destinationRock);
            }

            // #special movement Castling LONG
            if (p is King && destination.col == origin.col - 2)
            {
                Position originRock = new Position(origin.row, origin.col - 4);
                Position destinationRock = new Position(origin.row, origin.col - 1);
                Piece pRock = b.removePiece(originRock);
                pRock.addMovAmount();
                b.putPiece(pRock, destinationRock);
            }

             //#special movement en passant
            if (p is Pawn)
            {
                // if Pawn moves in diagonal and did not had a captured piece
                if(origin.col != destination.col && capturedPiece == null)
                {
                    Position posPawn;
                    if (p.color == Color.White)
                    {
                        posPawn = new Position(destination.row + 1, destination.col);
                    }
                    else
                    {
                        posPawn = new Position(destination.row - 1, destination.col);
                    }
                    capturedPiece = b.removePiece(posPawn);
                    captureds.Add(capturedPiece);
                }
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
                captureds.Remove(capturedPiece);
            }
            b.putPiece(p, origin);

            // #special movement UNDO Castling SHORT
            if (p is King && destination.col == origin.col + 2)
            {
                Position originRock = new Position(origin.row, origin.col + 3);
                Position destinationRock = new Position(origin.row, origin.col + 1);
                Piece pRock = b.removePiece(destinationRock);
                pRock.subtractMovAmount();
                b.putPiece(pRock, originRock);
            }

            // #special movement UNDO Castling LONG
            if (p is King && destination.col == origin.col - 2)
            {
                Position originRock = new Position(origin.row, origin.col - 4);
                Position destinationRock = new Position(origin.row, origin.col - 1);
                Piece pRock = b.removePiece(destinationRock);
                pRock.subtractMovAmount();
                b.putPiece(pRock, originRock);
            }

            // #special movement UNDO en passant
            if (p is Pawn)
            {
                // if Pawn moves in diagonal and did not had a captured piece it was an el passant
                if (origin.col != destination.col && capturedPiece == pieceVulnerableEnPassant)
                {
                    // remove piece from wrong place performed by undoMov (couse it was an el passant)
                    Piece pawn = b.removePiece(destination);
                    Position posCapturedPawn;
                    if (p.color == Color.White)
                    {
                        posCapturedPawn = new Position(3, destination.col);
                    }
                    else
                    {
                        posCapturedPawn = new Position(4, destination.col);
                    }
                    b.putPiece(pawn, posCapturedPawn);
                }
            }
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

            Piece p = b.piece(destination);

            //# special movement promotion
            if( p is Pawn)
            {
                if ((p.color == Color.White && destination.row == 0) || (p.color == Color.Black && destination.row == 7))
                {
                    p = b.removePiece(destination);
                    pieces.Remove(p);
                    Piece queen = new Queen(b, p.color);
                    b.putPiece(queen, destination);
                    pieces.Add(queen);
                }
            }



            // # special movement en passant

            if (p is Pawn && (destination.row == origin.row - 2 || destination.row == origin.row + 2))
                {
                    pieceVulnerableEnPassant = p;
                }
                else
                {
                    pieceVulnerableEnPassant = null;
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
            foreach (Piece p in captureds)
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
            putNewPiece(new Pawn(b, Color.Black, this), 'a', 7);
            putNewPiece(new Pawn(b, Color.Black, this), 'b', 7);
            putNewPiece(new Pawn(b, Color.Black, this), 'c', 7);
            putNewPiece(new Pawn(b, Color.Black, this), 'd', 7);
            putNewPiece(new Pawn(b, Color.Black, this), 'e', 7);
            putNewPiece(new Pawn(b, Color.Black, this), 'f', 7);
            putNewPiece(new Pawn(b, Color.Black, this), 'g', 7);
            putNewPiece(new Pawn(b, Color.Black, this), 'h', 7);

            putNewPiece(new Rock(b, Color.Black), 'a', 8);
            putNewPiece(new Horse(b, Color.Black), 'b', 8);
            putNewPiece(new Bishop(b, Color.Black), 'c', 8);
            putNewPiece(new Queen(b, Color.Black), 'd', 8);
            putNewPiece(new King(b, Color.Black, this), 'e', 8);
            putNewPiece(new Bishop(b, Color.Black), 'f', 8);
            putNewPiece(new Horse(b, Color.Black), 'g', 8);
            putNewPiece(new Rock(b, Color.Black), 'h', 8);


            putNewPiece(new Pawn(b, Color.White, this), 'a', 2);
            putNewPiece(new Pawn(b, Color.White, this), 'b', 2);
            putNewPiece(new Pawn(b, Color.White, this), 'c', 2);
            putNewPiece(new Pawn(b, Color.White, this), 'd', 2);
            putNewPiece(new Pawn(b, Color.White, this), 'e', 2);
            putNewPiece(new Pawn(b, Color.White, this), 'f', 2);
            putNewPiece(new Pawn(b, Color.White, this), 'g', 2);
            putNewPiece(new Pawn(b, Color.White, this), 'h', 2);


            putNewPiece(new Rock(b, Color.White), 'a', 1);
            putNewPiece(new Horse(b, Color.White), 'b', 1);
            putNewPiece(new Bishop(b, Color.White), 'c', 1);
            putNewPiece(new Queen(b, Color.White), 'd', 1);
            putNewPiece(new King(b, Color.White, this), 'e', 1);
            putNewPiece(new Bishop(b, Color.White), 'f', 1);
            putNewPiece(new Horse(b, Color.White), 'g', 1);
            putNewPiece(new Rock(b, Color.White), 'h', 1);

            /*checkmate situation
            putNewPiece(new King(b, Color.Black), 'a', 8);
            putNewPiece(new Rock(b, Color.Black), 'b', 8);

            putNewPiece(new Rock(b, Color.White), 'h', 7);
            putNewPiece(new Rock(b, Color.White), 'b', 1);
            putNewPiece(new King(b, Color.White), 'd', 1);
            */

        }
    }
}
