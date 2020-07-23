using System;
namespace ChaxGame
{
    /// <summary>
    /// Grid console.
    /// A singleton to display the Cube to console.
    /// </summary>
    public class GridConsole
    {
        /// <summary>
        /// The Grid to display.
        /// </summary>
        readonly char[,] DGrid;

        /// <summary>
        /// Characters to display for empty cell, player one cell and player two cell.
        /// </summary>
        public const char EMPTY = '#', P1 = 'X', P2 = 'O';

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ChaxGame.GridConsole"/> class.
        /// </summary>
        private GridConsole()
        {
            DGrid = new char[13, 13];
            for (int k = 0; k < 6; k += 2)
            {
                for (int i = k; i < 13 - k; ++i)
                {
                    DGrid[i, k] = DGrid[i, 12 - k] = '-';
                    DGrid[k, i] = DGrid[12 - k, i] = '|';
                }

                DGrid[k, k] = DGrid[6, k] = DGrid[12 - k, k] = EMPTY;
                DGrid[k, 12 - k] = DGrid[6, 12 - k] = DGrid[12 - k, 12 - k] = EMPTY;
                DGrid[k, 6] = DGrid[12 - k, 6] = EMPTY;
            }

            for (int k = 1; k < 12; k += 2)
            {
                if (k > 3 && k < 9) continue;
                DGrid[k, k] = '\\';
                DGrid[k, 6] = '-';
                DGrid[6, k] = '|';
                DGrid[12 - k, k] = '/';
            }
        }

        /// <summary>
        /// Fill the specified xy and content.
        /// </summary>
        /// <param name="xy">Xy.</param>
        /// <param name="content">Content.</param>
        void Fill((int, int) xy, Content content)
        {
            (int i, int j) = xy;
            switch (content)
            {
                case Content.Empty: DGrid[i, j] = EMPTY; break;
                case Content.P1: DGrid[i, j] = P1; break;
                case Content.P2: DGrid[i, j] = P2; break;
                default: DGrid[i, j] = EMPTY; break;
            }
        }

        /// <summary>
        /// Fill the specified xy and content.
        /// </summary>
        /// <param name="xy">Xy.</param>
        /// <param name="content">Content.</param>
        void Fill((int, int) xy, char content)
        {
            (int i, int j) = xy;
            DGrid[i, j] = content;
        }

        /// <summary>
        /// Display the specified cube and detail.
        /// </summary>
        /// <param name="cube">Cube.</param>
        /// <param name="detail">If set to <c>true</c> detail.</param>
        void Display(Cube cube, bool detail = true)
        {
            for (int k = 0; k < 24; ++k)
            {
                var cell = cube.GetCell(k);
                Fill(cell.XY, cell.Content);
            }

            Display();
        }

        /// <summary>
        /// Display the specified detail.
        /// </summary>
        /// <param name="detail">If set to <c>true</c> detail.</param>
        void Display(bool detail = true)
        {
            Console.Clear();
            int s1 = 0, s2 = 0;
            for (int j = 0; j < 13; ++j)
            {
                string s = "";
                for (int i = 0; i < 13; ++i)
                {
                    var c = DGrid[i, j];
                    s += c == char.MinValue ? ' ' : c;
                    if (c == P1) ++s1;
                    if (c == P2) ++s2;
                }

                if (detail)
                {
                    if (j == 3) s += "        00 10 20          D  B";
                    if (j == 4) s += "                           \\ |";
                    if (j == 5) s += "        01    21            \\|";
                    if (j == 6) s += "                          L-- --R";
                    if (j == 7) s += "        02 12 22             |\\";
                    if (j == 8) s += "                             | \\";
                    if (j == 9) s += "                             F  U";
                }

                Console.WriteLine(s);
            }

            Console.WriteLine();
            if (detail)
            {
                Console.WriteLine($"{EMPTY} : EmptyCell");
                Console.WriteLine($"{P1} : PlayerOne");
                Console.WriteLine($"{P2} : PlayerTwo");
                Console.WriteLine();
                Console.WriteLine($"Remain : {P1}={s1,2} - {P2}={s2,2}");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        static GridConsole Instance { get; } = new GridConsole();

        /// <summary>
        /// Display the grid to console.
        /// </summary>
        /// <param name="cube">Cube.</param>
        /// <param name="detail">If set to <c>true</c> detail.</param>
        public static void DisplayCube(Cube cube, bool detail = true) => Instance.Display(cube, detail);

        /// <summary>
        /// Fills the cell.
        /// </summary>
        /// <param name="xy">2D coordinates.</param>
        /// <param name="c">Character.</param>
        public static void FillCell((int, int) xy, char c) => Instance.Fill(xy, c);

        /// <summary>
        /// Fills the cell.
        /// </summary>
        /// <param name="xy">2D coordinate.</param>
        /// <param name="c">Content.</param>
        public static void FillCell((int, int) xy, Content c) => Instance.Fill(xy, c);

        /// <summary>
        /// Display the grid to console.
        /// </summary>
        /// <param name="detail">If set to <c>true</c> detail.</param>
        public static void DisplayConsole(bool detail = true) => Instance.Display(detail);

    }
}
