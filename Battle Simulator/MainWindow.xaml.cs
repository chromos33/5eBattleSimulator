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
        TrulyObservableCollection<Map.Map> Maps = new TrulyObservableCollection<Map.Map>();

        public MainWindow()
        {
            InitializeComponent();
            Main.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            Maps.CollectionChanged += MapsUpdated;
            ReadData();
        }

        private void MapsUpdated(object sender, NotifyCollectionChangedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(Maps);
            string docPath = System.IO.Directory.GetCurrentDirectory();
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, "Maps.json")))
            {
                outputFile.WriteLine(json);
            }
        }
        private void ReadData()
        {
            string docPath = System.IO.Directory.GetCurrentDirectory();
            if (File.Exists(System.IO.Path.Combine(docPath, "Maps.json")))
            {
                using (StreamReader inputfile = new StreamReader(System.IO.Path.Combine(docPath, "Maps.json")))
                {
                    string json = inputfile.ReadToEnd();
                    Maps = JsonConvert.DeserializeObject<TrulyObservableCollection<Map.Map>>(json);
                }
            }
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
                page = new MapCreationPage(Maps);
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
                page = new MapConfigurationPage(Maps);
                LoadedPages.Add(page);
                Main.Content = page;
            }
        }
    }
}
