using Battle_Simulator.CharacterStuff;
using Battle_Simulator.Halper;
using Battle_Simulator.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Battle_Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Page> LoadedPages = new List<Page>();
        DataManager DataManager = new DataManager();
        public MainWindow()
        {
            InitializeComponent();
            Main.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        

        private void MapCreationLink_Click(object sender, RoutedEventArgs e)
        {
            Page page;
            if ((page = LoadedPages.Where(x => x.GetType() == typeof(MapCreationPage)).FirstOrDefault()) != null)
            {
                Main.Content = page;
            }
            else
            {
                page = new MapCreationPage(DataManager);
                LoadedPages.Add(page);
                Main.Content = page;
            }
        }

        private void MapConfigurationLink_Click(object sender, RoutedEventArgs e)
        {
            Page page;
            if ((page = LoadedPages.Where(x => x.GetType() == typeof(MapConfigurationPage)).FirstOrDefault()) != null)
            {
                Main.Content = page;
            }
            else
            {
                page = new MapConfigurationPage(DataManager);
                LoadedPages.Add(page);
                Main.Content = page;
            }
        }

        private void AttackOptionLink_Click(object sender, RoutedEventArgs e)
        {
            Page page;
            if ((page = LoadedPages.Where(x => x.GetType() == typeof(AttackOptionManager)).FirstOrDefault()) != null)
            {
                Main.Content = page;
            }
            else
            {
                page = new AttackOptionManager(DataManager);
                LoadedPages.Add(page);
                Main.Content = page;
            }
        }
    }
}
