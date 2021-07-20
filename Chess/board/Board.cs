﻿namespace board
{
    class Board
    {
        public int rows { get; set; }
        public int cols { get; set; }
        private Piece[,] pieces;

        public Board(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            pieces = new Piece[rows, cols];
        }

        // Allow access to other classes to private piece... return an especific piece on the array
        public Piece piece (int row, int col)
        {
            return pieces[row, col];
        }

        // put one piece on the board (array of pieces) and put position to the piece p
        public void putPiece(Piece p, Position pos)
        {
            pieces[pos.row, pos.col] = p;
            p.position = pos;   
        }
    }
}
