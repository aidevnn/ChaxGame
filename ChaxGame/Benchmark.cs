using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ChaxGame.Moves;

namespace ChaxGame
{
    public static class Benchmark
    {

        public static void MoveGen(int M = 5000)
        {
            for (int k = 2; k < 11; ++k)
            {
                Console.WriteLine($"Players : {k} x 2 tokens");
                var lt = Enumerable.Range(0, 24).OrderBy(z => MainClass.Random.NextDouble()).Take(k * 2).ToList();
                var cube = new Cube();
                for (int i = 0; i < k; ++i)
                {
                    cube.SetCell(lt[2 * i], Content.P1);
                    cube.SetCell(lt[2 * i + 1], Content.P2);
                }

                for (int a = 0; a < 5; ++a)
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    int s = 0;
                    for (int b = 0; b < M; ++b)
                    {
                        var moves = Generator.BattleMoves(cube, Content.P1);
                        s += moves.Count();
                    }
                    Console.WriteLine($"###### Time : {sw.ElapsedMilliseconds}");
                }
            }
        }

        public static void BruteForce(int depth = 4)
        {
            int nb = 0;
            void BruteForce(Cube cube, Content player, int depth0)
            {
                if (depth0 == 0)
                {
                    ++nb;
                    return;
                }

                var moves = Generator.BattleMoves(cube, player);
                foreach (var mv in moves)
                {
                    mv.Do(cube);
                    BruteForce(cube, player.GetOpponent(), depth0 - 1);
                    mv.Undo(cube);
                }
            }

            for (int k = 2; k < 12; ++k)
            {
                Console.WriteLine($"Players : {k} x 2 tokens");

                for (int a = 0; a < 5; ++a)
                {
                    nb = 0;
                    var lt = Enumerable.Range(0, 24).OrderBy(z => MainClass.Random.NextDouble()).Take(k * 2).ToList();
                    var cube = new Cube();
                    for (int i = 0; i < k; ++i)
                    {
                        cube.SetCell(lt[2 * i], Content.P1);
                        cube.SetCell(lt[2 * i + 1], Content.P2);
                    }

                    var export0 = cube.ExportState();
                    MoveBattle.NbSteps = 0;
                    Stopwatch sw = Stopwatch.StartNew();
                    BruteForce(cube, Content.P2, depth);

                    var export1 = cube.ExportState();
                    Console.WriteLine($"###### Time : {sw.ElapsedMilliseconds,4}; NbGames:{nb,8} / {MoveBattle.NbSteps,8}; export:{export0.Equals(export1)}");
                }
            }
        }

    }
}
