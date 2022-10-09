using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace BattleShip
{
    internal class Commentator
    {
        public void CommentatorGame(NavyAsset navyAsset, bool isMyShip)
        {

            string title = isMyShip ? "Your" : "Enemy";

            if (navyAsset.CheckBattleship && navyAsset.IsBattleshipSunk)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sink", title, nameof(navyAsset.Battleship));
                navyAsset.CheckBattleship = false;
            }

            if (navyAsset.CheckAircraft && navyAsset.IsAircraftSunk)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sink", title, nameof(navyAsset.Aircraft));
                navyAsset.CheckAircraft = false;
            }

            if (navyAsset.CheckCruiser && navyAsset.IsCruiserSunk)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sink", title, nameof(navyAsset.Cruiser));
                navyAsset.CheckCruiser = false;
            }

            if (navyAsset.CheckDestroyer && navyAsset.IsDestroyerSunk)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sink", title, nameof(navyAsset.Destroyer));
                navyAsset.CheckDestroyer = false;
            }

            if (navyAsset.CheckSubmarine && navyAsset.IsSubmarineSunk)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sink", title, nameof(navyAsset.Submarine));
                navyAsset.CheckSubmarine = false;
            }
            // navyAsset.IsBattleshipSunk

        }
    }
}
