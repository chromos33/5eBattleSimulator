using Battle_Simulator.Map;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleSimulatorTests
{
    [TestFixture]
    public class MapTest
    {
        [Test]
        public void CTOR_Input4times5_MapSquareCount20()
        {
            Map map = new Map(4, 5,"");
            Assert.AreEqual(20, map.SquareCount);
        }
    }
}
