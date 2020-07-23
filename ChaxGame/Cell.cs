using System;
using System.Collections.Generic;
using System.Linq;

namespace ChaxGame
{
    /// <summary>
    /// Cell.
    /// Each cell has an unique Id, 3D coordinates, Power, and Content.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the coords.
        /// </summary>
        /// <value>The coords.</value>
        public (int, int, int) Coords { get; private set; }

        /// <summary>
        /// Gets the 2D coordinate for displaying
        /// </summary>
        /// <value>The xy.</value>
        public (int, int) XY { get; private set; }

        /// <summary>
        /// Gets the power of a cell
        /// </summary>
        /// <value>The power.</value>
        public int Power { get; private set; }

        /// <summary>
        /// Gets or sets the content of a cell.
        /// </summary>
        /// <value>The content.</value>
        public Content Content { get; set; } = Content.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ChaxGame.Cell"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        public Cell(int id, int x, int y, int z)
        {
            Id = id;
            Coords = (x, y, z);

            int s = 2 * z;
            int d = 6 - 2 * z;
            XY = (s + d * x, s + d * y);

            Power = Coords.Power();
        }

        /// <summary>
        /// The all neighbors of a cell.
        /// A key value pair, the direction of the neighbor and the neighbor cell
        /// </summary>
        public Dictionary<DIR, Cell> Neighbors = new Dictionary<DIR, Cell>();

        /// <summary>
        /// The rows.
        /// A row was composed of two next neighbors of this cell in a same direction
        /// </summary>
        public List<Cell[]> Rows = new List<Cell[]>();
    }
}
