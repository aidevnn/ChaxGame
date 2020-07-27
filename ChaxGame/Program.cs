using System;

namespace ChaxGame
{
    class MainClass
    {
        public static Random Random = new Random();

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //DisplayDemo.Moves1();

            //Benchmark.BruteForce(4);

            var g = new GameState(MoveStrategy.Domination);
            while (true)
            {
                int depth = g.Turn < 25 ? 3 : 4;
                GridConsole.DisplayCube(g.Cube);
                Console.WriteLine("Compute....");

                var mv = MinMaxAlgorithm.AlphaBeta(g, depth);
                g.DoMoveAndDisplay(mv);
            }
        }
    }
}
