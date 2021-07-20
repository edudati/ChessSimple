namespace board
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

        // Allow access to other classes to private piece from a Position... return an especific piece on the array
        public Piece piece(Position pos)
        {
            return pieces[pos.row, pos.col];
        }

        // put one piece on the board (array of pieces) and put position to the piece p, 
        public void putPiece(Piece p, Position pos)
        {
            //exception if exist some piece in the position
            if (existPiece(pos))
            {
                throw new BoardException("There is another piece in the same place.");
            }
            pieces[pos.row, pos.col] = p;
            p.position = pos;   
        }

        // This method validate if the position is valid and check is exist a piece in this position
        public bool existPiece(Position pos)
        {
            validatePosition(pos);
            return piece(pos) != null;
        }
        
        // Check if the position is valid
        public bool isValidPosition(Position pos)
        {
            if (pos.row < 0 || pos.row >= rows || pos.col < 0 || pos.col >= cols)
            {
                return false;
            }
            return true;
        }

        //validate if is a valid position
        public void validatePosition(Position pos)
        {
            if (!isValidPosition(pos))
            {
                throw new BoardException("Invalid position.");
            }
        }
    }
}
