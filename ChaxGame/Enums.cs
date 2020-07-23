using System;
namespace ChaxGame
{
    /// <summary>
    /// Directions of moves
    /// U for Up, D for Down, L for Left, R for Right, F for Forward, B for Backward
    /// </summary>
    public enum DIR { U, D, L, R, F, B }

    /// <summary>
    /// Content of a cell
    /// Empty, or P1 for player one, or P2 for player two
    /// </summary>
    public enum Content { Empty, P1, P2 }

    /// <summary>
    /// Phase of game
    /// Place for action placement phase, Firsturn for player one first action remove, Battle for action battle phase
    /// </summary>
    public enum Phase { Place, FirstTurn, Battle }

    /// <summary>
    /// Check of console agent input
    /// Good for good inputs, BadDir for bad direction inputs, BadBonus for bad bonus inputs
    /// </summary>
    public enum Check { Good, BadDir, BadBonus }

    /// <summary>
    /// Action type.
    /// Place for action placement, Remove for action first tun, Battle for action battle and Pass for no action
    /// </summary>
    public enum ActionType { Place, Remove, Battle, Pass }
}
