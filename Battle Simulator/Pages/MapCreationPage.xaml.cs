using Battle_Simulator.Halper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MapCreationPage.xaml
    /// </summary>
    public partial class MapCreationPage : Page
    {
        DataManager DataManager;
        public MapCreationPage(DataManager DataManager)
        {
            InitializeComponent();
            this.DataManager = DataManager;
            MapSelector.ItemsSource = DataManager.Maps;
            this.ShowsNavigationUI = false;
        }
        private void WidthInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateNumericTextFields(sender);
        }

        private void HeightInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateNumericTextFields(sender);
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Int32.TryParse(WidthInputField.Text,out int width);
            if(width == 0)
            {
                MessageBox.Show("Width must be bigger than 0");
            }
            Int32.TryParse(HeightInputField.Text, out int height);
            if (height == 0)
            {
                MessageBox.Show("Width must be bigger than 0");
            }
            string name = MapNameInputField.Text;
            if(name == "")
            {
                MessageBox.Show("Name must at least contain 1 character");
            }
            SaveMap(new Map.Map(width, height, name));
        }
        private void ValidateNumericTextFields(object sender)
        {
            TextBox box = (TextBox)sender;
            box.Text = RemoveNANCharacters(box.Text);
            box.SelectionStart = box.Text.Length;
        }
        private string RemoveNANCharacters(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }
        public void SaveMap(Map.Map map)
        {
            Map.Map tmp;
            if ((tmp = DataManager.Maps.Where(x => x.Name == map.Name).FirstOrDefault()) != null)
            {
                tmp = map;
            }
            else
            {
                DataManager.Maps.Add(map);
            }
        }

        private void MapSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Map.Map selectedMap = (Map.Map) e.AddedItems[0];
            WidthInputField.Text = selectedMap.Width.ToString();
            HeightInputField.Text = selectedMap.Height.ToString();
            MapNameInputField.Text = selectedMap.Name;
        }
    }
}
