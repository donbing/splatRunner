using System;
using System.Collections.Generic;

namespace splatRunner
{
    internal class Character
    {
        public int xCoord { get; set; }
        public int yCoord { get; set; }
        public string symbol { get; set; }
    } 
    internal class Program
    {
        static Character player = new Character();
        // private const string playerSymbol = "@";
        // private static int playerY = Console.WindowHeight/2;
        // private static int playerX = Console.WindowWidth/2;
        private static bool shouldContinue = true;
        private static int moveSpeed = 1;
        private const string enemy1Symbol = "#";
        private static int enemy1Y = new Random().Next(0, Console.WindowHeight);
        private static int enemy1X = new Random().Next(0, Console.WindowWidth);

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

            player.xCoord = Console.WindowWidth/2;
            player.yCoord = Console.WindowHeight / 2;
            player.symbol = "@";
            while (shouldContinue)
            {
                WriteCharacterAtPosition(player);

                var velocityGenerator = new Random();

                enemy1X = enemy1X + velocityGenerator.Next(-1, 2);
                enemy1Y = enemy1Y + velocityGenerator.Next(-1, 2); // best enemy name ever! enemyPoenemy1YonY

                // WriteCharacterAtPosition (enemy1X, enemy1Y, enemy1Symbol);

                var move = Console.ReadKey();
                Console.Clear();
                if (KeyActions.ContainsKey(move.Key))
                {
                    KeyActions[move.Key].Invoke();
                }
            }
        }

        private static void WriteCharacterAtPosition(Character thing )
        {
            Console.SetCursorPosition(thing.xCoord, thing.yCoord);
            Console.Write(thing.symbol);
        }
    }
}