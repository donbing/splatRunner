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

        private static readonly Dictionary<ConsoleKey, Action> KeyActions = new Dictionary<ConsoleKey, Action>
        {
            [ConsoleKey.LeftArrow] = () => cursorY = cursorY - 1,
            [ConsoleKey.RightArrow] = () => cursorY = cursorY + 1,
            [ConsoleKey.UpArrow] = () => cursorX = cursorX - 1,
            [ConsoleKey.DownArrow] = () => cursorX = cursorX + 1,
            [ConsoleKey.Escape] = () => shouldContinue = false,
            [ConsoleKey.F9] = Beeper.DoBeepyTune
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