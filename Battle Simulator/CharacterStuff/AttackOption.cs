using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Battle_Simulator.CharacterStuff
{
    public class AttackOption: INotifyPropertyChanged
    {
        public string Name;
        public Dice DmgDice;
        public string DefaultDmgBonus;
        private Random rng;
        public AttackStatModifier AttributeMod = AttackStatModifier.STR;
        public int AttributeBonus;

        public event PropertyChangedEventHandler PropertyChanged;

        public AttackOption(Dice dmgdice,string DefaultDmgBonus)
        {
            DmgDice = dmgdice;
            this.DefaultDmgBonus = DefaultDmgBonus;
            rng = new Random();
        }
        public AttackOption()
        {

        }
        public void TriggerUpdate()
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
        }
        public void SetAttributeBonus(int mod)
        {
            AttributeBonus = mod;
        }
        public override string ToString()
        {
            return Name;
        }

        private int parseFlatDmgBonus()
        {
            return Int32.Parse(DefaultDmgBonus);
        }

        public int RollDamage()
        {
            return rng.Next(1, (int)DmgDice) + parseFlatDmgBonus() + AttributeBonus;
        }
    }

    public enum Dice
    {
        D3 = 3,
        D4 = 4,
        D6 = 6,
        D8 = 8,
        D10 = 10,
        D12 = 12,
        D20 = 20
    }
    public enum AttackStatModifier
    {
        NONE = 0,
        STR = 1,
        DEX = 2,
        CON = 3,
        INT = 4,
        WIS = 5,
        CHA = 6

    }
}
