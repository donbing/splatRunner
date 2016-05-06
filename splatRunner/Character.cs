using System;

namespace splatRunner
{
    public class Character
    {
        public Coordinates position { get; set; }
        public string symbol { get; set; }

        public virtual void WriteCharacterAtPosition()
        {
            Console.SetCursorPosition(position.x, position.y);
            Console.Write(symbol);
        }

        public void EdgeLooper(Coordinates nextCoords)
        {
            nextCoords.x = Math.Abs(nextCoords.x)%Console.WindowWidth;
            nextCoords.y = Math.Abs(nextCoords.y)%Console.WindowHeight;
        }

        internal void MoveLeft(int moveSpeed)
        {
            position.x = Math.Min(0, position.x - moveSpeed) % Console.WindowWidth;
        }

        internal void MoveRight(int moveSpeed)
        {
            position.x = Math.Max(Console.WindowWidth, position.x + moveSpeed) % Console.WindowWidth;
        }

        internal void MoveUp(int moveSpeed)
        {
            position.y = Math.Min(0, position.y - moveSpeed)%Console.WindowHeight;
        }

        internal void MoveDown(int moveSpeed)
        {
            position.y = Math.Max(Console.WindowHeight, position.y + moveSpeed) % Console.WindowHeight;
        }
    }
}