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
        /// <param name="idCell">Identifier cell before.</param>
        public MoveBattle(Content player, int idCell)
        {
            Player = player;
            Opponent = Player.GetOpponent();
            ActionType = ActionType.Battle;
            IdBefore = IdAfter = idCell;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ChaxGame.Moves.MoveBattle"/> class.
        /// </summary>
        /// <param name="previous">Previous.</param>
        /// <param name="idBefore">Identifier before.</param>
        /// <param name="idAfter">Identifier after.</param>
        public MoveBattle(MoveBattle previous, int idBefore, int idAfter)
        {
            Player = previous.Player;
            Opponent = Player.GetOpponent();
            IdBefore = idBefore;
            IdAfter = idAfter;
            ActionType = ActionType.Battle;
            AllSteps.AddRange(previous.AllSteps);
            TotalKills = previous.TotalKills;
            Steps = previous.Steps + 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ChaxGame.Moves.MoveBattle"/> class.
        /// </summary>
        /// <param name="move">Move.</param>
        public MoveBattle(MoveBattle move)
        {
            ActionType = ActionType.Battle;
            Player = move.Player;
            Opponent = Player.GetOpponent();
            Weight = move.Weight;
            TotalKills = move.TotalKills;
            Steps = move.Steps;
            AllSteps.AddRange(move.AllSteps);
        }

        public int CompareTo(MoveBattle other)
        {
            return other.Weight.CompareTo(Weight) > 0 ? 1 : -1;
        }

        /// <summary>
        /// Gets or sets the identifier of starting cell.
        /// </summary>
        /// <value>The identifier cell.</value>
        public int IdBefore { get; set; }

        /// <summary>
        /// Gets or sets the identifier of last cell.
        /// </summary>
        /// <value>The identifier after.</value>
        public int IdAfter { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>The player.</value>
        public Content Player { get; set; }

        /// <summary>
        /// Gets the opponent.
        /// </summary>
        /// <value>The opponent.</value>
        public Content Opponent { get; private set; }

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
        /// The stack of all steps.
        /// It will also be helpfull for displaying frames during a game.
        /// </summary>
        public List<SubMove> AllSteps { get; set; } = new List<SubMove>(12);

        /// <summary>
        /// Gets or sets the steps.
        /// </summary>
        /// <value>The steps.</value>
        public int Steps { get; set; }

        /// <summary>
        /// Gets or sets the total kils.
        /// </summary>
        /// <value>The total kils.</value>
        public int TotalKills { get; set; }

        public static int NbSteps = 0;

        /// <summary>
        /// Do move on the specified cube.
        /// </summary>
        /// <param name="cube">Cube.</param>
        public void Do(Cube cube)
        {
            foreach (var mv in AllSteps)
            {
                ++NbSteps;
                mv.DoStep(cube, Player);
            }
        }

        /// <summary>
        /// Undo move on the specified cube.
        /// </summary>
        /// <param name="cube">Cube.</param>
        public void Undo(Cube cube)
        {
            foreach (var mv in AllSteps.Reverse<SubMove>())
            {
                ++NbSteps;
                mv.UndoStep(cube, Player, Opponent);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var cb = Cube.Id2Coords[IdBefore];
            var ca = Cube.Id2Coords[IdAfter];
            var title = $"Player:{Player} MoveBattle; Weight: {Weight}";
            sb.AppendLine(title); 
            var head = $"From: [{IdBefore,2:00}]{cb}";
            var tail = $"To  : [{IdAfter,2:00}]{ca}";
            sb.AppendLine(head);

            foreach (var e in AllSteps)
            {
                string s0 = $"   From: [{e.idBefore,2:00}]{Cube.Id2Coords[e.idBefore]} To  : [{e.idAfter,2:00}]{Cube.Id2Coords[e.idAfter]}";
                string s1 = e.idOpp1 != -1 ? $"[{e.idOpp1,2:00}]{Cube.Id2Coords[e.idOpp1]}" : "";
                string s2 = e.idOpp2 != -1 ? $" and [{e.idOpp2,2:00}]{Cube.Id2Coords[e.idOpp2]}" : "";
                sb.AppendLine($"{s0}; Kill: {s1}{s2}");
            }

            sb.AppendLine(tail);
            return sb.ToString();
        }
    }
}
