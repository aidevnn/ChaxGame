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

        static void DisplayMoves1()
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

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            DisplayMoves1();
        }
    }
}
