namespace Battleship.GameController.Contracts
{
    public class Grid
    {
        public Grid(int rows)
        {
            Rows = rows;
        }

        public Letters Columns { get; }
        public int Rows { get; set; }
    }
}
