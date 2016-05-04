using System;
using System.Collections.Generic;
using ConsoleApplication1;

namespace splatRunner
{
    public class Character
    {
        public Coordinates position { get; set; }
        public string symbol { get; set; }

        public virtual void WriteCharacterAtPosition()
        {
            Console.SetCursorPosition(position.x, position.y);
            Console.Write(symbol);
        }
    }

    public class Coordinates
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Enemy : Character
    {
        private static Random velocityGenerator = new Random();
        Coordinates previousCoords;
        
        public override void WriteCharacterAtPosition()
        {
            previousCoords = position;

            GetValue();
        }

        void GetValue()
        {
            var nextCoords = NextCoords();

            if (nextCoords.x != previousCoords.x && nextCoords.y != previousCoords.y)
            {
                // best enemy name ever! enemyPoenemy1YonY

                Console.SetCursorPosition(nextCoords.x, nextCoords.y);
                Console.Write(symbol);

                position.y = nextCoords.y;
                position.x = nextCoords.x;
            }
            else
            {
                GetValue();
            }
        }

        Coordinates NextCoords()
        {
            var nextCoords = new Coordinates();

            nextCoords.x = position.x + velocityGenerator.Next(-1, 2);
            nextCoords.y = position.y + velocityGenerator.Next(-1, 2);

            if (nextCoords.x >= 119)
            {
                nextCoords.x = 0;
            }
            else if (position.x <= 0)
            {
                nextCoords.x = 119;
            }

            if (nextCoords.y >= 29)
            {
                nextCoords.y = 0;
            }
            else if (position.y <= 0)
            {
                nextCoords.y = 29;
            }
            return nextCoords;
        }
    }

     class Program
    {
        private static Random velocityGenerator = new Random();

        static List<Character> allSprites = new List<Character>();
         
        static Character player = new Character
        {
            position = new Coordinates()
            {
                x = Console.WindowWidth / 2, 
                y = Console.WindowHeight / 2,
            },
            
            symbol = "@"
        };

        static Character enemy = new Enemy
        {
            position = CreateRandomCoordinates(),

            symbol = "#"
        };

         static Coordinates CreateRandomCoordinates()
         {
             return new Coordinates()
             {
                 x = velocityGenerator.Next(0, Console.WindowWidth),
                 y = velocityGenerator.Next(0, Console.WindowHeight),
             };
         }

         private static bool shouldContinue = true;
        private static int moveSpeed = 1;

        private static readonly Dictionary<ConsoleKey, Action> KeyActions = new Dictionary<ConsoleKey, Action>
        {
            [ConsoleKey.Escape] = () => shouldContinue = false,
            [ConsoleKey.F9] = () => Beeper.DoBeepyTune(),
            [ConsoleKey.LeftArrow] = () => player.position.x -= moveSpeed,
            [ConsoleKey.RightArrow] = () => player.position.x += moveSpeed,
            [ConsoleKey.UpArrow] = () => player.position.y -= moveSpeed,
            [ConsoleKey.DownArrow] = () => player.position.y += moveSpeed,
            [ConsoleKey.W] = () => moveSpeed++,
            [ConsoleKey.S] = () => moveSpeed--,
            [ConsoleKey.F1] = () => ShowHelp(),
        };


        private static void ShowHelp()
        {
            Console.SetCursorPosition(0, 0);
            foreach (var actionKey in KeyActions)
            {
                Console.WriteLine(actionKey);
            }
        }

        private static void Main(string[] args)
        {
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
            Console.Clear();

            allSprites.Add(player);
            allSprites.Add(enemy);
            allSprites.Add(new Enemy
            {
                position = CreateRandomCoordinates(),
                symbol = "1"
            });

            allSprites.Add(new Enemy
            {
                position = CreateRandomCoordinates(),
                symbol = "2"
            });

            while (shouldContinue)
            {
                foreach (var character in allSprites)
                {
                    character.WriteCharacterAtPosition();
                }

                var move = Console.ReadKey();
                Console.Clear();
                if (KeyActions.ContainsKey(move.Key))
                {
                    KeyActions[move.Key].Invoke();
                }
            }
        }
    }
}