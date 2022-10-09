using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using static System.Console;

namespace BattleShip
{
    internal class Map
    {
        public void PrintMap(List<Position> positions, NavyAsset MyNavyAsset, NavyAsset EnemyMyNavyAsset, bool showEnemyShips)
        {
            PrintHeader();
            WriteLine();
            if (!showEnemyShips)
                showEnemyShips = MyNavyAsset.IsObliteratedAll;

            List<Position> SortedLFirePositions = positions.OrderBy(o => o.x).ThenBy(n => n.y).ToList();
            List<Position> SortedShipsPositions = EnemyMyNavyAsset.AllShipsPosition.OrderBy(o => o.x).ThenBy(n => n.y).ToList();

            SortedShipsPositions = SortedShipsPositions.Where(FP => !SortedLFirePositions.Exists(ShipPos => ShipPos.x == FP.x && ShipPos.y == FP.y)).ToList();


            int hitCounter = 0;
            int EnemyshipCounter = 0;
            int myShipCounter = 0;
            int enemyHitCounter = 0;

            char row = 'A';
            try
            {
                for (int x = 1; x < 11; x++)
                {
                    for (int y = 1; y < 11; y++)
                    {
                        bool keepGoing = true;

                        #region row indicator
                        if (y == 1)
                        {
                            ForegroundColor = ConsoleColor.DarkYellow;
                            Write("[" + row + "]");
                            row++;
                        }
                        #endregion


                        if (SortedLFirePositions.Count != 0 && SortedLFirePositions[hitCounter].x == x && SortedLFirePositions[hitCounter].y == y)
                        {

                            if (SortedLFirePositions.Count - 1 > hitCounter)
                                hitCounter++;

                            if (EnemyMyNavyAsset.AllShipsPosition.Exists(ShipPos => ShipPos.x == x && ShipPos.y == y))
                            {

                                ForegroundColor = ConsoleColor.Red;
                                Write("[*]");

                                //PrintStatistic(x, y, navyAsset,true);
                                keepGoing = false;
                                //continue;

                            }
                            else
                            {
                                ForegroundColor = ConsoleColor.Black;
                                Write("[X]");

                                keepGoing = false;
                                //continue;

                            }

                        }

                        if (keepGoing && showEnemyShips && SortedShipsPositions.Count != 0 && SortedShipsPositions[EnemyshipCounter].x == x && SortedShipsPositions[EnemyshipCounter].y == y)

                        {

                            if (SortedShipsPositions.Count - 1 > EnemyshipCounter)
                                EnemyshipCounter++;

                            ForegroundColor = ConsoleColor.DarkGreen;
                            Write("[O]");
                            keepGoing = false;
                        }

                        if (keepGoing)
                        {
                            ForegroundColor = ConsoleColor.Blue;
                            Write("[ ]");
                        }


                        PrintStatistic(x, y, MyNavyAsset,1);


                        if (y == 10)
                        {
                            Write("      ");

                            PrintMapOfEnemy(x, row, MyNavyAsset, EnemyMyNavyAsset, ref myShipCounter, ref enemyHitCounter);
                        }
                    }

                    WriteLine();
                }

            }
            catch (Exception e)
            {
                string error = e.Message.ToString();
            }
        }

        static void PrintStatistic(int x, int y, NavyAsset navyAsset,int value)
        {
            if (x == 1 && y == 10)
            {
                if(value == 1)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("Enemy Board:  ");
                    value = value + 1;
                }
                else
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("Your Board:   ");
                }
            }


            if (x == 2 && y == 10)
            {
                if (navyAsset.IsAircraftSunk)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("Aircraft [5]  ");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("Aircraft [5]  ");
                }

            }

            if (x == 3 && y == 10)
            {
                if (navyAsset.IsBattleshipSunk)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("Battleship [4]");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("Battleship [4]");
                }
            }

            if (x == 4 && y == 10)
            {

                if (navyAsset.IsCruiserSunk)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("Cruiser [3]    ");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("Cruiser [3]   ");
                }
            }

            if (x == 5 && y == 10)
            {

                if (navyAsset.IsSubmarineSunk)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("Submarine [3] ");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("Submarine [3] ");
                }
            }

            if (x == 6 && y == 10)
            {

                if (navyAsset.IsDestroyerSunk)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("Destroyer [2] ");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("Destroyer [2] ");
                }

            }


            if (x > 6 && y == 10)
            {
                for (int i = 0; i < 14; i++)
                {
                    Write(" ");
                }
            }

        }

        static void PrintMapOfEnemy(int x, char row, NavyAsset MyNavyAsset, NavyAsset EnemyNavyAsset, ref int MyshipCounter, ref int EnemyHitCounter)
        {
            List<Position> EnemyFirePositions = new List<Position>();
            row--;
            Random random = new Random();
            List<Position> SortedLFirePositions = EnemyNavyAsset.FirePositions.OrderBy(o => o.x).ThenBy(n => n.y).ToList();
            List<Position> SortedLShipsPositions = MyNavyAsset.AllShipsPosition.OrderBy(o => o.x).ThenBy(n => n.y).ToList();

            SortedLShipsPositions = SortedLShipsPositions.Where(FP => !SortedLFirePositions.Exists(ShipPos => ShipPos.x == FP.x && ShipPos.y == FP.y)).ToList();


            try
            {

                for (int y = 1; y < 11; y++)
                {
                    bool keepGoing = true;

                    #region row indicator
                    if (y == 1)
                    {
                        ForegroundColor = ConsoleColor.DarkYellow;
                        Write("[" + row + "]");
                        row++;
                    }
                    #endregion


                    if (SortedLFirePositions.Count != 0 && SortedLFirePositions[EnemyHitCounter].x == x && SortedLFirePositions[EnemyHitCounter].y == y)
                    {

                        if (SortedLFirePositions.Count - 1 > EnemyHitCounter)
                            EnemyHitCounter++;

                        if (MyNavyAsset.AllShipsPosition.Exists(ShipPos => ShipPos.x == x && ShipPos.y == y))
                        {

                            ForegroundColor = ConsoleColor.Red;
                            Write("[*]");

                            //PrintStatistic(x, y, navyAsset,true);
                            keepGoing = false;
                            //continue;

                        }
                        else
                        {
                            ForegroundColor = ConsoleColor.Black;
                            Write("[X]");

                            //PrintStatistic(x, y, navyAsset, true);
                            keepGoing = false;
                            //continue;

                        }

                    }

                    if (keepGoing && SortedLShipsPositions.Count != 0 && SortedLShipsPositions[MyshipCounter].x == x && SortedLShipsPositions[MyshipCounter].y == y)

                    {

                        if (SortedLShipsPositions.Count - 1 > MyshipCounter)
                            MyshipCounter++;

                        ForegroundColor = ConsoleColor.DarkGreen;
                        Write("[O]");

                        // PrintStatistic(x, y, navyAsset, true);
                        keepGoing = false;
                        //continue;

                    }

                    if (keepGoing)
                    {
                        ForegroundColor = ConsoleColor.Blue;
                        Write("[ ]");
                    }


                    PrintStatistic(x, y, EnemyNavyAsset,0);

                }


            }
            catch (Exception e)
            {
                string error = e.Message.ToString();
            }
        }

        public void PrintHeader()
        {
            ForegroundColor = ConsoleColor.DarkYellow;
            Write("[ ]");
            for (int i = 1; i < 11; i++)
                Write("[" + i + "]");
        }
    }
}
