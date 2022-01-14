using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Battleship.GameController.Contracts;
using NAudio.Wave;

namespace Battleship.Ascii
{
    public class Program
    {

        #region Declarations --------------------------------------------------

        private static List<Ship> myFleet;

        private static List<Ship> enemyFleet;

        private static readonly Grid grid = new Grid(10);

        #endregion


        #region Constructors --------------------------------------------------


        private static List<Position> enemyShootPositions = new List<Position>();

        static void Main()
        {
            Console.Title = "Battleship";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.WriteLine("                                     |__");
            Console.WriteLine(@"                                     |\/");
            Console.WriteLine("                                     ---");
            Console.WriteLine("                                     / | [");
            Console.WriteLine("                              !      | |||");
            Console.WriteLine("                            _/|     _/|-++'");
            Console.WriteLine("                        +  +--|    |--|--|_ |-");
            Console.WriteLine(@"                     { /|__|  |/\__|  |--- |||__/");
            Console.WriteLine(@"                    +---------------___[}-_===_.'____                 /\");
            Console.WriteLine(@"                ____`-' ||___-{]_| _[}-  |     |_[___\==--            \/   _");
            Console.WriteLine(@" __..._____--==/___]_|__|_____________________________[___\==--____,------' .7");
            Console.WriteLine(@"|                        Welcome to Battleship                         BB-61/");
            Console.WriteLine(@" \_________________________________________________________________________|");
            Console.WriteLine();

            InitializeGame();

            StartGame();
        }

        #endregion


        #region Public Methods ------------------------------------------------

        public static Position ParsePosition(string input)
        {
            bool isPositionValid;
            string letter = input;
            int number;

            //do
            //{
                letter = input.ToUpper().Substring(0, 1);
                int.TryParse(input.Substring(1), out number);

                isPositionValid = Enum.IsDefined(typeof(Letters), letter) && number >= 1 && number <= grid.Rows;
            if(!isPositionValid)
                throw new ArgumentException("Bruh");

            //    if (!isPositionValid)
            //    {
            //        Console.WriteLine("Coordinates are invalid!");
            //        Console.WriteLine("Enter coordinates for your shot :");
            //        input = Console.ReadLine();
            //    }
            //}
            //while (!isPositionValid);

            return new Position((Letters)Enum.Parse(typeof(Letters), letter), number);
        }

        #endregion


        #region Private/Protected Methods -------------------------------------

        private static void StartGame()
        {
            
                
                //Console.Clear();
                Console.WriteLine(@"                   __");
                Console.WriteLine(@"                  /  \");
                Console.WriteLine(@"            .-.  |    |");
                Console.WriteLine(@"   *    _.-'  \   \__/");
                Console.WriteLine(@"    \.-'       \");
                Console.WriteLine(@"   /          _/");
                Console.WriteLine(@"  |      _  /""");
                Console.WriteLine(@"  |     /_\'");
                Console.WriteLine(@"   \    \_/");
                Console.WriteLine(@"    """"""""");

                do
                {
                    using (var waveOut = new WaveOutEvent())
                        using (var wavReader = new WaveFileReader(@"Explosion.wav"))
                        {
                            waveOut.Init(wavReader);
                            waveOut.Volume = 0.5f;
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("**************************");
                            Console.WriteLine("**Player, it's your turn**");
                            Console.WriteLine("**************************");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("");
                            Console.WriteLine("Enter coordinates for your shot :");

                            string input = Console.ReadLine();
                            Position position = null;

                            bool isValid = false;

                            while (isValid == false)
                            {
                                try
                                {
                                    position = ParsePosition(input);
                                    isValid = true;
                                }
                                catch
                                {
                                    Console.WriteLine("The value you entered is invalid. Please enter a valid position : ");
                                    input = Console.ReadLine();
                                }
                            }


                            (bool isHit, Ship hitShip) = GameController.GameController.CheckIsHit(enemyFleet, position);
                            if (isHit)
                            {
                                //Console.Beep();
                                //Console.WriteLine(@"                \         .  ./");
                                //Console.WriteLine(@"              \      .:"";'.:..""   /");
                                //Console.WriteLine(@"                  (M^^.^~~:.'"").");
                                //Console.WriteLine(@"            -   (/  .    . . \ \)  -");
                                //Console.WriteLine(@"               ((| :. ~ ^  :. .|))");
                                //Console.WriteLine(@"            -   (\- |  \ /  |  /)  -");
                                //Console.WriteLine(@"                 -\  \     /  /-");
                                //Console.WriteLine(@"                   \  \   /  /");

                                Console.WriteLine(@"     _.-^^---....,,--       ");
                                Console.WriteLine(@" _--                  --_  ");
                                Console.WriteLine(@"<                        >)");
                                Console.WriteLine(@"|                         | ");
                                Console.WriteLine(@" \._                   _./  ");
                                Console.WriteLine(@"    ```--. . , ; .--'''       ");
                                Console.WriteLine(@"          | |   |             ");
                                Console.WriteLine(@"       .-=||  | |=-.   ");
                                Console.WriteLine(@"       `-=#$%&%$#=-'   ");
                                Console.WriteLine(@"          | ;  :|     ");
                                Console.WriteLine(@"_____.,-#%&$@%#&#~,._____");

                                waveOut.Play();
                                Thread.Sleep(2000);
                                waveOut.Stop();

                                if (hitShip != null && !hitShip.Positions.Any())
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine(@"You sunk your opponent's {0}!", hitShip.Name);
                                    Console.WriteLine(@"");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                    
                                if (!enemyFleet[0].Positions.Any() && !enemyFleet[1].Positions.Any() && !enemyFleet[2].Positions.Any() && !enemyFleet[3].Positions.Any() && !enemyFleet[4].Positions.Any())
                                {
                                    Console.WriteLine(@"");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine(@"**************************");
                                    Console.WriteLine(@"**************************");
                                    Console.WriteLine(@"**************************");
                                    Console.WriteLine(@"****You are the Winner****");
                                    Console.WriteLine(@"**************************");
                                    Console.WriteLine(@"**************************");
                                    Console.WriteLine(@"**************************");
                                    return;
                                }
                            }

                            Console.WriteLine("");
                            if (!isHit)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("Miss");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Yeah ! Nice hit !");
                            }

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("");

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("*****************************");
                            Console.WriteLine("**It's your opponent's turn**");
                            Console.WriteLine("*****************************");
                            Console.ForegroundColor = ConsoleColor.White;
                            do
                            {
                                position = GetRandomPosition();

                            }
                            while (enemyShootPositions.Contains(position));

                            enemyShootPositions.Add(position);

                            (isHit, hitShip) = GameController.GameController.CheckIsHit(myFleet, position);
                            Console.WriteLine();

                            if (isHit)
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            else
                                Console.ForegroundColor = ConsoleColor.Blue;

                            Console.WriteLine("Opponent shot in {0}{1} and {2}", position.Column, position.Row, isHit ? "has hit your ship !" : "miss");
                            Console.ForegroundColor = ConsoleColor.White;

                            if (isHit)
                            {
                                //Console.Beep();
                                //Console.WriteLine(@"                \         .  ./");
                                //Console.WriteLine(@"              \      .:"";'.:..""   /");
                                //Console.WriteLine(@"                  (M^^.^~~:.'"").");
                                //Console.WriteLine(@"            -   (/  .    . . \ \)  -");
                                //Console.WriteLine(@"               ((| :. ~ ^  :. .|))");
                                //Console.WriteLine(@"            -   (\- |  \ /  |  /)  -");
                                //Console.WriteLine(@"                 -\  \     /  /-");
                                //Console.WriteLine(@"                   \  \   /  /");

                                Console.WriteLine(@"     _.-^^---....,,--       ");
                                Console.WriteLine(@" _--                  --_  ");
                                Console.WriteLine(@"<                        >)");
                                Console.WriteLine(@"|                         | ");
                                Console.WriteLine(@" \._                   _./  ");
                                Console.WriteLine(@"    ```--. . , ; .--'''       ");
                                Console.WriteLine(@"          | |   |             ");
                                Console.WriteLine(@"       .-=||  | |=-.   ");
                                Console.WriteLine(@"       `-=#$%&%$#=-'   ");
                                Console.WriteLine(@"          | ;  :|     ");
                                Console.WriteLine(@"_____.,-#%&$@%#&#~,._____");

                                waveOut.Play();
                                Thread.Sleep(2000);
                                waveOut.Stop();

                                if (hitShip != null && !hitShip.Positions.Any())
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(@"");
                                    Console.WriteLine(@"You opponent sunk your {0}!", hitShip.Name);
                                    Console.ForegroundColor = ConsoleColor.White;
                                }

                                if (!myFleet[0].Positions.Any() && !myFleet[1].Positions.Any() && !myFleet[2].Positions.Any() && !myFleet[3].Positions.Any() && !myFleet[4].Positions.Any())
                                {
                                    Console.WriteLine(@"");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(@"**************************");
                                    Console.WriteLine(@"**************************");
                                    Console.WriteLine(@"**************************");
                                    Console.WriteLine(@"****Your opponent wins****");
                                    Console.WriteLine(@"**************************");
                                    Console.WriteLine(@"**************************");
                                    Console.WriteLine(@"**************************");
                                    return;
                                }
                            }
                        }
                }
                while (true);
            
        }


        private static Position GetRandomPosition()
        {

            int rows = 10;
            int lines = 10;
            var random = new Random();
            var letter = (Letters)random.Next(lines);
            int number = random.Next(1, rows + 1);
            var position = new Position(letter, number);
            return position;
        }

        private static void InitializeGame()
        {
            InitializeMyFleet();

            InitializeEnemyFleet();
        }

        private static void InitializeMyFleet()
        {
            myFleet = GameController.GameController.InitializeShips().ToList();

            Console.WriteLine("Please position your fleet (Game board size is from A to J and 1 to 10) :");

            foreach (Ship ship in myFleet)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter the starting position for the {0} (size: {1}) : ", ship.Name, ship.Size);

                string position1 = Console.ReadLine();
                HandleInput(position1, ship, 1);

                Console.WriteLine("Please enter the second position for the {0} (size: {1}) : ", ship.Name, ship.Size);

                string position2 = Console.ReadLine();
                HandleInput(position2, ship, 2);
            }
        }

