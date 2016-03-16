using System;
using System.Collections.Generic;
using ConsoleApplication1;

namespace splatRunner
{
    public class Character
    {
        public int xCoord { get; set; }
        public int yCoord { get; set; }
        public string symbol { get; set; }

        public virtual void WriteCharacterAtPosition()
        {
            Console.SetCursorPosition(xCoord, yCoord);
            Console.Write(symbol);
        }
    }

    public class Enemy : Character
    {
        private static Random velocityGenerator = new Random();

        public override void WriteCharacterAtPosition()
        {
            xCoord = xCoord + velocityGenerator.Next(-1, 2);
            yCoord = yCoord + velocityGenerator.Next(-1, 2); // best enemy name ever! enemyPoenemy1YonY

            Console.SetCursorPosition(xCoord, yCoord);
            Console.Write(symbol);
        }

    }

    internal class Program
    {
        private static Random velocityGenerator = new Random();

        static List<Character> allSprites = new List<Character>();
         
        static Character player = new Character
        {
            xCoord = Console.WindowWidth / 2,
            yCoord = Console.WindowHeight / 2,
            symbol = "@"
        };

        static Character enemy = new Enemy
        {
            xCoord = velocityGenerator.Next(0, Console.WindowWidth),
            yCoord = velocityGenerator.Next(0, Console.WindowHeight),
            symbol = "#"
        };
        
        private static bool shouldContinue = true;
        private static int moveSpeed = 1;

        private static readonly Dictionary<ConsoleKey, Action> KeyActions = new Dictionary<ConsoleKey, Action>
        {
            [ConsoleKey.Escape] = () => shouldContinue = false,
            [ConsoleKey.F9] = () => Beeper.DoBeepyTune(),
            [ConsoleKey.LeftArrow] = () => player.xCoord -= moveSpeed,
            [ConsoleKey.RightArrow] = () => player.xCoord += moveSpeed,
            [ConsoleKey.UpArrow] = () => player.yCoord -= moveSpeed,
            [ConsoleKey.DownArrow] = () => player.yCoord += moveSpeed,
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
                xCoord = velocityGenerator.Next(0, Console.WindowWidth),
                yCoord = velocityGenerator.Next(0, Console.WindowHeight),
                symbol = "1"
            });

            allSprites.Add(new Enemy
            {
                xCoord = velocityGenerator.Next(0, Console.WindowWidth),
                yCoord = velocityGenerator.Next(0, Console.WindowHeight),
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