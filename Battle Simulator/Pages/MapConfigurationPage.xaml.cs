using Battle_Simulator.Halper;
using Battle_Simulator.Map;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Battle_Simulator.Pages
{
    /// <summary>
    /// Interaction logic for MapConfigurationPage.xaml
    /// </summary>
    public partial class MapConfigurationPage : Page
    {
        TrulyObservableCollection<Map.Map> maps;
        public MapConfigurationPage(TrulyObservableCollection<Map.Map> maps)
        {
            InitializeComponent();
            this.maps = maps;
            MapSelector.ItemsSource = this.maps;
            this.ShowsNavigationUI = false;
        }

        private void MapSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                Map.Map selectedMap = (Map.Map)e.AddedItems[0];
                Grid MapGrid = new Grid();
                for (int x = 0; x < selectedMap.Width; x++)
                {
                    MapGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
                }
                for (int y = 0; y < selectedMap.Width; y++)
                {
                    MapGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                }
                foreach (MapSquare square in selectedMap.MapSquares)
                {
                    square.InitialiseControls();
                    MapGrid.Children.Add(square.GetButton());
                    Grid.SetRow(square.GetButton(), square.Coordinates.Y);
                    Grid.SetColumn(square.GetButton(), square.Coordinates.X);
                }
                MapContainer.Content = MapGrid;
            }
        }
    }
}