        private static void HandleInput(string position, Ship ship, int positionIndex)
        {
            bool isValid = false;

            while (isValid == false)
            {
                try
                {
                    if (positionIndex == 1 && IsStartingPositionValid(position) || positionIndex == 2 && IsSecondPositionValid(ship, position))
                    {
                        isValid = true;
                        ship.AddPosition(position);

                        if (positionIndex == 2) //We need to add the rest of the ship
                        {
                            Position position1 = ship.Positions[0];
                            Position position2 = ship.Positions[1];

                            int rowDelta = position2.Row - position1.Row;
                            int columnDelta = position2.Column - position1.Column;

                            Position lastInsertedPosition = position2;

                            for (int i = positionIndex; i <= ship.Size; i++)
                            {
                                var newPosition = new Position(lastInsertedPosition.Column + columnDelta, lastInsertedPosition.Row + rowDelta);
                                ship.Positions.Add(newPosition);
                                lastInsertedPosition = newPosition;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The value you entered is invalid. Please enter a valid position : ");
                        position = Console.ReadLine();
                    }
                }
                catch
                {
                    Console.WriteLine("The value you entered is invalid. Please enter a valid position : ");
                    position = Console.ReadLine();
                }
            }
        }

        private static bool IsStartingPositionValid(string input)
        {
            Position position = ParsePosition(input);

            return position.Row >= 1 && position.Row <= 10 && !myFleet.SelectMany(x => x.Positions).Contains(position);
        }

        private static bool IsSecondPositionValid(Ship ship, string input)
        {
            Position position1 = ship.Positions.First();

            //Get all valid positions
            var validPositions = new List<Position>();

            var up = new Position(position1.Column, position1.Row - 1);
            var down = new Position(position1.Column, position1.Row + 1);
            var left = new Position(position1.Column - 1, position1.Row);
            var right = new Position(position1.Column + 1, position1.Row);

            if (position1.Row - ship.Size >= 0 && !myFleet.SelectMany(x => x.Positions).Contains(up)) //up
                validPositions.Add(up);

            if (position1.Row + ship.Size <= 10 && !myFleet.SelectMany(x => x.Positions).Contains(down)) // down
                validPositions.Add(down);

            if (position1.Column - ship.Size >= 0 && !myFleet.SelectMany(x => x.Positions).Contains(left)) //left
                validPositions.Add(left);

            if (position1.Column + ship.Size <= Letters.J && !myFleet.SelectMany(x => x.Positions).Contains(right)) //right
                validPositions.Add(right);

            Position position = ParsePosition(input);

            return validPositions.Contains(position);
        }

        private static void InitializeEnemyFleet()
        {
            enemyFleet = GameController.GameController.InitializeShips().ToList();
            var configNum = (new Random().Next(1, 5));

            switch (configNum)
            {
                case 1:
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 4 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 5 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 6 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 7 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 8 });

                    enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 5 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 6 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 7 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 8 });

