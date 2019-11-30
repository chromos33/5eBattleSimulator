using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battle_Simulator.Map
{
    public class MapSquare: INotifyPropertyChanged
    {
        public MapPoint Coordinates { get; set; }

        public SquareType type;
        public SquareType SquareType { get { return type; } }

        private Button Control;

        public event PropertyChangedEventHandler PropertyChanged;

        public MapSquare(int X, int Y, SquareType type)
        {
            this.type = type;
            Coordinates = new MapPoint(X, Y);
        }

        public Button GetButton()
        {
            return Control;
        }
        public bool IsWalkable()
        {
            return SquareType == SquareType.Path;
        }
        public bool CanShootThrough()
        {
            return SquareType == SquareType.Path || SquareType == SquareType.RangePassThrough;
        }
        public void InitialiseControls()
        {
            Control = new Button();
            Control.Width = 30;
            Control.Height = 30;
            Control.Click += handleStateChange;
            Control.Content = "";
            updateBackgroundColor();
        }
        void handleStateChange(object sender, RoutedEventArgs e)
        {
            if((int) SquareType < 4)
            {
                int calctype = (int)type;
                calctype++;
                type = (SquareType)calctype;
            }
            else
            {
                type = SquareType.Wall;
            }
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("type"));
            updateBackgroundColor();
        }
        void updateBackgroundColor()
        {
            switch(type)
            {
                case SquareType.Wall:
                    Control.Background = Brushes.Black;
                    Control.Foreground = Brushes.White;
                    break;
                case SquareType.Path:
                    Control.Background = Brushes.LightGray;
                    Control.Foreground = Brushes.Black;
                    break;
                case SquareType.RangePassThrough:
                    Control.Background = Brushes.Gray;
                    Control.Foreground = Brushes.Black;
                    break;
                case SquareType.Undefined:
                    Control.Background = Brushes.White;
                    Control.Foreground = Brushes.Black;
                    break;
            }
        }
    }

    public enum SquareType
    {
        Wall = 1,
        Path = 2,
        RangePassThrough = 3,
        Undefined = 4
    }

}
