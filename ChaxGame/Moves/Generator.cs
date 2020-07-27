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

        public static void BuildMoveBattleStep(Cube cube, MoveBattle current, SortedSet<MoveBattle> moves)
        {
            var cell = cube.GetCell(current.IdAfter);
            if (current.Steps == 0 && cell.Content.DiffPlayer(current.Player))
                throw new Exception("Root move invalid.");

            foreach (var n in cell.Neighbors)
            {
                if (n.Content != Content.Empty)
                    continue;

                var mv = new MoveBattle(current, current.IdBefore, n.Id);
                var ms = BuildSubMove(cube, current.Player, current.IdAfter, n.Id);
                mv.TotalKills += ms.nbKills;
                mv.AllSteps.Add(ms);

                ms.DoStep(cube, mv.Player);

                if (mv.Steps == 1 || ms.nbKills != 0)
                {
                    mv.Weight =MainClass.Random.Next(100) + n.Power * 100 + mv.TotalKills * 1000;
                    moves.Add(mv);
                }

                if (ms.nbKills != 0)
                    BuildMoveBattleStep(cube, mv, moves);

                ms.UndoStep(cube, mv.Player, mv.Opponent);
            }
        }

        public static SubMove BuildSubMove(Cube cube, Content player, int idBefore, int idAfter)
        {
            var cell = cube.GetCell(idAfter);
            var ms = new SubMove() { idBefore = idBefore, idAfter = idAfter, idOpp1 = -1, idOpp2 = -1 };

            foreach (var row in cell.Rows)
            {
                var row0 = row[0];
                var c0 = row0.Content;
                var c1 = row[1].Content;

                if (player.DiffPlayer(c0) && player.SamePlayer(c1))
                {
                    if (ms.nbKills == 0) ms.idOpp1 = row0.Id;
                    else ms.idOpp2 = row0.Id;

                    ++ms.nbKills;
                }
            }

            return ms;
        }

        public static IEnumerable<IMove> BattleMoves(Cube cube, Content player)
        {
            SortedSet<MoveBattle> moves = new SortedSet<MoveBattle>();
            for(int i = 0; i < 24; ++i)
            {
                var cell = cube.GetCell(i);
                if (cell.Content != player)
                    continue;

                BuildMoveBattleStep(cube, new MoveBattle(player, cell.Id), moves);
            }

            if (moves.Count == 0)
                return new List<MovePass>() { new MovePass(player) };

            return moves;
        }
    }
}
