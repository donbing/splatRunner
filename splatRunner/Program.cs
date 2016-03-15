using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApplication1
{
    internal class Program
    {
        private const string playerSymbol = "@";
        private static int cursorX = Console.WindowHeight/2;
        private static int cursorY = Console.WindowWidth/2;
        private static bool shouldContinue = true;
        private static int moveSpeed = 1;

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
        };

        private static void Main(string[] args)
        {
            Console.CursorVisible = false;

            while (shouldContinue)
            {
                Console.Clear();
                Console.SetCursorPosition(cursorY, cursorX);
                Console.Write(playerSymbol);

                var move = Console.ReadKey();

                if (KeyActions.ContainsKey(move.Key))
                {
                    KeyActions[move.Key].Invoke();
                }
            }
        }
    }
}