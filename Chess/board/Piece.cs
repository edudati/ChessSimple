namespace board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public Board b { get; protected set; }
        public int movAmount { get; protected set; }

        public Piece(Board b, Color color)
        {
            position = null;
            this.b = b;
            this.color = color;
            movAmount = 0;
        }

        public void addMovAmount()
        {
            movAmount++;
        }

        public void subtractMovAmount()
        {
            movAmount--;
        }

        public abstract bool[,] possiblesMovs();

        public bool existPossibleMov()
        {
            bool[,] mat = possiblesMovs();
            for (int i = 0; i < b.rows; i++)
            {
                for (int j = 0; j < b.cols; j++)
                {
                    if(mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool canMoveTo(Position pos)
        {
            return possiblesMovs()[pos.row, pos.col];
        }
    }
}
