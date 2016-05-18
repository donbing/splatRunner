using System;

namespace splatRunner
{
    public class Coordinates
    {
        public int x { get; set; }
        public int y { get; set; }
        static Random velocityGenerator = new Random();

        public static Coordinates CreateRandomCoordinates()
        {
            return new Coordinates()
            {
                x = velocityGenerator.Next(0, Console.WindowWidth),
                y = velocityGenerator.Next(0, Console.WindowHeight),
            };
        }
    }
}