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
        /// </summary>
        public List<Cell> Neighbors = new List<Cell>(3);

        /// <summary>
        /// The rows.
        /// A row was composed of two next neighbors of this cell in a same direction
        /// </summary>
        public List<Cell[]> Rows = new List<Cell[]>();

        /// <summary>
        /// Checks the row for killing opponent.
        /// </summary>
        /// <returns><c>true</c>, if row was checked, <c>false</c> otherwise.</returns>
        /// <param name="row">Row.</param>
        public int CheckRow(Cell[] row)
        {
            if (row.Length != 2)
                throw new Exception("Not a row");

            var c0 = row[0];
            var c1 = row[1];

            if (Content.DiffPlayer(c0.Content) && Content.SamePlayer(c1.Content))
                return c0.Id;

            return -1;
        }
    }
}
