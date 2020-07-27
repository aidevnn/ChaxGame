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
        public void TestCase01OneCell()
        {
            var cube = new Cube();
            cube.SetCell((0, 0, 0), Content.P1);
            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, 0);
            var moves = new SortedSet<MoveBattle>();
            Generator.BuildMoveBattle(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 3);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            var coordsBefore = moves.Select(m => cube.GetCell(m.IdBefore).Coords).Distinct().ToList();
            Assert.IsTrue(coordsBefore.Contains((0, 0, 0)));
            Assert.AreEqual(coordsBefore.Count, 1);

            var coordsAfter = moves.Select(m => cube.GetCell(m.IdAfter).Coords).Distinct().ToList();
            Assert.AreEqual(coordsAfter.Count, 3);
            Assert.IsTrue(coordsAfter.Contains((0, 1, 0)));
            Assert.IsTrue(coordsAfter.Contains((1, 0, 0)));
            Assert.IsTrue(coordsAfter.Contains((0, 0, 1)));
            Assert.IsFalse(coordsAfter.Contains((0, 0, 0)));

            int nbKills = moves.Max(m => m.TotalKills);
            Assert.AreEqual(nbKills, 0);

            foreach(var mv in moves)
            {
                mv.Do(cube);
                mv.Undo(cube);
                var export = cube.ExportState();
                Assert.IsTrue(exportBefore.Equals(export));
            }
        }

        [Test(Description = "Two Cells Cube with obstruction (1)")]
        public void TestCase02TwoCells()
        {
            var cube = new Cube();
            cube.SetCell((0, 0, 0), Content.P1);
            cube.SetCell((0, 0, 1), Content.P2);
            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, 0);
            var moves = new SortedSet<MoveBattle>();
            Generator.BuildMoveBattle(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 2);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            var coordsBefore = moves.Select(m => cube.GetCell(m.IdBefore).Coords).Distinct().ToList();
            Assert.IsTrue(coordsBefore.Contains((0, 0, 0)));
            Assert.AreEqual(coordsBefore.Count, 1);

            var coordsAfter = moves.Select(m => cube.GetCell(m.IdAfter).Coords).Distinct().ToList();
            Assert.AreEqual(coordsAfter.Count, 2);
            Assert.IsTrue(coordsAfter.Contains((0, 1, 0)));
            Assert.IsTrue(coordsAfter.Contains((1, 0, 0)));
            Assert.IsFalse(coordsAfter.Contains((0, 0, 1)));
            Assert.IsFalse(coordsAfter.Contains((0, 0, 0)));

            int nbKills = moves.Max(m => m.TotalKills);
            Assert.AreEqual(nbKills, 0);

            foreach (var mv in moves)
            {
                mv.Do(cube);
                mv.Undo(cube);
                var export = cube.ExportState();
                Assert.IsTrue(exportBefore.Equals(export));
            }
        }

        [Test(Description = "Two Cells Cube without obstruction (2)")]
        public void TestCase03TwoCells()
        {
            var cube = new Cube();
            cube.SetCell((0, 0, 0), Content.P1);
            cube.SetCell((0, 0, 2), Content.P2);
            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, 0);
            var moves = new SortedSet<MoveBattle>();
            Generator.BuildMoveBattle(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 3);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            var coordsBefore = moves.Select(m => cube.GetCell(m.IdBefore).Coords).Distinct().ToList();
            Assert.IsTrue(coordsBefore.Contains((0, 0, 0)));
            Assert.AreEqual(coordsBefore.Count, 1);

            var coordsAfter = moves.Select(m => cube.GetCell(m.IdAfter).Coords).Distinct().ToList();
            Assert.AreEqual(coordsAfter.Count, 3);
            Assert.IsTrue(coordsAfter.Contains((0, 1, 0)));
            Assert.IsTrue(coordsAfter.Contains((1, 0, 0)));
            Assert.IsTrue(coordsAfter.Contains((0, 0, 1)));
            Assert.IsFalse(coordsAfter.Contains((0, 0, 0)));

            int nbKills = moves.Max(m => m.TotalKills);
            Assert.AreEqual(nbKills, 0);

            foreach (var mv in moves)
            {
                mv.Do(cube);
                mv.Undo(cube);
                var export = cube.ExportState();
                Assert.IsTrue(exportBefore.Equals(export));
            }
        }

        [Test(Description = "Three Cells Cube with killed opponent (1)")]
        public void TestCase04ThreeCellsOneKill()
        {
            var cube = new Cube();
            cube.SetCell((0, 0, 0), Content.P1);
            cube.SetCell((0, 1, 2), Content.P1);
            cube.SetCell((0, 1, 1), Content.P2);
            var cell0 = cube.GetCell((0, 1, 0));
            var cell1 = cube.GetCell((0, 1, 1));
            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, 0);
            var moves = new SortedSet<MoveBattle>();
            Generator.BuildMoveBattle(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 3);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            int nbKills = moves.Max(m => m.TotalKills);
            Assert.AreEqual(nbKills, 1);

            var mvKill = moves.FirstOrDefault(m => m.IdAfter == cell0.Id);

            Assert.IsNotNull(mvKill);
            Assert.IsTrue(mvKill.AllSteps.Exists(e => e.idAfter == cell0.Id && e.idOpp1 == cell1.Id));

            foreach (var mv in moves)
            {
                mv.Do(cube);
                mv.Undo(cube);
                var export = cube.ExportState();
                Assert.IsTrue(exportBefore.Equals(export));
            }
        }

        [Test(Description = "Three Cells Cube with killed opponent (2)")]
        public void TestCase05ThreeCellsOneKill()
        {
            var cube = new Cube();
            cube.SetCell((0, 2, 0), Content.P1);
            cube.SetCell((0, 0, 1), Content.P1);
            cube.SetCell((0, 1, 1), Content.P2);
            var cell0 = cube.GetCell((0, 2, 1));
            var cell1 = cube.GetCell((0, 1, 1));
            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, cube.GetCell((0, 2, 0)).Id);
            var moves = new SortedSet<MoveBattle>();
            Generator.BuildMoveBattle(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 3);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            int nbKills = moves.Sum(m => m.TotalKills);
            Assert.AreEqual(nbKills, 1);

            var mvKill = moves.First(m => m.IdAfter == cell0.Id);
            Assert.IsNotNull(mvKill);
            Assert.IsTrue(mvKill.AllSteps.Exists(e => e.idAfter == cell0.Id && e.idOpp1 == cell1.Id));

            foreach (var mv in moves)
            {
                mv.Do(cube);
                mv.Undo(cube);
                var export = cube.ExportState();
                Assert.IsTrue(exportBefore.Equals(export));
            }
        }

        [Test(Description = "Kill two opponents (1)")]
        public void TestCase06KillTwoOpponents()
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
            var moves = new SortedSet<MoveBattle>();
            Generator.BuildMoveBattle(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 3);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            int nbKills = moves.Sum(m => m.TotalKills);
            Assert.AreEqual(nbKills, 2);

            var mvKill = moves.First(m => m.IdAfter == cell2.Id);
            Assert.IsNotNull(mvKill);
            Assert.IsTrue(mvKill.AllSteps.Exists(e => e.idAfter == cell2.Id && e.idOpp1 == cell5.Id && e.idOpp2 == cell3.Id));

            foreach (var mv in moves)
            {
                mv.Do(cube);
                mv.Undo(cube);
                var export = cube.ExportState();
                Assert.IsTrue(exportBefore.Equals(export));
            }
        }

        [Test(Description = "Kill two opponents (2)")]
        public void TestCase07KillTwoOpponents()
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
            var moves = new SortedSet<MoveBattle>();
            Generator.BuildMoveBattle(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 4);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            int nbKills = moves.Max(m => m.TotalKills);
            Assert.AreEqual(nbKills, 2);

            var mvKill = moves.First(m => m.IdAfter == cell2.Id);
            Assert.IsNotNull(mvKill);
            Assert.IsTrue(mvKill.AllSteps.Exists(e => e.idAfter == cell2.Id && e.idOpp1 == cell5.Id && e.idOpp2 == cell3.Id));

            foreach (var mv in moves)
            {
                mv.Do(cube);
                mv.Undo(cube);
                var export = cube.ExportState();
                Assert.IsTrue(exportBefore.Equals(export));
            }
        }

        [Test(Description = "Kill two opponents with bonus (1)")]
        public void TestCase08KillTwoOpponentsBonus()
        {
            var cube = new Cube();

            var cell1 = cube.GetCell((0, 0, 2));
            var cell2 = cube.GetCell((1, 0, 2));
            var cell3 = cube.GetCell((2, 0, 2));

            var cell4 = cube.GetCell((1, 0, 1));
            var cell5 = cube.GetCell((1, 0, 0));

            var cell6 = cube.GetCell((2, 0, 1));
            var cell7 = cube.GetCell((2, 0, 0));

            cell1.Content = Content.P1;
            cell5.Content = Content.P1;
            cell7.Content = Content.P1;

            cell4.Content = Content.P2;
            cell6.Content = Content.P2;

            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, cell1.Id);
            var moves = new SortedSet<MoveBattle>();
            Generator.BuildMoveBattle(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 4);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            int nbKills = moves.Max(m => m.TotalKills);
            Assert.AreEqual(nbKills, 2);

            var mvKill0 = moves.First(m => m.IdAfter == cell2.Id);
            Assert.IsNotNull(mvKill0);
            Assert.IsTrue(mvKill0.AllSteps.Exists(e => e.idAfter == cell2.Id && e.idOpp1 == cell4.Id));

            var mvKill1 = moves.First(m => m.IdAfter == cell3.Id);
            Assert.IsNotNull(mvKill1);
            Assert.IsTrue(mvKill1.AllSteps.Exists(e => e.idAfter == cell2.Id && e.idOpp1 == cell4.Id));
            Assert.IsTrue(mvKill1.AllSteps.Exists(e => e.idAfter == cell3.Id && e.idOpp1 == cell6.Id));

            foreach (var mv in moves)
            {
                mv.Do(cube);
                mv.Undo(cube);
                var export = cube.ExportState();
                Assert.IsTrue(exportBefore.Equals(export));
            }
        }

        [Test(Description = "Kill two opponents with bonus (2)")]
        public void TestCase09KillTwoOpponentsBonus()
        {
            var cube = new Cube();

            var cell1 = cube.GetCell((1, 0, 2));
            var cell2 = cube.GetCell((2, 0, 2));

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
            var moves = new SortedSet<MoveBattle>();
            Generator.BuildMoveBattle(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 3);
            Assert.IsTrue(exportBefore.Equals(exportAfter));

            int nbKills = moves.Max(m => m.TotalKills);
            Assert.AreEqual(nbKills, 2);

            var mvKill0 = moves.First(m => m.IdAfter == cell2.Id);
            Assert.IsNotNull(mvKill0);
            Assert.IsTrue(mvKill0.AllSteps.Exists(e => e.idAfter == cell2.Id && e.idOpp1 == cell5.Id));

            var mvKill1 = moves.First(m => m.IdAfter == cell1.Id);
            Assert.IsNotNull(mvKill1);
            Assert.IsTrue(mvKill1.AllSteps.Exists(e => e.idAfter == cell2.Id && e.idOpp1 == cell5.Id));
            Assert.IsTrue(mvKill1.AllSteps.Exists(e => e.idAfter == cell1.Id && e.idOpp1 == cell3.Id));

            foreach (var mv in moves)
            {
                mv.Do(cube);
                mv.Undo(cube);
                var export = cube.ExportState();
                Assert.IsTrue(exportBefore.Equals(export));
            }
        }

        [Test(Description = "Kill three opponents")]
        public void TestCase10KillThreeOpponents()
        {
            var cube = new Cube();

            var cell1 = cube.GetCell((0, 0, 2));

            var cell2 = cube.GetCell((0, 1, 1));
            var cell3 = cube.GetCell((0, 1, 0));

            var cell4 = cube.GetCell((0, 2, 1));
            var cell5 = cube.GetCell((0, 2, 0));

            var cell6 = cube.GetCell((1, 2, 1));
            var cell7 = cube.GetCell((2, 2, 1));

            cell1.Content = Content.P1;
            cell3.Content = Content.P1;
            cell5.Content = Content.P1;
            cell7.Content = Content.P1;

            cell2.Content = Content.P2;
            cell4.Content = Content.P2;
            cell6.Content = Content.P2;

            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, cell1.Id);
            var moves = new SortedSet<MoveBattle>();
            Generator.BuildMoveBattle(cube, root, moves);
            var exportAfter = cube.ExportState();

            Assert.AreEqual(moves.Count, 5);

            int nbKills = moves.Max(m => m.TotalKills);
            Assert.AreEqual(nbKills, 3);

            foreach (var mv in moves)
            {
                mv.Do(cube);
                mv.Undo(cube);
                var export = cube.ExportState();
                Assert.IsTrue(exportBefore.Equals(export));
            }
        }

    }
}
