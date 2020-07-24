using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChaxGame.Moves;

namespace ChaxGame
{
    class MainClass
    {
        public static Random Random = new Random();

        public static void DisplayMoves(Cube cube, List<MoveBattle> moves)
        {
            Console.WriteLine($"Nb Moves:{moves.Count}");
            Console.ReadLine();

            foreach (var mv in moves)
            {
                GridConsole.DisplayCube(cube);
                Console.WriteLine(mv);
                Console.ReadLine();

                mv.Do(cube);

                GridConsole.DisplayCube(cube);
                Console.ReadLine();

                mv.Undo(cube);
            }
        }

        static void TestMoves1()
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

            DisplayMoves(cube, moves);
        }

        static void TestMoves2()
        {
            var cube = new Cube();

            var cell1 = cube.GetCell((1, 0, 2));

            var cell2 = cube.GetCell((1, 0, 1));
            var cell3 = cube.GetCell((1, 0, 0));

            var cell4 = cube.GetCell((2, 0, 1));
            var cell5 = cube.GetCell((2, 0, 0));

            cell1.Content = Content.P1;
            cell3.Content = Content.P1;
            cell5.Content = Content.P1;

            cell2.Content = Content.P2;
            cell4.Content = Content.P2;

            var exportBefore = cube.ExportState();

            var root = new MoveBattle(Content.P1, cell1.Id);
            var moves = new List<MoveBattle>();
            Generator.BuildMoveBattles(cube, root, moves);
            var exportAfter = cube.ExportState();

            DisplayMoves(cube, moves);
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            TestMoves2();

        }
    }
}
