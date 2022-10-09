using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace BattleShip
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isShowShips = false;//show enemy ships position
            Map map = new Map();

            NavyAsset MyNavyAsset = new NavyAsset();
            NavyAsset EnemyNavyAsset = new NavyAsset();

            Dictionary<char, int> Coordinates = PopulateDictionary();
            PrintHeader();
            for (int h = 0; h < 19; h++)
            {
                Write(" ");
            }

            static void PrintHeader()
            {
                ForegroundColor = ConsoleColor.DarkYellow;
                Write("[ ]");
                for (int i = 1; i < 11; i++)
                    Write("[" + i + "]");
            }

            map.PrintMap(MyNavyAsset.FirePositions, MyNavyAsset, EnemyNavyAsset, isShowShips);

            static Dictionary<char, int> PopulateDictionary()
            {
                Dictionary<char, int> Coordinate =
                         new Dictionary<char, int>
                         {
                         { 'A', 1 },
                         { 'B', 2 },
                         { 'C', 3 },
                         { 'D', 4 },
                         { 'E', 5 },
                         { 'F', 6 },
                         { 'G', 7 },
                         { 'H', 8 },
                         { 'I', 9 },
                         { 'J', 10 }
                         };

                return Coordinate;
            }
        }
    }
}
