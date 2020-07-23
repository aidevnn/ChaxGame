using System;
using System.Collections.Generic;

namespace ChaxGame
{
    public static class Extensions
    {
        /// <summary>
        /// Code the specified direction
        /// </summary>
        /// <returns>The code, from 0 to 5</returns>
        /// <param name="d">Direction</param>
        public static int Code(this DIR d) => (int)d;

        public static Content Revert(this Content c)
        {
            if (c == Content.Empty) return Content.Empty;
            if (c == Content.P1) return Content.P2;
            return Content.P1;
        }

        /// <summary>
        /// Next coordinates the specified coordinate according to direction.
        /// </summary>
        /// <returns>The next coordinates.</returns>
        /// <param name="c">Coordinate.</param>
        /// <param name="d">Direction.</param>
        public static (int, int, int) Next(this (int, int, int) c, DIR d)
        {
            switch (d)
            {
                case DIR.R: return (c.Item1 + 1, c.Item2, c.Item3);
                case DIR.L: return (c.Item1 - 1, c.Item2, c.Item3);
                case DIR.F: return (c.Item1, c.Item2 + 1, c.Item3);
                case DIR.B: return (c.Item1, c.Item2 - 1, c.Item3);
                case DIR.U: return (c.Item1, c.Item2, c.Item3 + 1);
                case DIR.D: return (c.Item1, c.Item2, c.Item3 - 1);
                default: return (c.Item1, c.Item2, c.Item3);
            }
        }

        /// <summary>
        /// Verifying In Cube coordinate
        /// </summary>
        /// <returns><c>true</c>, if coordinate was in cube, <c>false</c> otherwise.</returns>
        /// <param name="c">Coordinate.</param>
        public static bool InCube(this (int, int, int) c)
        {
            (int x, int y, int z) = c;
            if (x == 1 && y == 1) return false;
            if (x < 0 || y < 0 || z < 0 || x > 2 || y > 2 || z > 2) return false;
            return true;
        }

        static Dictionary<(int, int, int), int> DScore;

        /// <summary>
        /// Power of the specified coordinate.
        /// </summary>
        /// <returns>The power from 0 to 3.</returns>
        /// <param name="c">Coordinate.</param>
        public static int Power(this (int, int, int) c)
        {
            if (DScore == null)
            {
                DScore = new Dictionary<(int, int, int), int>();
                DScore[(0, 0, 0)] = DScore[(0, 2, 0)] = DScore[(2, 0, 0)] = DScore[(2, 2, 0)] = 3;
                DScore[(0, 0, 2)] = DScore[(0, 2, 2)] = DScore[(2, 0, 2)] = DScore[(2, 2, 2)] = 3;

                DScore[(0, 0, 1)] = DScore[(0, 2, 1)] = DScore[(2, 0, 1)] = DScore[(2, 2, 1)] = 2;

                DScore[(1, 0, 0)] = DScore[(0, 1, 0)] = DScore[(2, 1, 0)] = DScore[(1, 2, 0)] = 1;
                DScore[(1, 0, 2)] = DScore[(0, 1, 2)] = DScore[(2, 1, 2)] = DScore[(1, 2, 2)] = 1;

                DScore[(1, 0, 1)] = DScore[(0, 1, 1)] = DScore[(1, 2, 1)] = DScore[(2, 1, 1)] = 0;
            }

            return DScore[c];
        }

        /// <summary>
        /// Gets the opponent of the specific player
        /// </summary>
        /// <returns>The opponent.</returns>
        /// <param name="player">Player.</param>
        public static Content GetOpponent(this Content player)
        {
            switch (player)
            {
                case Content.P1: return Content.P2;
                case Content.P2: return Content.P1;
                default: return Content.Empty;
            }
        }

        /// <summary>
        /// Check if a cell content same as the player.
        /// </summary>
        /// <returns><c>true</c>, if player was samed, <c>false</c> otherwise.</returns>
        /// <param name="c0">Content.</param>
        /// <param name="c1">Other Content.</param>
        public static bool SamePlayer(this Content c0, Content c1)
        {
            if (c0 == Content.Empty || c1 == Content.Empty) return false;
            return c0 == c1;
        }

        /// <summary>
        /// Check if a cell content opponent of the player.
        /// </summary>
        /// <returns><c>true</c>, if player was different, <c>false</c> otherwise.</returns>
        /// <param name="c0">Content.</param>
        /// <param name="c1">Other Content.</param>
        public static bool DiffPlayer(this Content c0, Content c1)
        {
            if (c0 == Content.Empty || c1 == Content.Empty) return false;
            return c0 != c1;
        }
    }
}
