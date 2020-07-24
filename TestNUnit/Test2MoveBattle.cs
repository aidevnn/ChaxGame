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
            var cube = new Cube();

            var cell1 = cube.GetCell((2, 0, 0));
            var cell2 = cube.GetCell((2, 0, 1));

            var cell3 = cube.GetCell((1, 0, 1));
            var cell4 = cube.GetCell((0, 0, 1));

            var cell5 = cube.GetCell((2, 1, 1));
            var cell6 = cube.GetCell((2, 2, 1));

            cell1.Content = Content.P1;
            cell4.Content = Content.P1;
            cell6.Content = Content.P1;

            cell3.Content = Content.P2;
            cell5.Content = Content.P2;

            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, cell1.Id);
            var moves = new List<MoveBattle>();
            Generator.BuildMoveBattles(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 3);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            var coordsBefore = moves.Select(m => cube.GetCell(m.IdCellBefore).Coords).Distinct().ToList();
            Assert.IsTrue(coordsBefore.Contains(cell1.Coords));
            Assert.AreEqual(coordsBefore.Count, 1);

            var coordsAfter = moves.Select(m => cube.GetCell(m.IdCellAfter).Coords).Distinct().ToList();
            Assert.AreEqual(coordsAfter.Count, 3);
            Assert.IsTrue(coordsAfter.Contains(cell1.Neighbors[0].Coords));
            Assert.IsTrue(coordsAfter.Contains(cell1.Neighbors[1].Coords));
            Assert.IsTrue(coordsAfter.Contains(cell1.Neighbors[2].Coords));
            Assert.IsFalse(coordsAfter.Contains(cell1.Coords));

            int nbKills = moves.Sum(m => m.KilledOpponents.Count);
            Assert.AreEqual(nbKills, 1);

            var mvKill = moves.Find(m => m.IdCellAfter == cell2.Id);
            Assert.IsNotNull(mvKill);
            Assert.IsTrue(mvKill.KilledOpponents.Exists(e => e.Item1 == cell2.Id && e.Item2 == cell5.Id && e.Item3 == cell3.Id));
        }

        [Test(Description = "Kill two opponents (2)")]
        public void TestCase5KillTwoOpponents2()
        {
            var cube = new Cube();

            var cell1 = cube.GetCell((0, 0, 1));
            var cell2 = cube.GetCell((0, 0, 2));

            var cell3 = cube.GetCell((1, 0, 2));
            var cell4 = cube.GetCell((2, 0, 2));

            var cell5 = cube.GetCell((0, 1, 2));
            var cell6 = cube.GetCell((0, 2, 2));

            cell1.Content = Content.P1;
            cell4.Content = Content.P1;
            cell6.Content = Content.P1;

            cell3.Content = Content.P2;
            cell5.Content = Content.P2;

            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, cell1.Id);
            var moves = new List<MoveBattle>();
            Generator.BuildMoveBattles(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 4);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            var coordsBefore = moves.Select(m => cube.GetCell(m.IdCellBefore).Coords).Distinct().ToList();
            Assert.IsTrue(coordsBefore.Contains(cell1.Coords));
            Assert.AreEqual(coordsBefore.Count, 1);

            var coordsAfter = moves.Select(m => cube.GetCell(m.IdCellAfter).Coords).Distinct().ToList();
            Assert.AreEqual(coordsAfter.Count, 4);
            Assert.IsTrue(coordsAfter.Contains(cell1.Neighbors[0].Coords));
            Assert.IsTrue(coordsAfter.Contains(cell1.Neighbors[1].Coords));
            Assert.IsTrue(coordsAfter.Contains(cell1.Neighbors[2].Coords));
            Assert.IsTrue(coordsAfter.Contains(cell1.Neighbors[3].Coords));
            Assert.IsFalse(coordsAfter.Contains(cell1.Coords));

            int nbKills = moves.Sum(m => m.KilledOpponents.Count);
            Assert.AreEqual(nbKills, 1);

            var mvKill = moves.Find(m => m.IdCellAfter == cell2.Id);
            Assert.IsNotNull(mvKill);
            Assert.IsTrue(mvKill.KilledOpponents.Exists(e => e.Item1 == cell2.Id && e.Item2 == cell5.Id && e.Item3 == cell3.Id));
        }

        [Test(Description = "Kill two opponents with bonus (1)")]
        public void TestCase5KillTwoOpponentsBonus1()
        {

            var cube = new Cube();

            var cell1 = cube.GetCell((0, 0, 2));
            var cell20 = cube.GetCell((1, 0, 2));
            var cell21 = cube.GetCell((2, 0, 2));

            var cell3 = cube.GetCell((1, 0, 1));
            var cell4 = cube.GetCell((1, 0, 0));

            var cell5 = cube.GetCell((2, 0, 1));
            var cell6 = cube.GetCell((2, 0, 0));

            cell1.Content = Content.P1;
            cell4.Content = Content.P1;
            cell6.Content = Content.P1;

            cell3.Content = Content.P2;
            cell5.Content = Content.P2;

            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, cell1.Id);
            var moves = new List<MoveBattle>();
            Generator.BuildMoveBattles(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 4);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            var coordsBefore = moves.Select(m => cube.GetCell(m.IdCellBefore).Coords).Distinct().ToList();
            Assert.IsTrue(coordsBefore.Contains(cell1.Coords));
            Assert.AreEqual(coordsBefore.Count, 1);

            var coordsAfter = moves.Select(m => cube.GetCell(m.IdCellAfter).Coords).Distinct().ToList();
            Assert.AreEqual(coordsAfter.Count, 4);
            Assert.IsTrue(coordsAfter.Contains((1, 0, 2)));
            Assert.IsTrue(coordsAfter.Contains((0, 1, 2)));
            Assert.IsTrue(coordsAfter.Contains((0, 0, 1)));
            Assert.IsTrue(coordsAfter.Contains((2, 0, 2)));
            Assert.IsFalse(coordsAfter.Contains(cell1.Coords));

            int nbKills = moves.Max(m => m.KilledOpponents.Count);
            Assert.AreEqual(nbKills, 2);

            var mvKill0 = moves.Find(m => m.IdCellAfter == cell20.Id);
            Assert.IsNotNull(mvKill0);
            Assert.IsTrue(mvKill0.KilledOpponents.Exists(e => e.Item1 == cell20.Id && e.Item2 == cell3.Id));

            var mvKill1 = moves.Find(m => m.IdCellAfter == cell21.Id);
            Assert.IsNotNull(mvKill1);
            Assert.IsTrue(mvKill1.KilledOpponents.Exists(e => e.Item1 == cell20.Id && e.Item2 == cell3.Id));
            Assert.IsTrue(mvKill1.KilledOpponents.Exists(e => e.Item1 == cell21.Id && e.Item2 == cell5.Id));
        }
    }
}
