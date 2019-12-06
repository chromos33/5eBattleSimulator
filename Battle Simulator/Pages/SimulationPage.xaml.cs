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
    /// Interaction logic for SimulationPage.xaml
    /// </summary>
    public partial class SimulationPage : Page
    {
        Map.Map MapTemplate;
        public SimulationPage(Map.Map SimulationMap)
        {
            InitializeComponent();
            MapTemplate = SimulationMap;
        }

        private void SimulationStart_Click(object sender, RoutedEventArgs e)
        {
            int Simcount = Int32.Parse(Simulations.Text);
            for(int i = 0; i < Simcount;i++)
            {
                Simulate(MapTemplate.SimulationClone());
            }
        }
        private void Simulate(Map.Map tempMap)
        {
            tempMap.Simulate();
        }

        private void Simulations_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox simCount = (TextBox)sender;
            simCount.Text = RemoveNANCharacters(simCount.Text);
        }
        private string RemoveNANCharacters(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }
    }
}
