using System;

namespace splatRunner
{
    public class Enemy : Character
    {
        private static Random velocityGenerator = new Random();
        Coordinates previousCoords;
        
        public override void WriteCharacterAtPosition()
        {
            var previousColour = Console.ForegroundColor;
            Console.ForegroundColor = Colour;

            previousCoords = position;

            DrawAtRandomPosition();

            Console.ForegroundColor = previousColour;
        }

        void DrawAtRandomPosition()
        {
            var nextCoords = NextCoords();

            if (nextCoords.x != previousCoords.x && nextCoords.y != previousCoords.y)
            {
                // best enemy name ever! enemyPoenemy1YonY
                EdgeLooper(nextCoords);
                Console.SetCursorPosition(nextCoords.x, nextCoords.y);
                Console.Write(symbol);

                position.y = nextCoords.y;
                position.x = nextCoords.x;
            }
            else
            {
                DrawAtRandomPosition();
            }
        }

        Coordinates NextCoords()
        {
            var nextCoords = new Coordinates();

            nextCoords.x = position.x + velocityGenerator.Next(-1, 2);
            nextCoords.y = position.y + velocityGenerator.Next(-1, 2);

            return nextCoords;
        }


        public static Enemy CreateEmemy(string symbol, ConsoleColor consoleColor)
        {
            return new Enemy
            {
                position = Coordinates.CreateRandomCoordinates(),
                symbol = symbol,
                Colour = consoleColor,
            };
        }
    }
}