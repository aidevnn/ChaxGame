using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ChaxGame.Moves;

namespace ChaxGame
{
    public class Node
    {
        public Node Parent, BestChild;
        public IMove Move;
        public long Score;
        public bool Maximize = true;

        public Node()
        {

        }

        public Node(Node parent, IMove move)
        {
            Parent = parent;
            Move = move;
            Maximize = !parent.Maximize;
        }
    }

    public static class MinMaxAlgorithm
    {
        public const long INF = 1000000000000;

        public static IMove AlphaBeta(GameState g, int depth = 4)
        {
            var g0 = new GameState(g);
            var root = new Node();
            var sw = Stopwatch.StartNew();
            AlphaBeta(g0, root, g.Player, depth);
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds,5} ms");
            return root.BestChild.Move;
        }

        static void AlphaBeta(GameState g, Node node, Content Player, int depth, long alpha = -INF, long beta = INF)
        {
            if (node.Move != null)
            {
                var sc = g.Cube.ComputeCubeScore(node.Move.Player);
                if (sc.NbPlayer == 0)
                {
                    node.Score = -INF;
                    return;
                }

                if (sc.NbOpponent == 0)
                {
                    node.Score = +INF;
                    return;
                }

                if (depth == 0)
                {
                    var Opponent = Player.GetOpponent();
                    int dom = 50 + sc.Dom(Player) - sc.Dom(Opponent);
                    int nbPlayer = sc.Nb(Player), nbOpponent = sc.Nb(Opponent);
                    int diff = nbPlayer - nbOpponent;
                    node.Score = dom + (12 - nbOpponent) * 100 + nbPlayer * 10000 + diff * 1000000;
                    return;
                }
            }

            if (node.Maximize)
            {
                node.Score = -INF;
                var moves = g.GenMoves();
                node.BestChild = new Node(node, moves.First());
                foreach(var mv in moves)
                {
                    var child = new Node(node, mv);
                    AlphaBeta(g, child, Player, depth - 1, alpha, beta);
                    if (node.Score < child.Score)
                    {
                        node.BestChild = child;
                        node.Score = child.Score;
                    }

                    alpha = Math.Max(alpha, node.Score);
                    if (alpha > beta)
                        break;
                }
            }
            else
            {
                node.Score = +INF;
                var moves = g.GenMoves();
                node.BestChild = new Node(node, moves.First());
                foreach (var mv in moves)
                {
                    var child = new Node(node, mv);
                    AlphaBeta(g, child, Player, depth - 1, alpha, beta);
                    if (node.Score > child.Score)
                    {
                        node.BestChild = child;
                        node.Score = child.Score;
                    }

                    beta = Math.Min(beta, node.Score);
                    if (beta < alpha)
                        break;
                }
            }
        }
    }
}