                    enemyFleet[2].Positions.Add(new Position { Column = Letters.A, Row = 3 });
                    enemyFleet[2].Positions.Add(new Position { Column = Letters.B, Row = 3 });
                    enemyFleet[2].Positions.Add(new Position { Column = Letters.C, Row = 3 });

                    enemyFleet[3].Positions.Add(new Position { Column = Letters.F, Row = 8 });
                    enemyFleet[3].Positions.Add(new Position { Column = Letters.G, Row = 8 });
                    enemyFleet[3].Positions.Add(new Position { Column = Letters.H, Row = 8 });

                    enemyFleet[4].Positions.Add(new Position { Column = Letters.C, Row = 5 });
                    enemyFleet[4].Positions.Add(new Position { Column = Letters.C, Row = 6 });
                    break;
                case 2:
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.C, Row = 2 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.C, Row = 3 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.C, Row = 4 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.C, Row = 5 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.C, Row = 6 });

                    enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 3 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 4 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 5 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 6 });

                    enemyFleet[2].Positions.Add(new Position { Column = Letters.D, Row = 7 });
                    enemyFleet[2].Positions.Add(new Position { Column = Letters.E, Row = 7 });
                    enemyFleet[2].Positions.Add(new Position { Column = Letters.F, Row = 7 });

                    enemyFleet[3].Positions.Add(new Position { Column = Letters.A, Row = 8 });
                    enemyFleet[3].Positions.Add(new Position { Column = Letters.B, Row = 8 });
                    enemyFleet[3].Positions.Add(new Position { Column = Letters.C, Row = 8 });

                    enemyFleet[4].Positions.Add(new Position { Column = Letters.D, Row = 5 });
                    enemyFleet[4].Positions.Add(new Position { Column = Letters.D, Row = 6 });
                    break;
                case 3:
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.A, Row = 1 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.A, Row = 2 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.A, Row = 3 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.A, Row = 4 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.A, Row = 5 });

                    enemyFleet[1].Positions.Add(new Position { Column = Letters.C, Row = 1 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.C, Row = 2 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.C, Row = 3 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.C, Row = 4 });

                    enemyFleet[2].Positions.Add(new Position { Column = Letters.D, Row = 5 });
                    enemyFleet[2].Positions.Add(new Position { Column = Letters.E, Row = 5 });
                    enemyFleet[2].Positions.Add(new Position { Column = Letters.F, Row = 5 });

                    enemyFleet[3].Positions.Add(new Position { Column = Letters.E, Row = 1 });
                    enemyFleet[3].Positions.Add(new Position { Column = Letters.F, Row = 1 });
                    enemyFleet[3].Positions.Add(new Position { Column = Letters.G, Row = 1 });

                    enemyFleet[4].Positions.Add(new Position { Column = Letters.F, Row = 5 });
                    enemyFleet[4].Positions.Add(new Position { Column = Letters.F, Row = 6 });
                    break;
                case 4:
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.D, Row = 4 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.D, Row = 5 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.D, Row = 6 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.D, Row = 7 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.D, Row = 8 });

                    enemyFleet[1].Positions.Add(new Position { Column = Letters.A, Row = 5 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.A, Row = 6 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.A, Row = 7 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.A, Row = 8 });

                    enemyFleet[2].Positions.Add(new Position { Column = Letters.D, Row = 3 });
                    enemyFleet[2].Positions.Add(new Position { Column = Letters.E, Row = 3 });
                    enemyFleet[2].Positions.Add(new Position { Column = Letters.F, Row = 3 });

                    enemyFleet[3].Positions.Add(new Position { Column = Letters.B, Row = 2 });
                    enemyFleet[3].Positions.Add(new Position { Column = Letters.C, Row = 2 });
                    enemyFleet[3].Positions.Add(new Position { Column = Letters.D, Row = 2 });

                    enemyFleet[4].Positions.Add(new Position { Column = Letters.F, Row = 5 });
                    enemyFleet[4].Positions.Add(new Position { Column = Letters.F, Row = 6 });
                    break;
                case 5:
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 3 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 4 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 5 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 6 });
                    enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 7 });

                    enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 4 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 5 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 6 });
                    enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 7 });

                    enemyFleet[2].Positions.Add(new Position { Column = Letters.A, Row = 2 });
                    enemyFleet[2].Positions.Add(new Position { Column = Letters.B, Row = 2 });
                    enemyFleet[2].Positions.Add(new Position { Column = Letters.C, Row = 2 });

                    enemyFleet[3].Positions.Add(new Position { Column = Letters.F, Row = 7 });
                    enemyFleet[3].Positions.Add(new Position { Column = Letters.G, Row = 7 });
                    enemyFleet[3].Positions.Add(new Position { Column = Letters.H, Row = 7 });

                    enemyFleet[4].Positions.Add(new Position { Column = Letters.C, Row = 4 });
                    enemyFleet[4].Positions.Add(new Position { Column = Letters.C, Row = 5 });
                    break;
            }

            // Console.WriteLine($"Config #{configNum}");
            // for (var i = 0; i < enemyFleet.Count; i++)
            // {
            //     Console.WriteLine($"Enemy Fleet #{i + 1}: {enemyFleet[i].Positions[0].Column}{enemyFleet[i].Positions[0].Row}");
            // }
        }

        #endregion

    }
}
