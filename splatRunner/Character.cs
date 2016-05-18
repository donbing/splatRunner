using System;
using System.Runtime.CompilerServices;

namespace splatRunner
{
    public class Character
    {
        public Coordinates position { get; set; }
        public string symbol { get; set; }
        public ConsoleColor Colour { get; set; } = ConsoleColor.White;

        public virtual void WriteCharacterAtPosition()
        {
            var previousColour = Console.ForegroundColor;
            Console.ForegroundColor = Colour;
            Console.SetCursorPosition(position.x, position.y);
            Console.Write(symbol);
            Console.ForegroundColor = previousColour;
        }

        public void EdgeLooper(Coordinates nextCoords)
        {
            nextCoords.x = Math.Abs(nextCoords.x) % Console.WindowWidth;
            nextCoords.y = Math.Abs(nextCoords.y) % Console.WindowHeight;
        }

        internal void MoveLeft(int moveSpeed)
        {
            var newX = position.x - moveSpeed;
            if (newX <0)
            {
                newX = Console.WindowWidth - 1;
            }
            position.x = newX;
        }

        internal void MoveRight(int moveSpeed)
        {
            var newX = position.x + moveSpeed;
            if (newX >= Console.WindowWidth)
            {
                newX = 0;
            }
            position.x = newX;
        }

        internal void MoveUp(int moveSpeed)
        {
            var newY = position.y - moveSpeed;
            if (newY < 0)
            {
                newY = Console.WindowHeight - 1;
            }
            position.y = newY;
        }

        internal void MoveDown(int moveSpeed)
        {
            var newY = position.y + moveSpeed;
            if (newY >= Console.WindowHeight)
            {
                newY = 0;
            }
            position.y = newY;
        }
    }
}