using Battle_Simulator.CharacterStuff;
using Battle_Simulator.Halper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    /// Interaction logic for AttackOptionManager.xaml
    /// </summary>
    public partial class AttackOptionManager : Page
    {
        DataManager DataManager;
        int currentAttackOptionIndex;
        public AttackOptionManager(DataManager DataManager)
        {
            InitializeComponent();
            this.DataManager = DataManager;
            Die.ItemsSource = Enum.GetValues(typeof(Dice));
            StatMod.ItemsSource = Enum.GetValues(typeof(AttributeName));
            AttackOptionSelector.ItemsSource = DataManager.AttackOptions;
        }

        private void AttackOptionSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            { 
                currentAttackOptionIndex = ((ListBox)sender).SelectedIndex;
                MapName.Text = DataManager.AttackOptions[currentAttackOptionIndex].Name;
                Die.SelectedItem = DataManager.AttackOptions[currentAttackOptionIndex].DmgDice;
                StatMod.SelectedItem = DataManager.AttackOptions[currentAttackOptionIndex].AttributeMod;
                FlatDmgBonus.Text = DataManager.AttackOptions[currentAttackOptionIndex].DefaultDmgBonus;
                StatModDmgEnabled.IsChecked = DataManager.AttackOptions[currentAttackOptionIndex].AttributeBonusToDmg;
            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(DataManager.AttackOptions.Count() == 0)
            {
                currentAttackOptionIndex = -1;
            }
            SaveOption();
        }
        private void SaveOption()
        {
            AttackOption currentAttackOption;
            if (currentAttackOptionIndex == -1)
            {
                currentAttackOption = new AttackOption();
            }
            else
            {
                currentAttackOption = DataManager.AttackOptions[currentAttackOptionIndex];
            }
            currentAttackOption.Name = MapName.Text;
            currentAttackOption.AttributeMod = (AttributeName)StatMod.SelectedItem;
            currentAttackOption.DmgDice = (Dice)Die.SelectedItem;
            currentAttackOption.DefaultDmgBonus = FlatDmgBonus.Text;
            currentAttackOption.AttributeBonusToDmg = (bool)StatModDmgEnabled.IsChecked;
            if (currentAttackOptionIndex == -1)
            {
                DataManager.AttackOptions.Add(currentAttackOption);
            }
            currentAttackOption.TriggerUpdate();
            AttackOptionSelector.Items.Refresh();


        }

        private void SaveNew_Click(object sender, RoutedEventArgs e)
        {
            currentAttackOptionIndex = -1;
            SaveOption();
        }

        private void AttackOptionDelete_Click(object sender, RoutedEventArgs e)
        {
            if(AttackOptionSelector.SelectedItem != null)
            {
                DataManager.AttackOptions.Remove((AttackOption)AttackOptionSelector.SelectedItem);
            }
        }
    }
}
