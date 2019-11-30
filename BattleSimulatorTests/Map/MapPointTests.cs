using Battle_Simulator.Map;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleSimulatorTests
{
    [TestFixture]
    public class MapPointTests
    {
        [Test]
        public void CTOR_MapPoint_ExpectedCoordinates()
        {
            MapPoint Point = new MapPoint(5, 3);

            Assert.AreEqual(5, Point.X);
            Assert.AreEqual(3, Point.Y);
        }
        [TestCase(5,4,5,3,true)]
        [TestCase(5,4,4,3,true)]
        [TestCase(5,4,3,3,false)]
        [TestCase(5,4,9,3,false)]
        public void IsAdjacent_TestCase_ExpectedResult(int X1,int Y1,int X2,int Y2, bool ExpectedResult)
        {
            MapPoint Point = new MapPoint(X1, Y2);
            MapPoint Other = new MapPoint(X2, Y2);
            Assert.AreEqual(ExpectedResult,Point.IsAdjacent(Other));
        }

        [TestCase(5,4,5,4,true)]
        [TestCase(5,4,5,3,false)]
        public void Equals_TestCase_ExpectedResult(int X1, int Y1, int X2, int Y2, bool ExpectedResult)
        {
            MapPoint Point = new MapPoint(X1, Y1);
            MapPoint Other = new MapPoint(X2, Y2);
            Assert.AreEqual(ExpectedResult, Point.Equals(Other));
        }
        [Test]
        public void Equals_WrongType_ThrowsArgumentException()
        {
            MapPoint Point = new MapPoint(0,0);
            Assert.Throws<ArgumentException>(() => Point.Equals(5));
        }
    }
}
