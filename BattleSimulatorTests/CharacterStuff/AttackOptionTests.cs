using Battle_Simulator.CharacterStuff;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleSimulatorTests.CharacterStuff
{
    [TestFixture]
    public class AttackOptionTests
    {
        [Test]
        public void RollDamage_D12_ReturnsBiggerThan0()
        {
            AttackOption option = new AttackOption(Dice.D12, "0");
            Assert.AreNotEqual(0, option.RollDamage());
        }
    }
}
