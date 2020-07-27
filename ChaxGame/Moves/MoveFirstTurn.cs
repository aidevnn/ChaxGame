using System;
using System.Text;

namespace ChaxGame.Moves
{
    /// <summary>
    /// Move first turn.
    /// </summary>
    public class MoveFirstTurn : IMove, IComparable<MoveFirstTurn>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ChaxGame.Moves.ActionFirstTurn"/> class.
        /// </summary>
        /// <param name="player">Player.</param>
        /// <param name="idCell">Identifier cell.</param>
        public MoveFirstTurn(Content player, int idCell)
        {
            Player = player;
            IdCell = idCell;
            ActionType = ActionType.Remove;
        }

        public int IdCell { get; set; }

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

        public int CompareTo(MoveFirstTurn other)
        {
            return other.Weight.CompareTo(Weight) > 0 ? 1 : -1;
        }

        /// <summary>
        /// Do move on the specified cube.
        /// </summary>
        /// <param name="cube">Cube.</param>
        public void Do(Cube cube)
        {
            cube.SetCell(IdCell, Content.Empty);
        }

        /// <summary>
        /// Undo move on the specified cube.
        /// </summary>
        /// <param name="cube">Cube.</param>
        public void Undo(Cube cube)
        {
            cube.SetCell(IdCell, Player);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var ca = Cube.Id2Coords[IdCell];
            var title = $"Player:{Player} MoveFirstTurn; Weight: {Weight}";
            sb.AppendLine(title);
            var tail = $"From: [{IdCell,2:00}]{ca}";
            sb.AppendLine(tail);

            return sb.ToString();
        }
    }
}
