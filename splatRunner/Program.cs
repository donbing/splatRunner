using System;
using System.Collections.Generic;
using ConsoleApplication1;

namespace splatRunner
{
    internal class Program
    {
        private const string playerSymbol = "@";
        private static int cursorX = Console.WindowHeight / 2;
        private static int cursorY = Console.WindowWidth / 2;
        private static bool shouldContinue = true;
        private static int moveSpeed = 1;
        private static GameMap map = new GameMap(Console.WindowHeight, Console.WindowWidth);

        private static readonly Dictionary<ConsoleKey, Action> KeyActions = new Dictionary<ConsoleKey, Action>
        {
            [ConsoleKey.Escape] = () => shouldContinue = false,
            [ConsoleKey.F9] = () => Beeper.DoBeepyTune(),
            [ConsoleKey.LeftArrow] = () => cursorY -= moveSpeed,
            [ConsoleKey.RightArrow] = () => cursorY += moveSpeed,
            [ConsoleKey.UpArrow] = () => cursorX -= moveSpeed,
            [ConsoleKey.DownArrow] = () => cursorX += moveSpeed,
            [ConsoleKey.W] = () => moveSpeed++,
            [ConsoleKey.S] = () => moveSpeed--,
            [ConsoleKey.F1] = () => ShowHelp(KeyActions),
        };

        private static void ShowHelp(Dictionary<ConsoleKey, Action> keyActions)
        {
            Console.Clear();
            foreach (var actionKey in keyActions)
            {
                Console.WriteLine(actionKey);
            }
        }

        private static void Main(string[] args)
        {
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
            Console.Clear();

            while (shouldContinue)
            {
                map.Draw();
                Console.SetCursorPosition(cursorY, cursorX);
                Console.Write(playerSymbol);

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