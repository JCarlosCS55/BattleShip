using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip.Models
{
    internal class GeneratePos
    {
        Random random = new Random();
        public List<Position> GeneratePosistion(int size, List<Position> AllPosition)
        {
            List<Position> positions = new List<Position>();

            bool IsExist = false;

            do
            {
                positions = GeneratePositionRandomly(size);
                IsExist = positions.Where(pos => AllPosition.Exists(ShipPos => ShipPos.x == pos.x && ShipPos.y == pos.y)).Any();
            }
            while (IsExist);
            AllPosition.AddRange(positions);
            return positions;
        }
        public List<Position> GeneratePositionRandomly(int size)
        {
            List<Position> positions = new List<Position>();

            int direction = random.Next(1, size);
            int row = random.Next(1, 11);
            int col = random.Next(1, 11);

            if (direction % 2 != 0)
            {
                if (row - size > 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row - i;
                        pos.y = col;
                        positions.Add(pos);
                    }
                }
                else
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row + i;
                        pos.y = col;
                        positions.Add(pos);
                    }
                }
            }
            else
            {
                if (col - size > 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row;
                        pos.y = col - i;
                        positions.Add(pos);
                    }
                }
                else
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row;
                        pos.y = col + i;
                        positions.Add(pos);
                    }
                }
            }
            return positions;
        }
    }
}
