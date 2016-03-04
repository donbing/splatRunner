using System;
using System.Collections.Generic;
using System.Linq;

namespace splatRunner
{
    public class GameMap
    {
        IList<MapRoom> rooms = new List<MapRoom>();
        private char wallIcon = '*';

        private readonly int windowHeight;
        private readonly int windowWidth;

        public GameMap(int windowHeight, int windowWidth)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
            CreateRooms();
        }

        public void Draw()
        {
            foreach (var mapRoom in rooms)
            {
                var widthString = new string(wallIcon, mapRoom.width);

                Console.SetCursorPosition(mapRoom.xpos, mapRoom.ypos);
                Console.Write(widthString);
                Console.SetCursorPosition(mapRoom.xpos, mapRoom.height + mapRoom.ypos);
                Console.Write(widthString);

                foreach (var index in Enumerable.Range(0, mapRoom.height))
                {
                    Console.SetCursorPosition(mapRoom.xpos, mapRoom.ypos + index);
                    Console.Write(wallIcon);
                    Console.SetCursorPosition(mapRoom.xpos + mapRoom.width, mapRoom.ypos + index);
                    Console.Write(wallIcon);
                }
            }
        }
        
        private void CreateRooms()
        {
            var random = new Random();
            var potentialRooms = Enumerable.Range(1, 20)
                .Select(x => new MapRoom
                {
                    xpos = random.Next(0, windowWidth),
                    width = random.Next(3, 10),
                    ypos = random.Next(0, windowHeight),
                    height = random.Next(3, 10),
                })
                .Where(x => x.xpos + x.width < windowWidth)
                .Where(x => x.ypos + x.height < windowHeight)
                .ToList();

            rooms = potentialRooms.Where(p =>
                !potentialRooms.Except(new[] {p}).Any(pr => pr.OverLaps(p))
                ).ToList();
        }

        class MapRoom
        {
            public int xpos { get; set; }
            public int width { get; set; }
            public int ypos { get; set; }
            public int height { get; set; }

            public bool OverLaps(MapRoom other)
            {
                var xRange = Enumerable.Range(xpos, width);
                var yRange = Enumerable.Range(ypos, height);
                return xRange.Contains(other.xpos) && yRange.Contains(other.ypos);
            }
        }
    }
}