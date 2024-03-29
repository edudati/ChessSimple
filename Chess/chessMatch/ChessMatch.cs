﻿using System.Collections.Generic;
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


        // Remove piece from original position, add a movement to the piece,
        // capture piece from destination and add to the list of captureds position if not empty, 
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

            // #SpecialMovement Castling (short)
            if (p is King && destination.col == origin.col + 2)
            {
                Position originRock = new Position(origin.row, origin.col + 3);
                Position destinationRock = new Position(origin.row, origin.col + 1);
                Piece pRock = b.removePiece(originRock);
                pRock.addMovAmount();
                b.putPiece(pRock, destinationRock);
            }

            // #SpecialMovement Castling (long)
            if (p is King && destination.col == origin.col - 2)
            {
                Position originRock = new Position(origin.row, origin.col - 4);
                Position destinationRock = new Position(origin.row, origin.col - 1);
                Piece pRock = b.removePiece(originRock);
                pRock.addMovAmount();
                b.putPiece(pRock, destinationRock);
            }

            // #SpecialMovement EnPassant
            if (p is Pawn)
            {
                // Is EnPassant if Pawn moves in diagonal and did not had a captured piece
                if (origin.col != destination.col && capturedPiece == null)
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

        // Undo method performMov 
        public void undoMov(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = b.removePiece(destination);
            p.subtractMovAmount();
            if (capturedPiece != null)
            {
                b.putPiece(capturedPiece, destination);
                captureds.Remove(capturedPiece);
            }
            b.putPiece(p, origin);

            // #SpecialMovement Castling (short)
            if (p is King && destination.col == origin.col + 2)
            {
                Position originRock = new Position(origin.row, origin.col + 3);
                Position destinationRock = new Position(origin.row, origin.col + 1);
                Piece pRock = b.removePiece(destinationRock);
                pRock.subtractMovAmount();
                b.putPiece(pRock, originRock);
            }

            // #SpecialMovement Castling (long)
            if (p is King && destination.col == origin.col - 2)
            {
                Position originRock = new Position(origin.row, origin.col - 4);
                Position destinationRock = new Position(origin.row, origin.col - 1);
                Piece pRock = b.removePiece(destinationRock);
                pRock.subtractMovAmount();
                b.putPiece(pRock, originRock);
            }

            // #SpecialMovement EnPassant
            if (p is Pawn)
            {
                if (origin.col != destination.col && capturedPiece == pieceVulnerableEnPassant)
                {
                    // Remove piece from wrong place performed by undoMov (couse it was an el passant)
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

        // Execute performMov and verify if is check, if will put the player hinself in check, if is checkmate, 
        public void executeFullMov(Position origin, Position destination)
        {
            Piece capturedPiece = performMov(origin, destination);
            if (isInCheck(currentPlayer))
            {
                undoMov(origin, destination, capturedPiece);
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

            // #SpecialMovement Promotion
            if (p is Pawn)
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

            // #SpecialMovement EnPassant
            if (p is Pawn && (destination.row == origin.row - 2 || destination.row == origin.row + 2))
            {
                pieceVulnerableEnPassant = p;
            }
            else
            {
                pieceVulnerableEnPassant = null;
            }
        }

        // Exceptions validating the original position
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

        // Exceptions validating the destination position
        public void validateDestinationPosition(Position origin, Position destination)
        {
            if (!b.piece(origin).canMoveTo(destination))
            {
                throw new BoardException("Invalid place of destination!");
            }
        }

        // Change the color of current player
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

        // Return a list with captured pieces by color.
        public HashSet<Piece> capturedPiecesByColor(Color color)
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

        // Return a list with pieces on the board by color
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
            aux.ExceptWith(capturedPiecesByColor(color));
            return aux;
        }

        // Return the opponent color
        private Color opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            return Color.White;
        }

        // return the piece King on the board of one specif color
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

        // Verify if the King is in check.
        // Set the position of the king and run through whole possibles moviments for all pieces in play.
        // If true, the king is in check.
        public bool isInCheck(Color color)
        {
            foreach (Piece p in piecesInPlay(opponent(color)))
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

        // Verify for Check in all possibles movements in all pieces on the board
        // If the piece remains in check in all possibles situations, it is chackmate!
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
                            undoMov(origin, destination, capturedPiece);
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

        // Put one piece on the board used in the first time and add the piece in the array pieces
        private void putNewPiece(Piece piece, char col, int row)
        {
            b.putPiece(piece, new PositionOfChess(col, row).ToPosition());
            pieces.Add(piece);
        }

        // Put all pieces in the start position
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

            /* Use for checkmate situation of Black pieces
            putNewPiece(new King(b, Color.Black), 'a', 8);
            putNewPiece(new Rock(b, Color.Black), 'b', 8);
            putNewPiece(new Rock(b, Color.White), 'h', 7);
            putNewPiece(new Rock(b, Color.White), 'b', 1);
            putNewPiece(new King(b, Color.White), 'd', 1);
            */
        }
    }
}
