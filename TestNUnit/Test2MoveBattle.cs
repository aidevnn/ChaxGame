using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

using ChaxGame;
using ChaxGame.Moves;

namespace TestNUnit
{
    [TestFixture()]
    public class Test2MoveBattle
    {

        [Test(Description = "One Cell Cube")]
        public void TestCase1OneCell()
        {
            var cube = new Cube();
            cube.SetCell((0, 0, 0), Content.P1);
            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, 0);
            var moves = new List<MoveBattle>();
            Generator.BuildMoveBattles(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 3);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            var coordsBefore = moves.Select(m => cube.GetCell(m.IdCellBefore).Coords).Distinct().ToList();
            Assert.IsTrue(coordsBefore.Contains((0, 0, 0)));
            Assert.AreEqual(coordsBefore.Count, 1);

            var coordsAfter = moves.Select(m => cube.GetCell(m.IdCellAfter).Coords).Distinct().ToList();
            Assert.AreEqual(coordsAfter.Count, 3);
            Assert.IsTrue(coordsAfter.Contains((0, 1, 0)));
            Assert.IsTrue(coordsAfter.Contains((1, 0, 0)));
            Assert.IsTrue(coordsAfter.Contains((0, 0, 1)));
            Assert.IsFalse(coordsAfter.Contains((0, 0, 0)));

            int nbKills = moves.Sum(m => m.KilledOpponents.Count);
            Assert.AreEqual(nbKills, 0);
        }

        [Test(Description = "Two Cells Cube with obstruction (1)")]
        public void TestCase2TwoCells1()
        {
            var cube = new Cube();
            cube.SetCell((0, 0, 0), Content.P1);
            cube.SetCell((0, 0, 1), Content.P2);
            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, 0);
            var moves = new List<MoveBattle>();
            Generator.BuildMoveBattles(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 2);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            var coordsBefore = moves.Select(m => cube.GetCell(m.IdCellBefore).Coords).Distinct().ToList();
            Assert.IsTrue(coordsBefore.Contains((0, 0, 0)));
            Assert.AreEqual(coordsBefore.Count, 1);

            var coordsAfter = moves.Select(m => cube.GetCell(m.IdCellAfter).Coords).Distinct().ToList();
            Assert.AreEqual(coordsAfter.Count, 2);
            Assert.IsTrue(coordsAfter.Contains((0, 1, 0)));
            Assert.IsTrue(coordsAfter.Contains((1, 0, 0)));
            Assert.IsFalse(coordsAfter.Contains((0, 0, 1)));
            Assert.IsFalse(coordsAfter.Contains((0, 0, 0)));

            int nbKills = moves.Sum(m => m.KilledOpponents.Count);
            Assert.AreEqual(nbKills, 0);
        }

        [Test(Description = "Two Cells Cube without obstruction (2)")]
        public void TestCase2TwoCells2()
        {
            var cube = new Cube();
            cube.SetCell((0, 0, 0), Content.P1);
            cube.SetCell((0, 0, 2), Content.P2);
            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, 0);
            var moves = new List<MoveBattle>();
            Generator.BuildMoveBattles(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 3);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            var coordsBefore = moves.Select(m => cube.GetCell(m.IdCellBefore).Coords).Distinct().ToList();
            Assert.IsTrue(coordsBefore.Contains((0, 0, 0)));
            Assert.AreEqual(coordsBefore.Count, 1);

            var coordsAfter = moves.Select(m => cube.GetCell(m.IdCellAfter).Coords).Distinct().ToList();
            Assert.AreEqual(coordsAfter.Count, 3);
            Assert.IsTrue(coordsAfter.Contains((0, 1, 0)));
            Assert.IsTrue(coordsAfter.Contains((1, 0, 0)));
            Assert.IsTrue(coordsAfter.Contains((0, 0, 1)));
            Assert.IsFalse(coordsAfter.Contains((0, 0, 0)));

            int nbKills = moves.Sum(m => m.KilledOpponents.Count);
            Assert.AreEqual(nbKills, 0);
        }

        [Test(Description = "Three Cells Cube with killed opponent (1)")]
        public void TestCase4ThreeCellsOneKill1()
        {
            var cube = new Cube();
            cube.SetCell((0, 0, 0), Content.P1);
            cube.SetCell((0, 1, 2), Content.P1);
            cube.SetCell((0, 1, 1), Content.P2);
            var cell0 = cube.GetCell((0, 1, 0));
            var cell1 = cube.GetCell((0, 1, 1));
            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, 0);
            var moves = new List<MoveBattle>();
            Generator.BuildMoveBattles(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 3);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            var coordsBefore = moves.Select(m => cube.GetCell(m.IdCellBefore).Coords).Distinct().ToList();
            Assert.IsTrue(coordsBefore.Contains((0, 0, 0)));
            Assert.AreEqual(coordsBefore.Count, 1);

            var coordsAfter = moves.Select(m => cube.GetCell(m.IdCellAfter).Coords).Distinct().ToList();
            Assert.AreEqual(coordsAfter.Count, 3);
            Assert.IsTrue(coordsAfter.Contains((0, 1, 0)));
            Assert.IsTrue(coordsAfter.Contains((1, 0, 0)));
            Assert.IsTrue(coordsAfter.Contains((0, 0, 1)));
            Assert.IsFalse(coordsAfter.Contains((0, 0, 0)));

            int nbKills = moves.Sum(m => m.KilledOpponents.Count);
            Assert.AreEqual(nbKills, 1);

            var mvKill = moves.Find(m => m.IdCellAfter == cell0.Id);
            Assert.IsNotNull(mvKill);
            Assert.IsTrue(mvKill.KilledOpponents.Exists(e => e.Item1 == cell0.Id && e.Item2 == cell1.Id));
        }

        [Test(Description = "Three Cells Cube with killed opponent (2)")]
        public void TestCase4ThreeCellsOneKill2()
        {
            var cube = new Cube();
            cube.SetCell((0, 2, 0), Content.P1);
            cube.SetCell((0, 0, 1), Content.P1);
            cube.SetCell((0, 1, 1), Content.P2);
            var cell0 = cube.GetCell((0, 2, 1));
            var cell1 = cube.GetCell((0, 1, 1));
            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, cube.GetCell((0, 2, 0)).Id);
            var moves = new List<MoveBattle>();
            Generator.BuildMoveBattles(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 3);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            var coordsBefore = moves.Select(m => cube.GetCell(m.IdCellBefore).Coords).Distinct().ToList();
            Assert.IsTrue(coordsBefore.Contains((0, 2, 0)));
            Assert.AreEqual(coordsBefore.Count, 1);

            var coordsAfter = moves.Select(m => cube.GetCell(m.IdCellAfter).Coords).Distinct().ToList();
            Assert.AreEqual(coordsAfter.Count, 3);
            Assert.IsTrue(coordsAfter.Contains((0, 1, 0)));
            Assert.IsTrue(coordsAfter.Contains((1, 2, 0)));
            Assert.IsTrue(coordsAfter.Contains((0, 2, 1)));
            Assert.IsFalse(coordsAfter.Contains((0, 0, 0)));

            int nbKills = moves.Sum(m => m.KilledOpponents.Count);
            Assert.AreEqual(nbKills, 1);

            var mvKill = moves.Find(m => m.IdCellAfter == cell0.Id);
            Assert.IsNotNull(mvKill);
            Assert.IsTrue(mvKill.KilledOpponents.Exists(e => e.Item1 == cell0.Id && e.Item2 == cell1.Id));
        }

        [Test(Description = "Kill two opponents (1)")]
        public void TestCase5KillTwoOpponents1()
        {

        }
    }
}
