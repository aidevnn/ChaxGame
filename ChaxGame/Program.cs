using System;

namespace ChaxGame
{
    class MainClass
    {
        public static Random Random = new Random(2543);

        public static void Main(string[] args)
        {
            //DisplayDemo.Moves1();
            //Benchmark.BruteForce(6);

            var g = new GameState(MoveStrategy.Domination);
            g.Cube.ImportState("112001122011101101000001");g.Turn = 39;g.Player = Content.P1;
            while (!g.EndGame())
            {
                int depth = g.Turn < 25 ? 3 : 6;
                GridConsole.DisplayCube(g.Cube);
                Console.WriteLine("Compute....");

                var mv = MinMaxAlgorithm.AlphaBeta(g, depth);
                g.DoMoveAndDisplay(mv);
            }
        }
    }
}
