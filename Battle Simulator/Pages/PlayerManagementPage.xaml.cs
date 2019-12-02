using Battle_Simulator.CharacterStuff;
using Battle_Simulator.Halper;
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
    /// Interaction logic for PlayerCreationPage.xaml
    /// </summary>
    public partial class PlayerManagementPage : Page
    {
        DataManager DataManager;
        public PlayerManagementPage(DataManager DataManager)
        {
            InitializeComponent();
            this.DataManager = DataManager;
            AddAttackOptionList.ItemsSource = DataManager.AttackOptions;
            HPDice.ItemsSource = Enum.GetValues(typeof(Dice));
        }

        private void CharacterSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MapDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddAttackOption_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
