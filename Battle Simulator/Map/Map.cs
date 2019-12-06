using Battle_Simulator.CharacterStuff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            throw new NotImplementedException();
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
