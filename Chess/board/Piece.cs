namespace board
{
    class Piece
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
    }
}
