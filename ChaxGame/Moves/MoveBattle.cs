using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChaxGame.Moves
{
    /// <summary>
    /// Move battle.
    /// </summary>
    public class MoveBattle : IMove, IComparable<MoveBattle>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ChaxGame.Moves.MoveBattle"/> class.
        /// </summary>
        /// <param name="player">Player.</param>
        /// <param name="idCellBefore">Identifier cell before.</param>
        public MoveBattle(Content player, int idCellBefore)
        {
            Player = player;
            IdCellBefore = IdCellAfter = idCellBefore;
            ActionType = ActionType.Battle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ChaxGame.Moves.MoveBattle"/> class.
        /// </summary>
        /// <param name="player">Player.</param>
        /// <param name="idCellBefore">Identifier cell before.</param>
        /// <param name="idCellAfter">Identifier cell after.</param>
        public MoveBattle(Content player, int idCellBefore, int idCellAfter)
        {
            Player = player;
            IdCellBefore = idCellBefore;
            IdCellAfter = idCellAfter;
            ActionType = ActionType.Battle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ChaxGame.Moves.MoveBattle"/> class.
        /// </summary>
        /// <param name="move">Move.</param>
        public MoveBattle(MoveBattle move)
        {
            ActionType = ActionType.Battle;
            Player = move.Player;
            IdCellBefore = move.IdCellBefore;
            IdCellAfter = move.IdCellAfter;
            Weight = move.Weight;
            KilledOpponents.AddRange(move.KilledOpponents);
        }

        public int CompareTo(MoveBattle other)
        {
            return Weight.CompareTo(other.Weight) > 0 ? 1 : -1;
        }

        /// <summary>
        /// Gets or sets the identifier cell before the move.
        /// </summary>
        /// <value>The identifier cell before.</value>
        public int IdCellBefore { get; set; }

        /// <summary>
        /// Gets or sets the identifier cell after the move.
        /// </summary>
        /// <value>The identifier cell after.</value>
        public int IdCellAfter { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>The player.</value>
        public Content Player { get; set; }

        /// <summary>
        /// Gets or sets the type of the action.
        /// </summary>
        /// <value>The type of the action.</value>
        public ActionType ActionType { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>The weight.</value>
        public int Weight { get; set; }

        /// <summary>
        /// Clone this instance.
        /// </summary>
        /// <returns>The clone.</returns>
        public MoveBattle Clone => new MoveBattle(this);

        /// <summary>
        /// The killed opponents cells identifiers.
        /// It will also be helpfull for displaying frames during a game.
        /// First int of tuple is for cell identifier to move in.
        /// Second list int of tuple is for killed opponents identifiers.
        /// </summary>
        public List<(int, int, int)> KilledOpponents = new List<(int, int, int)>(12);

        /// <summary>
        /// Do move on the specified cube.
        /// </summary>
        /// <param name="cube">Cube.</param>
        public void Do(Cube cube)
        {
            cube.SetCell(IdCellBefore, Content.Empty);
            cube.SetCell(IdCellAfter, Player);

            foreach (var e in KilledOpponents)
            {
                if (e.Item2 != -1) cube.SetCell(e.Item2, Content.Empty);
                if (e.Item3 != -1) cube.SetCell(e.Item3, Content.Empty);
            }
        }

        /// <summary>
        /// Undo move on the specified cube.
        /// </summary>
        /// <param name="cube">Cube.</param>
        public void Undo(Cube cube)
        {
            var opponent = Player.GetOpponent();
            cube.SetCell(IdCellAfter, Content.Empty);
            cube.SetCell(IdCellBefore, Player);

            foreach (var e in KilledOpponents)
            {
                if (e.Item2 != -1) cube.SetCell(e.Item2, opponent);
                if (e.Item3 != -1) cube.SetCell(e.Item3, opponent);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var cb = Cube.Id2Coords[IdCellBefore];
            var ca = Cube.Id2Coords[IdCellAfter];
            var title = $"Player:{Player} MoveBattle; Weight: {Weight}";
            sb.AppendLine(title); 
            var head = $"From: [{IdCellBefore,2:00}]{cb}";
            var tail = $"To  : [{IdCellAfter,2:00}]{ca}";
            sb.AppendLine(head);
            if (KilledOpponents.Count != 0)
            {
                sb.AppendLine("  Frames.");
                foreach(var e in KilledOpponents)
                {
                    string s0 = $"    To: [{e.Item1,2:00}]{Cube.Id2Coords[e.Item1]}";
                    string s1 = e.Item2 != -1 ? $"[{e.Item2,2:00}]{Cube.Id2Coords[e.Item2]}" : "";
                    string s2 = e.Item3 != -1 ? $" and [{e.Item3,2:00}]{Cube.Id2Coords[e.Item3]}" : "";
                    sb.AppendLine($"{s0}; Kill: {s1}{s2}");
                }
            }
            sb.AppendLine(tail);
            return sb.ToString();
        }
    }
}
