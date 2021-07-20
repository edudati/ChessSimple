namespace board
{
    class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public Board b { get; protected set; }
        public int movAmount { get; protected set; }

        public Piece(Position position, Board b, Color color)
        {
            this.position = position;
            this.b = b;
            this.color = color;
            movAmount = 0;
        }
    }
}
