using System;
namespace ChaxGame.Moves
{
    /// <summary>
    /// Move placement.
    /// </summary>
    public class MovePlacement : IMove, IComparable<MovePlacement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ChaxGame.Moves.MovePlacement"/> class.
        /// </summary>
        /// <param name="player">Player.</param>
        /// <param name="idCell">Identifier cell.</param>
        public MovePlacement(Content player, int idCell)
        {
            Player = player;
            IdCell = idCell;
            ActionType = ActionType.Place;
        }

        /// <summary>
        /// Gets or sets the identifier cell to place player token.
        /// </summary>
        /// <value>The identifier cell.</value>
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

        public int CompareTo(MovePlacement other)
        {
            return Weight.CompareTo(other.Weight) > 0 ? 1 : -1;
        }

        /// <summary>
        /// Do move on the specified cube.
        /// </summary>
        /// <param name="cube">Cube.</param>
        public void Do(Cube cube)
        {
            cube.SetCell(IdCell, Player);
        }

        /// <summary>
        /// Undo move on the specified cube.
        /// </summary>
        /// <param name="cube">Cube.</param>
        public void Undo(Cube cube)
        {
            cube.SetCell(IdCell, Content.Empty);
        }
    }
}
