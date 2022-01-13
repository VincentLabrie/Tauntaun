namespace Battleship.GameController.Contracts
{
    public class Grid
    {
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }

        public int Columns { get; set; }
        public int Rows { get; set; }
    }
}
