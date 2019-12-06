using Battle_Simulator.CharacterStuff;
using Battle_Simulator.Halper;
using Battle_Simulator.Map;
using System;
using System.Collections.Generic;
using System.Linq;
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
        List<Character> MapCharacters = new List<Character>();
        public MapCharacterPlacementPage(DataManager DataManager )
        {
            InitializeComponent();
            CharacterSelector.ItemsSource = DataManager.Characters;
            Allegiance.ItemsSource = Enum.GetValues(typeof(CharacterType));
            CharacterSelector.IsEnabled = false;
            MapSelector.ItemsSource = DataManager.Maps;
            CharacterList.ItemsSource = MapCharacters;
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
                    MapGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(45) });
                }
                for (int y = 0; y < selectedMap.Width; y++)
                {
                    MapGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(45) });
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
            if(CharacterList.SelectedItem != null && ((MapSquare)((Button)sender).Tag).IsWalkable())
            {
                ((MapSquare)((Button)sender).Tag).SetOccupant((Character)CharacterList.SelectedItem);
                ((Character)CharacterList.SelectedItem).GetPosition()?.UnsetOccupant();
                ((Character)CharacterList.SelectedItem).SetPosition(((MapSquare)((Button)sender).Tag));
            }
        }

        private void enableUI()
        {
            CharacterSelector.IsEnabled = true;
        }

        private void AddCharacter_Click(object sender, RoutedEventArgs e)
        {
            if(CharacterSelector.SelectedItem != null)
            {
                Character selectedCharacter = ((Character)CharacterSelector.SelectedItem).GetClone(MapCharacters.Count);
                selectedCharacter.Type = (CharacterType)Allegiance.SelectedItem;
                MapCharacters.Add(selectedCharacter);
                CharacterList.Items.Refresh();
            }
        }

        private void Simulate_Click(object sender, RoutedEventArgs e)
        {
            if(MapSelector.SelectedItem != null && MapCharacters.Where(x => x.Type == CharacterType.NPC).Count() > 0 && MapCharacters.Where(x => x.Type == CharacterType.PC).Count() > 0)
            {
                Map.Map simulationMap = ((Map.Map)MapSelector.SelectedItem);
                simulationMap.SetCharacterList(MapCharacters);
                Frame pageFrame = null;
                DependencyObject currParent = VisualTreeHelper.GetParent(this);
                while (currParent != null && pageFrame == null)
                {
                    pageFrame = currParent as Frame;
                    currParent = VisualTreeHelper.GetParent(currParent);
                }

                // Change the page of the frame.
                if (pageFrame != null)
                {
                    pageFrame.Content = new SimulationPage(simulationMap);
                }
            }
        }
    }
}
