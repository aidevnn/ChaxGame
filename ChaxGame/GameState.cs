﻿using System;
using System.Collections.Generic;
using System.Linq;

using ChaxGame.Moves;

namespace ChaxGame
{
    public class GameState
    {
        /// <summary>
        /// The cube.
        /// </summary>
        public Cube Cube = new Cube();

        /// <summary>
        /// The turn.
        /// </summary>
        public int Turn = 0;

        /// <summary>
        /// The player.
        /// </summary>
        public Content Player = Content.P1;

        public MoveStrategy Strategy;

        public GameState(MoveStrategy strategy)
        {
            Strategy = strategy;
        }

        public GameState(GameState g)
        {
            Cube.ImportState(g.Cube.ExportState());
            Turn = g.Turn;
            Player = g.Player;
            Strategy = g.Strategy;
        }

        public IEnumerable<IMove> GenMoves()
        {
            if (Turn < 24)
                return Generator.Placement(Cube, Player, Strategy);

            if (Turn == 24)
                return Generator.FirstTurn(Cube, Player, Strategy);

            return Generator.BattleMoves(Cube, Player);
        }

        public void Do(IMove move)
        {
            move.Do(Cube);
            ++Turn;
            Player = move.Player.GetOpponent();
        }

        public void Undo(IMove move)
        {
            move.Undo(Cube);
            --Turn;
            Player = move.Player;
        }

        public bool EndGame()
        {
            var sc = Cube.ComputeCubeScore(Player);
            return Turn > 2 && (sc.NbPlayer == 0 || sc.NbOpponent == 0);
        }

        public void DoMoveAndDisplay(IMove move)
        {
            Console.WriteLine(move);
            Console.WriteLine("#############");

            if (Turn < 25 || move.ActionType == ActionType.Pass)
            {
                Do(move);
                GridConsole.DisplayCube(Cube);
                Console.WriteLine("End Display");
                Console.ReadLine();
            }
            else
            {
                ++Turn;
                var mv = move as MoveBattle;

                int nb = mv.AllSteps.Count, k = 0;
                foreach (var ms in mv.AllSteps)
                {
                    ms.DoStep(Cube, mv.Player);
                    GridConsole.DisplayCube(Cube);
                    Console.WriteLine($"frame: {++k}/{nb}");
                    if (k != nb)
                        Console.ReadLine();
                }

                Console.WriteLine("End Display");
                Console.ReadLine();
                Player = move.Player.GetOpponent();
            }
        }
    }
}
