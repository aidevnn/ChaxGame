using NUnit.Framework;
using System;

using ChaxGame;

namespace TestNUnit
{
    [TestFixture()]
    public class Test1CubeDomination
    {
        [Test(Description ="Empty Cube")]
        public void TestCaseEmptyCube()
        {
            var c = new Cube();
            var s1 = c.ComputeDomination(Content.P1);
            var s2 = c.ComputeDomination(Content.P2);
            Assert.AreEqual(s1, 50);
            Assert.AreEqual(s2, 50);
        }

        [Test(Description = "One Cell Cube")]
        public void TestCaseOneCellCube()
        {
            var c = new Cube();
            c.SetCell((0, 0, 0), Content.P1);
            var s1 = c.ComputeDomination(Content.P1);
            var s2 = c.ComputeDomination(Content.P2);
            Assert.AreEqual(s1, 90);
            Assert.AreEqual(s2, 10);
        }

        [Test(Description = "Two Cells Cube (1)")]
        public void TestCaseTwoCellsCube1()
        {
            var c = new Cube();
            c.SetCell((0, 0, 0), Content.P1);
            c.SetCell((2, 2, 0), Content.P2);
            var s1 = c.ComputeDomination(Content.P1);
            var s2 = c.ComputeDomination(Content.P2);
            Assert.AreEqual(s1, 66); // 50 + 28 - 12 = 66
            Assert.AreEqual(s2, 66); // 50 + 28 - 12 = 66
        }

        [Test(Description = "Two Cells Cube (2)")]
        public void TestCaseTwoCellsCube2()
        {
            var c = new Cube();
            c.SetCell((0, 0, 0), Content.P1);
            c.SetCell((2, 0, 0), Content.P2);
            var s1 = c.ComputeDomination(Content.P1);
            var s2 = c.ComputeDomination(Content.P2);
            Assert.AreEqual(s1, 54); // 50 + 22 - 18 = 54
            Assert.AreEqual(s2, 54); // 50 + 22 - 18 = 54
        }
    }
}
