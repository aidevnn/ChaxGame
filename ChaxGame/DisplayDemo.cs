using System;
using System.Collections.Generic;

using ChaxGame.Moves;

namespace ChaxGame
{
    public static class DisplayDemo
    {

        public static void DisplayMoves(Cube cube, SortedSet<MoveBattle> moves)
        {
            Console.WriteLine($"Nb Moves:{moves.Count}");
            Console.ReadLine();

            var export0 = cube.ExportState();

            foreach (var mv in moves)
            {
                GridConsole.DisplayCube(cube);
                Console.WriteLine(mv);
                Console.WriteLine("#############");
                Console.ReadLine();

                foreach (var ms in mv.AllSteps)
                {
                    ms.DoStep(cube, mv.Player);
                    GridConsole.DisplayCube(cube);
                    Console.ReadLine();
                }

                mv.Undo(cube);

                var export1 = cube.ExportState();
                if (!export0.Equals(export1))
                    throw new Exception();
            }
        }

        public static void Moves1()
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

            DisplayMoves(cube, moves);
        }

        public static void Moves2()
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

            DisplayMoves(cube, moves);
        }

        public static void Moves3()
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

            var moves = Generator.BattleMoves(cube, Content.P1) as SortedSet<MoveBattle>;

            DisplayMoves(cube, moves);
        }
    }
}
