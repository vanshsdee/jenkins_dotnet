using System;

class Program
{
    class Robot
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public string Direction { get; private set; }
        public bool IsPlaced { get; private set; }
        private const int MapSize = 10;  // Static 10x10 grid

        public Robot()
        {
            IsPlaced = false;
        }

        public bool Place(int x, int y, string direction)
        {
            if (x >= 0 && x < MapSize && y >= 0 && y < MapSize &&
                (direction == "NORTH" || direction == "EAST" || direction == "SOUTH" || direction == "WEST"))
            {
                X = x;
                Y = y;
                Direction = direction;
                IsPlaced = true;
                return true;
            }
            return false;
        }

        public void Left()
        {
            if (!IsPlaced) return;

            if (Direction == "NORTH")
                Direction = "WEST";
            else if (Direction == "SOUTH")
                Direction = "EAST";
            else if (Direction == "WEST")
                Direction = "SOUTH";
            else if (Direction == "EAST")
                Direction = "NORTH";
        }

        public void Right()
        {
            if (!IsPlaced) return;

            if (Direction == "NORTH")
                Direction = "EAST";
            else if (Direction == "SOUTH")
                Direction = "WEST";
            else if (Direction == "WEST")
                Direction = "NORTH";
            else if (Direction == "EAST")
                Direction = "SOUTH";
        }

        public bool Move()
        {
            if (!IsPlaced) return false;

            int newX = X;
            int newY = Y;

            switch (Direction)
            {
                case "NORTH":
                    newY--;
                    break;

                case "SOUTH":
                    newY++;
                    break;

                case "EAST":
                    newX++;
                    break;

                case "WEST":
                    newX--;
                    break;
            }

            if (newX >= 0 && newX < MapSize && newY >= 0 && newY < MapSize)
            {
                X = newX;
                Y = newY;
                return true;
            }
            return false;
        }

        public void DisplayMap()
        {
            for (int y = 0; y < MapSize; y++)
            {
                for (int x = 0; x < MapSize; x++)
                {
                    if (IsPlaced && X == x && Y == y)
                    {
                        // Displays the Robot with direction 
                        char robotSymbol;

                        if (Direction == "NORTH")
                            robotSymbol = '^';

                        else if (Direction == "SOUTH")
                            robotSymbol = 'v';

                        else if (Direction == "EAST")
                            robotSymbol = '>';

                        else
                            robotSymbol = '<';

                        Console.Write($"{robotSymbol} ");
                    }
                    else
                    {
                        Console.Write("0 ");  // empty cell 
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }

    static void Main(string[] args)
    {
        Robot robot = new Robot();
        Console.WriteLine("\t\t\t\t***************************");
        Console.WriteLine("\t\t\t\t** MOON ROBOT SIMULATION **");
        Console.WriteLine("\t\t\t\t***************************");

        Console.WriteLine("Commands: \n1. PLACE X,Y,DIRECTION \n2. MOVE\n3. LEFT\n4. RIGHT\n5. EXIT");
        Console.WriteLine("Example of command: PLACE 0,0,NORTH or PLACE 9,9,SOUTH");
        Console.WriteLine("Robot Direction Symbols: ^ (NORTH), v (SOUTH), > (EAST), < (WEST)");

        robot.DisplayMap();  // Show Moon Map Without Robot

        while (true)
        {
            Console.Write("\nEnter command: ");

            string input = Console.ReadLine();
            if (input != null)
            {
                input = input.Trim();
                input = input.ToUpper();
            }

            if (input == "EXIT")
                break;

            if (input.StartsWith("PLACE "))
            {
                string coordinates = input.Substring(6);
                string[] coordinates_value = coordinates.Split(',');

                if (coordinates_value.Length == 3)
                {
                    // get first part or X coordinate
                    bool ValueX = int.TryParse(coordinates_value[0], out int x);

                    // get second part or Y coordinate
                    bool ValueY = int.TryParse(coordinates_value[1], out int y);

                    if (ValueX && ValueY)
                    {
                        // Get the direction 
                        string direction = coordinates_value[2].Trim();

                        // place the robot 
                        bool robotPlacement = robot.Place(x, y, direction);

                        // If given coordinates are not on grid. bottom right corner coordinates - (9,9)
                        if (!robotPlacement)
                        {
                            Console.WriteLine("Invalid placement");
                        }
                    }
                    else
                    {
                        Console.WriteLine("X and Y must be valid numbers");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid PLACE command format. Write in this format only: PLACE X,Y,DIRECTION");
                }
            }
            else if (!robot.IsPlaced)
            {
                Console.WriteLine("Robot must be PLACED first");
            }

            else
            {
                switch (input)
                {
                    case "MOVE":
                        if (!robot.Move())          //calls robot.Move method and if it returns false then display the message
                            Console.WriteLine("Move blocked - Would fall off the map!");
                        break;
                    case "LEFT":
                        robot.Left();
                        break;
                    case "RIGHT":
                        robot.Right();
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }

            robot.DisplayMap();  // Show updated map after every command
        }
    }
}