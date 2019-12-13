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
        public List<MapSquare> Path(MapSquare Start, MapSquare Target)
        {
            InitPathfinding();
            Start.SearchSquare();
            List<MapSquare> Path = MoveTo(Target, new List<MapSquare>() {Start});
            return Path;
        }
        private List<MapSquare> MoveTo(MapSquare Target,List<MapSquare> Path )
        {
            MapSquare current = Path.Last();
            if(current != null)
            {
                var AdjacentSquares = MapSquares.Where(x => current.isAdjacent(x) && x.IsWalkable() && !x.hasBeenSearched()).OrderBy(x => x.Coordinates.Distance(Target.Coordinates));
                var ShortestAdjacentSquare = AdjacentSquares.FirstOrDefault();
                if (ShortestAdjacentSquare != null)
                {
                    ShortestAdjacentSquare.SearchSquare();
                    Path.Add(ShortestAdjacentSquare);
                    if (Path.Count >= 3)
                    {
                        RemoveRedundantSquares(Path);
                    }
                    if (!Path.Last().isThisSquare(Target))
                    {
                        Path = MoveTo(Target, Path);
                    }
                }
                else
                {
                    if (!Path.Last().isThisSquare(Target))
                    {
                        //Stuck Go Backwards through the Path and search for First Square that still has Adjacent Walkable Sqaures and delete until then
                        for(int i = Path.Count()-1;i>=0;i--)
                        {
                            if(MapSquares.Where( x=> x.isAdjacent(Path[i]) && !x.hasBeenSearched()).Count() == 0)
                            {
                                Path.RemoveAt(i);
                            }
                            else
                            {
                                MoveTo(Target, Path);
                            }
                        }
                        if(Path.Count() == 0)
                        {
                            throw new Exception("No Path Possible");
                        }
                    }
                }
                
            }
            return Path;
        }
        private void RemoveRedundantSquares(List<MapSquare> Path)
        {
            int SearchedIndex = -1;
            int StartIndexSecondBeforeLast = Path.Count() - 3;
            int LastIndex = Path.Count() - 1;
            for (int i = StartIndexSecondBeforeLast; i>=0;i--)
            {
                if(Path[LastIndex].isAdjacent(Path[i]))
                {
                    SearchedIndex = i;
                    break;
                }
            }
            if(SearchedIndex >= 0)
            {
                Path.RemoveRange(SearchedIndex + 1, LastIndex - (SearchedIndex + 1));
            }
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
            return mapCharacters.Where(x => x.IsAlive() && x.Type == CharacterType.NPC).Count() == 0 || mapCharacters.Where(x => x.IsAlive() && x.Type == CharacterType.PC).Count() == 0;
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
        public List<Character> GetCharacterList()
        {
            return mapCharacters;
        }
        static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }
    }
}
