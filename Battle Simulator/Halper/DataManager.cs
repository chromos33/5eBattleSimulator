using Battle_Simulator.CharacterStuff;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace Battle_Simulator.Halper
{
    public class DataManager
    {
        public TrulyObservableCollection<Map.Map> Maps = new TrulyObservableCollection<Map.Map>();
        public TrulyObservableCollection<AttackOption> AttackOptions = new TrulyObservableCollection<AttackOption>();
        public TrulyObservableCollection<Character> Characters = new TrulyObservableCollection<Character>();

        public DataManager()
        {
            ReadData();
            Maps.CollectionChanged += MapsUpdated;
            AttackOptions.CollectionChanged += AttackOptionsUpdated;
            Characters.CollectionChanged += CharactersUpdated;
        }

        private void CharactersUpdated(object sender, NotifyCollectionChangedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(Characters);
            string docPath = System.IO.Directory.GetCurrentDirectory();
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, "Characters.json")))
            {
                outputFile.WriteLine(json);
            }
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
        void AttackOptionsUpdated(object sender, NotifyCollectionChangedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(AttackOptions);
            string docPath = System.IO.Directory.GetCurrentDirectory();
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, "AttackOptions.json")))
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
            if (File.Exists(System.IO.Path.Combine(docPath, "AttackOptions.json")))
            {
                using (StreamReader inputfile = new StreamReader(System.IO.Path.Combine(docPath, "AttackOptions.json")))
                {
                    string json = inputfile.ReadToEnd();
                    AttackOptions = JsonConvert.DeserializeObject<TrulyObservableCollection<AttackOption>>(json);
                }
            }
            if (File.Exists(System.IO.Path.Combine(docPath, "Characters.json")))
            {
                using (StreamReader inputfile = new StreamReader(System.IO.Path.Combine(docPath, "Characters.json")))
                {
                    string json = inputfile.ReadToEnd();
                    Characters = JsonConvert.DeserializeObject<TrulyObservableCollection<Character>>(json);
                }
            }
        }
    }
}
