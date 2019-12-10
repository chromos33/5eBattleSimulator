using Battle_Simulator.CharacterStuff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Battle_Simulator.Map
{
    public class Map : INotifyPropertyChanged
    {
        public MapSquare[] MapSquares;

        public event PropertyChangedEventHandler PropertyChanged;

        private List<Character> mapCharacters = new List<Character>();
        public string Name { get; }
        public int Width { get; }
        public int Height { get; }
        public int SquareCount {
            get { return MapSquares.Length; }
        }
        public override string ToString()
        {
            return Name;
        }
        void MapSquareChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("MapSquares"));
        }
        public Map(int Width, int Height,string Name)
        {
            this.Name = Name;
            this.Width = Width;
            this.Height = Height;

            MapSquares = new MapSquare[Width*Height];
            int index = 0;
            for (int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    MapSquares[index] = new MapSquare(x, y, SquareType.Undefined);
                    MapSquares[index].PropertyChanged += MapSquareChanged;
                    index++;
                }
            }
        }

        internal void Simulate()
        {
            RollInitiativeForAll();
            RollNPCHealth();
            InitPCHealth();
            int turn = 0;
            
            while (!CombatEnd())
            {
                turn++;
                SimulateTurn();
            }
        }
        private void SimulateTurn()
        {
            foreach(Character CharOnTurn in mapCharacters)
            {
                CharOnTurn.TakeTurn(this);
            }
        }
        public List<MapSquare> Path(MapSquare s1, MapSquare s2)
        {
            InitPathfinding();
            List<MapSquare> openList = new List<MapSquare>();
            List<MapSquare> closedList = new List<MapSquare>();
            int g = 0;
            openList.Add(s1);
            while(openList.Count() > 0)
            {
                var lowest = openList.Min(l => l.f);
                MapSquare current = openList.First(l => l.f == lowest);
                closedList.Add(current);
                openList.Remove(current);
                //TODO if square is enemy change target
                if(closedList.FirstOrDefault(l => l.isThisSquare(s2)) != null)
                {
                    break;
                }
                var adjacentSquares = AdjacentWalkableSquares(current);
                //TODO continue on "foreach(var adjacentSquare in adjacentSquares)" https://gigi.nullneuron.net/gigilabs/a-pathfinding-example-in-c/
            }

            return closedList;
        }
        public List<MapSquare> AdjacentWalkableSquares(MapSquare s1)
        {
            return MapSquares.Where(x => x.isAdjacent(s1) && x.IsWalkable()).ToList();
        }
        public void InitPathfinding()
        {
            foreach(MapSquare square in MapSquares)
            {
                square.InitForPathfinding();
            }
        }
        private void RollInitiativeForAll()
        {
            foreach(Character current in mapCharacters)
            {
                current.RollInitiative();
            }
            mapCharacters.Sort((x, y) => x.GetInitiative().CompareTo(y.GetInitiative()));
        }
        private bool CombatEnd()
        {
            return mapCharacters.Where(x => !x.IsAlive() && x.Type == CharacterType.NPC).Count() == 0 || mapCharacters.Where(x => !x.IsAlive() && x.Type == CharacterType.PC).Count() == 0;
        }
        private void RollNPCHealth()
        {
            foreach (Character current in mapCharacters.Where(x => x.Type == CharacterType.NPC))
            {
                current.RollHealth();
            }
        }
        private void InitPCHealth()
        {
            foreach (Character current in mapCharacters.Where(x => x.Type == CharacterType.PC))
            {
                current.InitHealth();
            }
            
        }

        internal Map SimulationClone()
        {
            Map simulationmap = GetClone();
            foreach(Character sourceChar in mapCharacters)
            {
                simulationmap.mapCharacters.Add(sourceChar.GetClone());
            }
            return simulationmap;
        }

        public void SetUpChangedEvents()
        {
            foreach(MapSquare square in MapSquares)
            {
                square.PropertyChanged += MapSquareChanged;
            }
        }
        public Map GetClone()
        {
            Map clone = new Map(Width,Height,Name);
            clone.MapSquares = new MapSquare[Width * Height];
            for(int i = 0; i < Width*Height; i++)
            {
                clone.MapSquares[i] = MapSquares[i].GetClone();
            } 

            return clone;
        }
        public void SetCharacterList(List<Character> MapCharacters)
        {
            mapCharacters = MapCharacters;
        }
    }
}
