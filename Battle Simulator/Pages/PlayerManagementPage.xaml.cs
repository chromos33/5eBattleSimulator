using Battle_Simulator.CharacterStuff;
using Battle_Simulator.Converter;
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
        Character SelectedCharacter;
        public PlayerManagementPage(DataManager DataManager)
        {
            InitializeComponent();
            this.DataManager = DataManager;
            AddAttackOptionList.ItemsSource = DataManager.AttackOptions;
            HPDice.ItemsSource = Enum.GetValues(typeof(Dice));
            CharacterSelector.ItemsSource = DataManager.Characters;
            SwitchFieldsOff();
        }

        private void CharacterSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count >0)
            {
                SelectedCharacter = (Character)e.AddedItems[0];
                setBindings((Character)e.AddedItems[0]);
                SwitchFieldsOn();
            }
            
        }

        private void CharacterDelete_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedCharacter != null)
            {
                DataManager.Characters.Remove(SelectedCharacter);
                ClearBindings();
                if(DataManager.Characters.Count == 0)
                {
                    SwitchFieldsOff();
                }
            }
        }


        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            SelectedCharacter = new Character();
            DataManager.Characters.Add(SelectedCharacter);
            setBindings(SelectedCharacter);
            SwitchFieldsOn();
        }
        private void setBindings(Character character)
        {
            CharacterName.SetBinding(TextBox.TextProperty, getBindingOfAttribute(character, "Name"));
            STR.SetBinding(TextBox.TextProperty, getBindingOfAttribute(character, "Str", ParseType.Int));
            DEX.SetBinding(TextBox.TextProperty, getBindingOfAttribute(character, "Dex", ParseType.Int));
            CON.SetBinding(TextBox.TextProperty, getBindingOfAttribute(character, "Con", ParseType.Int));
            INT.SetBinding(TextBox.TextProperty, getBindingOfAttribute(character, "Int", ParseType.Int));
            WIS.SetBinding(TextBox.TextProperty, getBindingOfAttribute(character, "Wis", ParseType.Int));
            CHA.SetBinding(TextBox.TextProperty, getBindingOfAttribute(character, "Cha", ParseType.Int));
            AttackOptionList.SetBinding(ListBox.ItemsSourceProperty, getBindingOfAttribute(character, "AttackOptions"));
            AC.SetBinding(TextBox.TextProperty, getBindingOfAttribute(character, "AC", ParseType.Int));
            HP.SetBinding(TextBox.TextProperty, getBindingOfAttribute(character, "TotalHealth", ParseType.Int));
            HPDice.SetBinding(ComboBox.SelectedValueProperty, getBindingOfAttribute(character, "HPDice"));
            Level.SetBinding(TextBox.TextProperty, getBindingOfAttribute(character, "Level", ParseType.Int));
        }
        private void ClearBindings()
        {
            BindingOperations.ClearAllBindings(CharacterName);
            BindingOperations.ClearAllBindings(STR);
            BindingOperations.ClearAllBindings(DEX);
            BindingOperations.ClearAllBindings(CON);
            BindingOperations.ClearAllBindings(INT);
            BindingOperations.ClearAllBindings(WIS);
            BindingOperations.ClearAllBindings(CHA);
            BindingOperations.ClearAllBindings(AttackOptionList);
            BindingOperations.ClearAllBindings(AC);
            BindingOperations.ClearAllBindings(HP);
            BindingOperations.ClearAllBindings(HPDice);
            BindingOperations.ClearAllBindings(Level);
        }

        private void AddAttackOption_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedCharacter != null)
            {
                SelectedCharacter.AddAttackOption((AttackOption)AddAttackOptionList.SelectedItem);
                AttackOptionList.Items.Refresh();
            }
        }
        private void SwitchFieldsOn()
        {
            bool onstate = true;
            CharacterName.IsEnabled = onstate;
            STR.IsEnabled = onstate;
            DEX.IsEnabled = onstate;
            CON.IsEnabled = onstate;
            INT.IsEnabled = onstate;
            WIS.IsEnabled = onstate;
            CHA.IsEnabled = onstate;
            AddAttackOptionList.IsEnabled = onstate;
            AddAttackOption.IsEnabled = onstate;
            AttackOptionList.IsEnabled = onstate;
            AC.IsEnabled = onstate;
            HP.IsEnabled = onstate;
            HPDice.IsEnabled = onstate;
            Level.IsEnabled = onstate;
        }
        private void SwitchFieldsOff()
        {
            bool onstate = false;
            CharacterName.IsEnabled = onstate;
            STR.IsEnabled = onstate;
            DEX.IsEnabled = onstate;
            CON.IsEnabled = onstate;
            INT.IsEnabled = onstate;
            WIS.IsEnabled = onstate;
            CHA.IsEnabled = onstate;
            AddAttackOptionList.IsEnabled = onstate;
            AddAttackOption.IsEnabled = onstate;
            AttackOptionList.IsEnabled = onstate;
            AC.IsEnabled = onstate;
            HP.IsEnabled = onstate;
            HPDice.IsEnabled = onstate;
            Level.IsEnabled = onstate;
        }
        private Binding getBindingOfAttribute(object Object,string PropertyPath,ParseType parsetype = ParseType.None)
        {
            Binding Binding = new Binding();
            Binding.Source = Object;
            Binding.Path = new PropertyPath(PropertyPath);
            Binding.Mode = BindingMode.TwoWay;
            Binding.UpdateSourceTrigger = UpdateSourceTrigger.LostFocus;
            if(parsetype != ParseType.None)
            {
                AddParseToBinding(Binding, parsetype);
            }
            return Binding;
        }
        private Binding AddParseToBinding(Binding binding, ParseType parseType)
        {
            switch(parseType)
            {
                case ParseType.Int:
                    binding.Converter = new IntToStringConverter();
                    return binding;
                case ParseType.Dice:
                    binding.Converter = new DiceConverter();
                    return binding;
                default:
                    return binding;  
            }
        }
    }
    public enum ParseType
    {
        None = 1,
        Int = 0,
        Dice = 2
    }

}
