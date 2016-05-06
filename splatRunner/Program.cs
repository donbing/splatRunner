using System;
using System.Collections.Generic;
using ConsoleApplication1;

namespace splatRunner
{
    public class Coordinates
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    class Program
    {
        private static Random velocityGenerator = new Random();

        static List<Character> allSprites = new List<Character>();
         
        static Character player = new Character
        {
            position = new Coordinates
            {
                x = Console.WindowWidth / 2, 
                y = Console.WindowHeight / 2,
            },
            symbol = "@",
        };

        static Character enemy = new Enemy
        {
            position = CreateRandomCoordinates(),
            symbol = "#",
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
            [ConsoleKey.LeftArrow] = () => player.MoveLeft(moveSpeed),
            [ConsoleKey.RightArrow] = () => player.MoveRight(moveSpeed),
            [ConsoleKey.UpArrow] = () => player.MoveUp(moveSpeed),
            [ConsoleKey.DownArrow] = () => player.MoveDown(moveSpeed),
            [ConsoleKey.W] = () => moveSpeed++,
            [ConsoleKey.S] = () => moveSpeed--,
            [ConsoleKey.F1] = () => ShowHelp(),
        };

        static Enemy ememy1 = new Enemy
        {
            position = CreateRandomCoordinates(),
            symbol = "1"
        };

        static Enemy ponenomy2 = new Enemy
        {
            position = CreateRandomCoordinates(),
            symbol = "2"
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
            allSprites.Add(ememy1);
            allSprites.Add(ponenomy2);

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