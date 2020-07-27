using NUnit.Framework;
using System;

using ChaxGame;

namespace TestNUnit
{
    [TestFixture()]
    public class Test1CubeDomination
    {
        [Test(Description ="Empty Cube")]
        public void TestCase01EmptyCube()
        {
            var c = new Cube();
            var s1 = c.ComputeCubeScore(Content.P1);
            var s2 = c.ComputeCubeScore(Content.P2);

            Assert.AreEqual(s1.NbPlayer, 0);
            Assert.AreEqual(s1.NbOpponent, 0);
            Assert.AreEqual(s2.NbPlayer, 0);
            Assert.AreEqual(s2.NbOpponent, 0);
            Assert.AreEqual(s1.DomPlayer, 0);
            Assert.AreEqual(s1.DomOpponent, 0);
            Assert.AreEqual(s2.DomPlayer, 0);
            Assert.AreEqual(s2.DomOpponent, 0);
        }

        [Test(Description = "One Cell Cube")]
        public void TestCase02OneCellCube()
        {
            var c = new Cube();
            c.SetCell((0, 0, 0), Content.P1);
            var s1 = c.ComputeCubeScore(Content.P1);
            var s2 = c.ComputeCubeScore(Content.P2);

            Assert.AreEqual(s1.NbPlayer, 1);
            Assert.AreEqual(s1.NbOpponent, 0);
            Assert.AreEqual(s2.NbPlayer, 0);
            Assert.AreEqual(s2.NbOpponent, 1);
            Assert.AreEqual(s1.DomPlayer, 40);
            Assert.AreEqual(s1.DomOpponent, 0);
            Assert.AreEqual(s2.DomPlayer, 0);
            Assert.AreEqual(s2.DomOpponent, 40);
        }

        [Test(Description = "Two Cells Cube (1)")]
        public void TestCase03TwoCellsCube()
        {
            var c = new Cube();
            c.SetCell((0, 0, 0), Content.P1);
            c.SetCell((2, 2, 0), Content.P2);
            var s1 = c.ComputeCubeScore(Content.P1);
            var s2 = c.ComputeCubeScore(Content.P2);

            Assert.AreEqual(s1.NbPlayer, 1);
            Assert.AreEqual(s2.NbPlayer, 1);
            Assert.AreEqual(s1.DomPlayer, 28);
            Assert.AreEqual(s1.DomOpponent, 12);
            Assert.AreEqual(s2.DomPlayer, 28);
            Assert.AreEqual(s2.DomOpponent, 12);
        }

        [Test(Description = "Two Cells Cube (2)")]
        public void TestCase04TwoCellsCube()
        {
            var c = new Cube();
            c.SetCell((0, 0, 0), Content.P1);
            c.SetCell((2, 0, 0), Content.P2);
            var s1 = c.ComputeCubeScore(Content.P1);
            var s2 = c.ComputeCubeScore(Content.P2);

            Assert.AreEqual(s1.NbPlayer, 1);
            Assert.AreEqual(s2.NbPlayer, 1);
            Assert.AreEqual(s1.DomPlayer, 22);
            Assert.AreEqual(s1.DomOpponent, 18);
            Assert.AreEqual(s2.DomPlayer, 22);
            Assert.AreEqual(s2.DomOpponent, 18);
        }
    }
}
