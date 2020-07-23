using System;
namespace ChaxGame.Moves
{
    /// <summary>
    /// Interface Move.
    /// </summary>
    public interface IMove
    {
        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>The player.</value>
        Content Player { get; set; }

        /// <summary>
        /// Gets or sets the type of the action.
        /// </summary>
        /// <value>The type of the action.</value>
        ActionType ActionType { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>The weight.</value>
        int Weight { get; set; }

        /// <summary>
        /// Do move on the specified cube.
        /// </summary>
        /// <param name="cube">Cube.</param>
        void Do(Cube cube);

        /// <summary>
        /// Undo move on the specified cube.
        /// </summary>
        /// <param name="cube">Cube.</param>
        void Undo(Cube cube);
    }
}
