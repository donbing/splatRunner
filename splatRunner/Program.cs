using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    class Program
    {
        private const string playerSymbol = "@";
        static int cursorX = Console.WindowHeight / 2;
        static int cursorY = Console.WindowWidth / 2;

        static bool shouldContinue = true;

        static readonly Dictionary<ConsoleKey, Action> KeyActions = new Dictionary<ConsoleKey, Action>
        {
            [ConsoleKey.LeftArrow] = () => cursorY = cursorY - 1,
            [ConsoleKey.RightArrow] = () => cursorY = cursorY + 1,
            [ConsoleKey.UpArrow] = () => cursorX = cursorX - 1,
            [ConsoleKey.DownArrow] = () => cursorX = cursorX + 1,
            [ConsoleKey.Escape] = () => shouldContinue = false,
            [ConsoleKey.F9] = Console.Beep,
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
