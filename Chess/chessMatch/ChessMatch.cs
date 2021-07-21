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

        public ChessMatch()
        {
            b = new Board(8, 8);
            shift = 1;
            currentPlayer = Color.White;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            finished = false;
            putPiecesOnTheBoard();
        }

        public void performMov(Position origin, Position destination)
        {
            Piece p = b.removePiece(origin);
            p.addMovAmount();
            Piece capturedPiece = b.removePiece(destination);
            b.putPiece(p, destination);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
        }

        public void executeAllMove(Position origin, Position destination)
        {
            performMov(origin, destination);
            shift++;
            changePlayer(currentPlayer);
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


        private void putNewPiece(Piece piece, char col, int row)
        {
            b.putPiece(piece, new PositionOfChess(col, row).ToPosition());
            pieces.Add(piece);
        }
        private void putPiecesOnTheBoard()
        {
            putNewPiece(new Rock(b, Color.Black), 'c', 8);
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
            putNewPiece(new Rock(b, Color.White), 'e', 2);

        }
    }
}
