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
            Commentator commentator = new Commentator();

            NavyAsset MyNavyAsset = new NavyAsset();
            NavyAsset EnemyNavyAsset = new NavyAsset();

            Dictionary<char, int> Coordinates = PopulateDictionary();
            map.PrintHeader();
            for (int h = 0; h < 19; h++)
            {
                Write(" ");
            }

            map.PrintMap(MyNavyAsset.FirePositions, MyNavyAsset, EnemyNavyAsset, isShowShips);

            int Game;
            for ( Game = 1; Game < 101; Game++)
            {
                MyNavyAsset.StepsTaken++;

                Position position = new Position();

                ForegroundColor = ConsoleColor.White;
                WriteLine("Enter firing position (e.g. B1 or f5).");
                string input = ReadLine();
                position = AnalyzeInput(input, Coordinates);

                if (position.x == -1 || position.y == -1)
                {
                    WriteLine("Invalid coordinates!");
                    Game--;
                    continue;
                }

                if (MyNavyAsset.FirePositions.Any(EFP => EFP.x == position.x && EFP.y == position.y))
                {
                    WriteLine("This coordinate already being shot.");
                    Game--;
                    continue;
                }


                EnemyNavyAsset.Fire();


                var index = MyNavyAsset.FirePositions.FindIndex(p => p.x == position.x && p.y == position.y);

                if (index == -1)
                    MyNavyAsset.FirePositions.Add(position);

                Clear();



                MyNavyAsset.AllShipsPosition.OrderBy(o => o.x).ThenBy(n => n.y).ToList();
                MyNavyAsset.CheckShipStatus(EnemyNavyAsset.FirePositions);

                EnemyNavyAsset.AllShipsPosition.OrderBy(o => o.x).ThenBy(n => n.y).ToList();
                EnemyNavyAsset.CheckShipStatus(MyNavyAsset.FirePositions);

                map.PrintHeader();
                for (int h = 0; h < 19; h++)
                {
                    Write(" ");
                }
                map.PrintMap(MyNavyAsset.FirePositions, MyNavyAsset, EnemyNavyAsset, isShowShips);

                commentator.CommentatorGame(MyNavyAsset, true);
                commentator.CommentatorGame(EnemyNavyAsset, false);
                if (EnemyNavyAsset.IsObliteratedAll || MyNavyAsset.IsObliteratedAll) { break; }

            }

            ForegroundColor = ConsoleColor.White;

            if (EnemyNavyAsset.IsObliteratedAll && !MyNavyAsset.IsObliteratedAll)
            {
                WriteLine("Game Ended, you win.");
            }
            else if (!EnemyNavyAsset.IsObliteratedAll && MyNavyAsset.IsObliteratedAll)
            {
                WriteLine("Game Ended, you lose.");
            }
            else
            {
                WriteLine("Game Ended, draw.");
            }

            WriteLine("Total steps taken:{0} ", Game);
            ReadLine();
        }

        static Position AnalyzeInput(string input, Dictionary<char, int> Coordinates)
        {
            Position pos = new Position();

            char[] inputSplit = input.ToUpper().ToCharArray();


            if (inputSplit.Length < 2 || inputSplit.Length > 4)
            {
                return pos;
            }




            if (Coordinates.TryGetValue(inputSplit[0], out int value))
            {
                pos.x = value;
            }
            else
            {
                return pos;
            }


            if (inputSplit.Length == 3)
            {

                if (inputSplit[1] == '1' && inputSplit[2] == '0')
                {
                    pos.y = 10;
                    return pos;
                }
                else
                {
                    return pos;
                }

            }


            if (inputSplit[1] - '0' > 9)
            {
                return pos;
            }
            else
            {
                pos.y = inputSplit[1] - '0';
            }

            return pos;
        }
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
