using Battle_Simulator.Map;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleSimulatorTests
{
    [TestFixture]
    public class MapSquareTests
    {
        [Test]
        public void CTOR_CorrdsWall_CorrectMapSquare()
        {
            MapSquare square = new MapSquare(5, 4, SquareType.Wall);
            Assert.AreEqual(SquareType.Wall, square.SquareType);
            Assert.IsNotNull(square.Coordinates);
            Assert.IsTrue(square.Coordinates.Equals(new MapPoint(5, 4)));
        }
        [TestCase(SquareType.Wall,false)]
        [TestCase(SquareType.Path,true)]
        [TestCase(SquareType.RangePassThrough,false)]
        [TestCase(SquareType.Undefined, false)]
        public void IsWalkable_TestCase_ExpectedResult(SquareType type,bool ExpectedResult)
        {
            MapSquare square = new MapSquare(0, 0, type);
            Assert.AreEqual(ExpectedResult, square.IsWalkable());
        }
        [TestCase(SquareType.Wall, false)]
        [TestCase(SquareType.Path, true)]
        [TestCase(SquareType.RangePassThrough, true)]
        [TestCase(SquareType.Undefined, false)]
        public void CanShootThrough_TestCase_ExpectedResult(SquareType type, bool ExpectedResult)
        {
            MapSquare square = new MapSquare(0, 0, type);
            Assert.AreEqual(ExpectedResult, square.CanShootThrough());
        }
    }
}
