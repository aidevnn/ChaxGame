using System;
namespace ChaxGame.Moves
{
    /// <summary>
    /// Move first turn.
    /// </summary>
    public class MoveFirstTurn : IMove
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
    }
}
