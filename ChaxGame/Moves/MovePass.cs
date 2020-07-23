using System;
namespace ChaxGame.Moves
{
    /// <summary>
    /// Move pass.
    /// </summary>
    public class MovePass : IMove
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ChaxGame.Moves.MovePass"/> class.
        /// </summary>
        /// <param name="player">Player.</param>
        public MovePass(Content player)
        {
            Player = player;
        }

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
        /// Do move on the specified cube. Nothing to do.
        /// </summary>
        /// <param name="cube">Cube.</param>
        public void Do(Cube cube) { }

        /// <summary>
        /// Undo move on the specified cube. Nothing to do.
        /// </summary>
        /// <param name="cube">Cube.</param>
        public void Undo(Cube cube) { }
    }
}
