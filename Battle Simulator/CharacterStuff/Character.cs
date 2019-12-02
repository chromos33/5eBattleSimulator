using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Simulator.CharacterStuff
{
    public class Character
    {
        public int AC;
        public string Name;
        public Dice HPDice;
        public int ConMod;
        public int Level;
        public int TotalHealth;
        public Random rng;
        public CharacterType type;
        public List<AttackOption> AttackOptions;

        public override string ToString()
        {
            return Name;
        }
        //For NPC
        public Character(int AC,Dice HPDice,int ConMod,int Level, List<AttackOption> options = null)
        {
            rng = new Random();
            this.AC = AC;
            this.HPDice = HPDice;
            this.ConMod = ConMod;
            this.Level = Level;
            type = CharacterType.NPC;
            if(options != null)
            {
                AttackOptions = options;
            }
            else
            {
                AttackOptions = new List<AttackOption>();
            } 
        }
        //For PC
        public Character(int AC,int TotalHealth,List<AttackOption> options = null)
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
        public void AddAttackOption(AttackOption option)
        {

        }
    }
    public enum CharacterType
    {
        PC = 1,
        NPC = 2
    }
}
