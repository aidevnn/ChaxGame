using System;
using System.Collections.Generic;
using System.Linq;

namespace ChaxGame.Moves
{
    /// <summary>
    /// Move battle.
    /// </summary>
    public class MoveBattle : IMove
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ChaxGame.Moves.MoveBattle"/> class.
        /// </summary>
        /// <param name="player">Player.</param>
        /// <param name="idCellBefore">Identifier cell before move.</param>
        public MoveBattle(Content player, int idCellBefore)
        {
            Player = player;
            IdCellBefore = IdCellAfter = idCellBefore;
            ActionType = ActionType.Battle;
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
        /// The killed opponents cells identifiers.
        /// First int is for player cell id, others int are for opponent cell id.
        /// It will also be helpfull for displaying frames during a game.
        /// </summary>
        public List<(int, int, int, int)> KilledOpponents = new List<(int, int, int, int)>(12);

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
        public MoveBattle Clone()
        {
            var m = new MoveBattle(Player, IdCellBefore);
            m.IdCellAfter = IdCellAfter;
            m.Weight = Weight;
            m.KilledOpponents.AddRange(KilledOpponents);

            return m;
        }

        /// <summary>
        /// Do move on the specified cube.
        /// </summary>
        /// <param name="cube">Cube.</param>
        public void Do(Cube cube)
        {
            cube.SetCell(IdCellBefore, Content.Empty);
            cube.SetCell(IdCellAfter, Player);

            foreach (var id in KilledOpponents)
            {
                if (id.Item2 != -1)
                    cube.SetCell(id.Item2, Content.Empty);
                if (id.Item3 != -1)
                    cube.SetCell(id.Item3, Content.Empty);
                if (id.Item4 != -1)
                    cube.SetCell(id.Item4, Content.Empty);
            }
        }

        /// <summary>
        /// Undo move on the specified cube.
        /// </summary>
        /// <param name="cube">Cube.</param>
        public void Undo(Cube cube)
        {
            var opponent = Player.GetOpponent();
            cube.SetCell(IdCellBefore, Player);
            cube.SetCell(IdCellAfter, Content.Empty);

            foreach (var id in KilledOpponents)
            {
                if (id.Item2 != -1)
                    cube.SetCell(id.Item2, opponent);
                if (id.Item3 != -1)
                    cube.SetCell(id.Item3, opponent);
                if (id.Item4 != -1)
                    cube.SetCell(id.Item4, opponent);
            }
        }
    }
}
