using Battle_Simulator.CharacterStuff;
using Battle_Simulator.Halper;
using Battle_Simulator.Map;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MapCharacterPlacementPage.xaml
    /// </summary>
    public partial class MapCharacterPlacementPage : Page
    {
        DataManager DataManager;
        public MapCharacterPlacementPage(DataManager DataManager )
        {
            InitializeComponent();
            CharacterSelector.ItemsSource = DataManager.Characters;
            Allegiance.ItemsSource = Enum.GetValues(typeof(CharacterType));
            CharacterSelector.IsEnabled = false;
            MapSelector.ItemsSource = DataManager.Maps;
        }

        private void MapSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            enableUI();
            if (e.AddedItems.Count > 0)
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
                    square.InitialiseMapPlacementControls();
                    square.GetButton().Click += CharacterPlaced;
                    MapGrid.Children.Add(square.GetButton());
                    Grid.SetRow(square.GetButton(), square.Coordinates.Y);
                    Grid.SetColumn(square.GetButton(), square.Coordinates.X);
                }
                MapContainer.Content = MapGrid;
            }
        }

        private void CharacterPlaced(object sender, RoutedEventArgs e)
        {
            if(CharacterSelector.SelectedItem != null)
            {
                if(!((MapSquare)((Button)sender).Tag).IsOccupied())
                {
                    //think this through better...
                    //ui would be better if you had an extra list where you added Characters to before placing on board
                    Character selectedCharacter = ((Character)CharacterSelector.SelectedItem).GetClone();
                    //if(selectedCharacter.)
                    ((MapSquare)((Button)sender).Tag).SetOccupant(selectedCharacter);
                    selectedCharacter.SetPosition(((MapSquare)((Button)sender).Tag));
                } 
            }
        }

        private void enableUI()
        {
            CharacterSelector.IsEnabled = true;
        }
    }
}
