using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BattleShip.Models
{
    class NavyAsset
    {
        Random random = new Random();
        GeneratePos pos = new GeneratePos();
        private const int AIRCRAFT = 5;
        private const int BATTLESHIP = 4;
        private const int SUBMARINE = 3;
        private const int DESTROYER = 2;
        private const int CRUISER = 3;

        public NavyAsset()
        {
            Aircraft = pos.GeneratePosistion(AIRCRAFT, AllShipsPosition);
            Battleship = pos.GeneratePosistion(BATTLESHIP, AllShipsPosition);
            Submarine = pos.GeneratePosistion(SUBMARINE, AllShipsPosition);
            Cruiser = pos.GeneratePosistion(CRUISER, AllShipsPosition);
            Destroyer = pos.GeneratePosistion(DESTROYER, AllShipsPosition); 
        }

        public int StepsTaken { get; set; } = 0;

        public List<Position> Aircraft { get; set; }
        public List<Position> Battleship { get; set; }
        public List<Position> Cruiser { get; set; }
        public List<Position> Submarine { get; set; }
        public List<Position> Destroyer { get; set; }
        public List<Position> AllShipsPosition { get; set; } = new List<Position>();
        public List<Position> FirePositions { get; set; } = new List<Position>();

        public bool IsAircraftSunk { get; set; } = false;
        public bool IsBattleshipSunk { get; set; } = false;
        public bool IsCruiserSunk { get; set; } = false;
        public bool IsSubmarineSunk { get; set; } = false;
        public bool IsDestroyerSunk { get; set; } = false;
        public bool IsObliteratedAll { get; set; } = false;


        public bool CheckAircraft { get; set; } = true;
        public bool CheckBattleship { get; set; } = true;
        public bool CheckCruiser { get; set; } = true;
        public bool CheckSubmarine { get; set; } = true;
        public bool CheckDestroyer { get; set; } = true;

        public NavyAsset CheckShipStatus(List<Position> HitPositions)
        {

            IsAircraftSunk = Aircraft.Where(Aircraft => !HitPositions.Any(H => Aircraft.x == H.x && Aircraft.y == H.y)).ToList().Count == 0;
            IsBattleshipSunk = Battleship.Where(Battleship => !HitPositions.Any(H => Battleship.x == H.x && Battleship.y == H.y)).ToList().Count == 0;
            IsCruiserSunk = Cruiser.Where(Cruiser => !HitPositions.Any(H => Cruiser.x == H.x && Cruiser.y == H.y)).ToList().Count == 0;
            IsSubmarineSunk = Submarine.Where(Submarine => !HitPositions.Any(H => Submarine.x == H.x && Submarine.y == H.y)).ToList().Count == 0;
            IsDestroyerSunk = Destroyer.Where(Destroyer => !HitPositions.Any(H => Destroyer.x == H.x && Destroyer.y == H.y)).ToList().Count == 0;
            IsObliteratedAll = IsAircraftSunk && IsBattleshipSunk && IsDestroyerSunk && IsSubmarineSunk && IsDestroyerSunk;
            return this;
        }

        public NavyAsset Fire()
        {
            Position EnemyShotPos = new Position();
            bool alreadyShot = false;
            do
            {
                EnemyShotPos.x = random.Next(1, 11);
                EnemyShotPos.y = random.Next(1, 11);
                alreadyShot = FirePositions.Any(EFP => EFP.x == EnemyShotPos.x && EFP.y == EnemyShotPos.y);
            }
            while (alreadyShot);

            FirePositions.Add(EnemyShotPos);
            return this;
        }
    }  
}
