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

        static Character player = new Character
        {
            position = Coordinates.CreateCenterCoordinates(),
            symbol = "@",
            Colour = ConsoleColor.Green,
        };

        static List<Character> allSprites = new List<Character>
        {
            Enemy.CreateEmemy("1", ConsoleColor.Blue),
            Enemy.CreateEmemy("2", ConsoleColor.DarkYellow),
            Enemy.CreateEmemy("#", ConsoleColor.Red),
            Squirrel.Create(),
            Squirrel.Create(),
            Squirrel.Create(),
            player,
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
                    MovementKeys[userKeyPress.Key].Invoke();

                    var allEnemies = allSprites.Except(new List<Character>() { player }).OfType<Enemy>();

                    foreach (var character in allEnemies)
                    {
                        character.RecalculatePosition();
                    }
                    
                    foreach (var enemy in allEnemies.ToList())
                    {
                        if (enemy.position.y == player.position.y && enemy.position.x == player.position.x)
                        {
                            if (enemy is Squirrel)
                            {
                                allSprites.Remove(enemy);
                            }
                            else
                            {
                                allSprites.Remove(player);
                                shouldContinue = false;
                                Console.WriteLine("GAME OVER");
                                Console.ReadKey();
                            }
                        }
                    }
                        foreach (var character in allSprites.ToList())
                    {
                        character.WriteCharacterAtPosition();
                    }
                    
                }
            }
        }
    }
}