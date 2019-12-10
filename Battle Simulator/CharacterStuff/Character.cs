using Battle_Simulator.Map;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Battle_Simulator.CharacterStuff
{
    public class Character : INotifyPropertyChanged
    {
        private int Id;
        private int str;
        public int Str { get { return str; } set { str = value; OnChanged(); } }

        private int dex;
        public int Dex { get { return dex; } set { dex = value; OnChanged(); } }

        private int con;
        public int Con { get { return con; } set { con = value; OnChanged(); } }

        private int intelligence;
        public int Int { get { return intelligence; } set { intelligence = value; OnChanged(); } }

        private int wis;
        public int Wis { get { return wis; } set { wis = value; OnChanged(); } }

        private int cha;
        public int Cha { get { return cha; } set { cha = value; OnChanged(); } }

        private int ac;
        public int AC
        {
            get
            {
                return ac;
            }
            set
            {
                ac = value;
                OnChanged();
            }
        }
        private int initiative;
        public int GetInitiative()
        {
            return initiative;
        }
        public void RollInitiative()
        {
            initiative = rng.Next(1, 20) + AttributeModifier(AttributeName.DEX);
        }

        private MapSquare Position;

        public bool HasPosition()
        {
            return Position != null;
        }
        public void SetPosition(MapSquare square)
        {
            Position = square;
        }
        public void UnsetPosition()
        {
            Position = null;
        }
        public MapSquare GetPosition()
        {
            return Position;
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnChanged();
            }
        }

        internal void TakeTurn(Map.Map map)
        {

        }

        private Dice hpdice;
        public Dice HPDice
        {
            get
            {
                return hpdice;
            }
            set
            {
                hpdice = value;
                OnChanged();
            }
        }
        private int conmod;
        public int ConMod
        {
            get
            {
                return conmod;
            }
            set
            {
                conmod = value;
                OnChanged();
            }
        }
        private int level;
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
                OnChanged();
            }
        }
        private int totalhealth;
        private int currentHealth;
        public int TotalHealth
        {
            get
            {
                return totalhealth;
            }
            set
            {
                totalhealth = value;
                OnChanged();
            }
        }
        internal void RollHealth()
        {
            totalhealth = 0;
            for(int i = 0; i< Level;i++)
            {
                totalhealth += rng.Next(1, (int)HPDice) + AttributeModifier(AttributeName.CON);
            }
            currentHealth = totalhealth;
        }
        internal void InitHealth()
        {
            currentHealth = totalhealth;
        }

        public bool IsAlive()
        {
            return currentHealth > 0;
        }

        private CharacterType type;
        public CharacterType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                OnChanged();
            }
        }
        private List<AttackOption> attackOptions = new List<AttackOption>();
        public List<AttackOption> AttackOptions
        {
            get
            {
                return attackOptions;
            }
            set
            {
                attackOptions = value;
                OnChanged();
            }
        }

        private Random rng;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChanged()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(""));
            }
        }
        public override string ToString()
        {
            if(Id > 0)
            {
                return GetPlacementKey();
            }
            return Name;
        }
        //For NPC
        public Character(int AC, Dice HPDice, int ConMod, int Level, List<AttackOption> options = null)
        {
            rng = new Random();
            this.AC = AC;
            this.HPDice = HPDice;
            this.ConMod = ConMod;
            this.Level = Level;
            Type = CharacterType.NPC;
            if (options != null)
            {
                AttackOptions = options;
            }
            else
            {
                AttackOptions = new List<AttackOption>();
            }
        }
        //For PC
        public Character(int AC, int TotalHealth, List<AttackOption> options = null)
        {
            this.AC = AC;
            this.TotalHealth = TotalHealth;
            type = CharacterType.PC;
            if (options != null)
            {
                AttackOptions = options;
            }
            else
            {
                AttackOptions = new List<AttackOption>();
            }
        }
        public Character()
        {
            Name = "New Character";
            AttackOptions = new List<AttackOption>();
            rng = new Random();
        }
        public void AddAttackOption(AttackOption option)
        {
            if (AttackOptions == null)
            {
                AttackOptions = new List<AttackOption>();
            }
            AttackOptions.Add(option);
            OnChanged();
        }
        public int AttributeModifier(AttributeName attribute)
        {
            int value = GetAttribute(attribute);

            decimal tmp = value - 10;
            if (tmp < 0)
            {
                return (int)Math.Ceiling((tmp / 2));
            }
            else
            {
                return (int)tmp / 2;
            }
        }
        public int GetAttribute(AttributeName attribute)
        {
            switch (attribute)
            {
                case AttributeName.STR: return Str;
                case AttributeName.DEX: return Dex;
                case AttributeName.CON: return Con;
                case AttributeName.INT: return Int;
                case AttributeName.WIS: return Wis;
                case AttributeName.CHA: return Cha;
                default: return 0;
            }
        }
        public Character GetClone(int Id)
        {
            Character newChar = (Character)this.MemberwiseClone();
            Id++;
            newChar.Id = Id;
            return newChar;
        }
        public string GetPlacementKey()
        {
            int max = Name.Length;
            if(max > 3)
            {
                max = 3;
            }
            return Name.Substring(0, max)+Id.ToString();
        }
        public Character GetClone()
        {
            Character newChar = (Character)this.MemberwiseClone();
            return newChar;
        }
    }
    public enum CharacterType
    {
        PC = 1,
        NPC = 2
    }
}
