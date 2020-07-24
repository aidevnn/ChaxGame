using System;
using System.Collections.Generic;
using System.Linq;

namespace ChaxGame.Moves
{
    public static class Generator
    {
        public static IEnumerable<IMove> RandomPlacement(Cube cube, Content player) => Placement(cube, player, MoveStrategy.Random);

        public static IEnumerable<IMove> PowerPlacement(Cube cube, Content player) => Placement(cube, player, MoveStrategy.Power);

        public static IEnumerable<IMove> Placement(Cube cube, Content player, MoveStrategy strategy = MoveStrategy.Domination)
        {
            SortedSet<MovePlacement> moves = new SortedSet<MovePlacement>();
            for (int idCell = 0; idCell < 24; ++idCell)
            {
                var c = cube.GetCell(idCell);
                if (c.Content != Content.Empty)
                    continue;

                int pow = strategy != MoveStrategy.Random ? 1 : 0;
                int dom = strategy == MoveStrategy.Domination ? 1 : 0;

                var mv = new MovePlacement(player, idCell);
                mv.Do(cube);
                mv.Weight = MainClass.Random.Next(100) + cube.ComputeDomination(player) * dom * 100 + 10000 * pow * c.Power;
                moves.Add(mv);
                mv.Undo(cube);
            }

            return moves;
        }

        public static IEnumerable<IMove> RandomFirstTurn(Cube cube, Content player) => FirstTurn(cube, player, MoveStrategy.Random);

        public static IEnumerable<IMove> PowerFirstTurn(Cube cube, Content player) => FirstTurn(cube, player, MoveStrategy.Power);

        public static IEnumerable<IMove> FirstTurn(Cube cube, Content player, MoveStrategy strategy = MoveStrategy.Domination)
        {
            SortedSet<MoveFirstTurn> moves = new SortedSet<MoveFirstTurn>();
            for (int idCell = 0; idCell < 24; ++idCell)
            {
                var c = cube.GetCell(idCell);
                if (c.Content != player)
                    continue;

                int pow = strategy != MoveStrategy.Random ? -1 : 0;
                int dom = strategy == MoveStrategy.Domination ? -1 : 0;

                var mv = new MoveFirstTurn(player, idCell);
                mv.Do(cube);
                mv.Weight = MainClass.Random.Next(100) + cube.ComputeDomination(player) * dom * 100 + 10000 * pow * c.Power;
                moves.Add(mv);
                mv.Undo(cube);
            }

            return moves;
        }

        public static void BuildMoveBattles(Cube cube, MoveBattle current, List<MoveBattle> moves, bool starting = true)
        {
            var cell = cube.GetCell(current.IdCellAfter);
            if (starting && cell.Content.DiffPlayer(current.Player))
                throw new Exception("Root move invalid.");

            foreach (var n in cell.Neighbors)
            {
                if (n.Content != Content.Empty)
                    continue;

                var mv = new MoveBattle(current.Player, current.IdCellBefore, n.Id);
                mv.KilledOpponents.AddRange(current.KilledOpponents);
                SetOppKill(cube, mv);

                if (starting)
                    moves.Add(mv);

                if (mv.KilledOpponents.Count > current.KilledOpponents.Count)
                {
                    if (!starting)
                        moves.Add(mv);

                    mv.Do(cube);
                    BuildMoveBattles(cube, mv, moves, false);
                    mv.Undo(cube);
                }
            }
        }

        public static void SetOppKill(Cube cube, MoveBattle mv)
        {
            var cell = cube.GetCell(mv.IdCellAfter);
            int nb = 0;
            var q = (-1, -1, -1);

            foreach(var row in cell.Rows)
            {
                var row0 = row[0];
                var c0 = row0.Content;
                var c1 = row[1].Content;

                if (mv.Player.DiffPlayer(c0) && mv.Player.SamePlayer(c1))
                {
                    if (nb == 0) q.Item2 = row0.Id;
                    else q.Item3 = row0.Id;

                    ++nb;
                }
            }

            if (nb != 0)
            {
                q.Item1 = mv.IdCellAfter;
                mv.KilledOpponents.Add(q);
            }
        }

        public static IEnumerable<IMove> BattleMoves(Cube cube, Content player)
        {
            SortedSet<MoveBattle> moves = new SortedSet<MoveBattle>();
            for(int i = 0; i < 24; ++i)
            {
                var cell = cube.GetCell(i);

            }

            if (moves.Count == 0)
                return new List<MovePass>() { new MovePass(player) };

            return moves;
        }
    }
}
