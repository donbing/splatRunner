using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication1;

namespace splatRunner
{
    class Program
    {
        private static bool shouldContinue = true;
        private static int moveSpeed = 1;

        static readonly Dictionary<ConsoleKey, Action> FunctionKeys = new Dictionary<ConsoleKey, Action>
        {
            [ConsoleKey.Escape] = () => shouldContinue = false,
            [ConsoleKey.F1] = () => ShowHelp(),
            [ConsoleKey.F9] = () => Beeper.DoBeepyTune(),
        };

        static readonly Dictionary<ConsoleKey, Action> MovementKeys = new Dictionary<ConsoleKey, Action>
        {
            [ConsoleKey.LeftArrow] = () => player.MoveLeft(moveSpeed),
            [ConsoleKey.RightArrow] = () => player.MoveRight(moveSpeed),
            [ConsoleKey.UpArrow] = () => player.MoveUp(moveSpeed),
            [ConsoleKey.DownArrow] = () => player.MoveDown(moveSpeed),
        };
        
        static List<Character> allSprites = new List<Character>();

        static Enemy ememy1 = Enemy.CreateEmemy("1", ConsoleColor.Gray);
        static Enemy ponenomy2 = Enemy.CreateEmemy("2", ConsoleColor.DarkYellow);
        static Character enemy = Enemy.CreateEmemy("#", ConsoleColor.Red);

        static Character player = new Character
        {
            position = Coordinates.CreateCenterCoordinates(),
            symbol = "@",
            Colour = ConsoleColor.Green,
        };
        
        private static void ShowHelp()
        {
            Console.SetCursorPosition(0, 0);
            foreach (var commandKey in FunctionKeys.Concat(MovementKeys))
            {
                Console.WriteLine(commandKey);
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

            foreach (var character in allSprites)
            {
                character.WriteCharacterAtPosition();
            }

            while (shouldContinue)
            {
                var userKeyPress = Console.ReadKey(true);

                if (FunctionKeys.ContainsKey(userKeyPress.Key))
                {
                    FunctionKeys[userKeyPress.Key].Invoke();
                }

                if (MovementKeys.ContainsKey(userKeyPress.Key))
                {
                    Console.Clear();
                    foreach (var character in allSprites)
                    {
                        character.WriteCharacterAtPosition();
                    }
                    MovementKeys[userKeyPress.Key].Invoke();
                }
            }
        }
    }
}