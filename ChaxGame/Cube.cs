﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ChaxGame
{
    /// <summary>
    /// Cube.
    /// </summary>
    public class Cube
    {
        /// <summary>
        /// Cube id to 3D coords.
        /// </summary>
        public static (int, int, int)[] Id2Coords = { (0, 0, 0), (0, 0, 1), (0, 0, 2), (0, 1, 0), (0, 1, 1), (0, 1, 2), (0, 2, 0), (0, 2, 1), (0, 2, 2), (1, 0, 0), (1, 0, 1), (1, 0, 2), (1, 2, 0), (1, 2, 1), (1, 2, 2), (2, 0, 0), (2, 0, 1), (2, 0, 2), (2, 1, 0), (2, 1, 1), (2, 1, 2), (2, 2, 0), (2, 2, 1), (2, 2, 2) };

        /// <summary>
        /// All cells by coordinates
        /// </summary>
        Cell[,,] Cells;

        /// <summary>
        /// All cells by identifier
        /// </summary>
        Dictionary<int, Cell> AllCells = new Dictionary<int, Cell>(24);

        /// <summary>
        /// All directions in a static array
        /// </summary>
        static DIR[] DIRS = { DIR.B, DIR.D, DIR.F, DIR.L, DIR.R, DIR.U };

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ChaxGame.Cube"/> class.
        /// </summary>
        public Cube()
        {
            Init();
        }

        /// <summary>
        /// Private CellContent for computing Cube domination.
        /// </summary>
        class CellContent
        {
            public int Id;
            public Content Content = Content.Empty, AltContent = Content.Empty;
            public int Step = 0, Power = 0;

            public List<CellContent> Neighbors = new List<CellContent>(4);

            public CellContent(int id, int power)
            {
                Id = id;
                Power = power;
            }
        }

        /// <summary>
        /// The cell contents for domination.
        /// </summary>
        Dictionary<int, CellContent> CellContents = new Dictionary<int, CellContent>(24);

        int domPlayer, domOpponent, nbPlayer, nbOpponent;

        /// <summary>
        /// Gets or sets the cube score.
        /// </summary>
        /// <value>The cube score.</value>
        public CubeScore CubeScore { get; set; }

        /// <summary>
        /// Clone this instance.
        /// </summary>
        /// <returns>The clone.</returns>
        public Cube Clone()
        {
            var cube = new Cube();
            for (int i = 0; i < 24; ++i)
                cube.SetCell(i, GetCell(i).Content);

            return cube;
        }

        /// <summary>
        /// Init this instance.
        /// </summary>
        void Init()
        {
            Cells = new Cell[3, 3, 3];
            int id = 0;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        if (i == 1 && j == 1) break;
                        var c = Cells[i, j, k] = new Cell(id++, i, j, k);
                        CellContents[c.Id] = new CellContent(c.Id, c.Power);
                        AllCells.Add(c.Id, c);
                    }
                }
            }

            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        if (i == 1 && j == 1) break;
                        var c = (i, j, k);
                        var cell0 = GetCell(c);
                        var cc = CellContents[cell0.Id];

                        foreach (var d in DIRS)
                        {
                            var c1 = c.Next(d);
                            var cell1 = GetCell(c1);
                            if (cell1 == null) continue;
                            cell0.Neighbors.Add(cell1);
                            cc.Neighbors.Add(CellContents[cell1.Id]);

                            var c2 = c1.Next(d);
                            var cell2 = GetCell(c2);
                            if (cell2 != null)
                                cell0.Rows.Add(new Cell[] { cell1, cell2 });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the cell by it's identifier.
        /// </summary>
        /// <returns>The cell.</returns>
        /// <param name="id">Identifier.</param>
        public Cell GetCell(int id)
        {
            if (!AllCells.ContainsKey(id))
                return null;

            return AllCells[id];
        }

        /// <summary>
        /// Gets the cell by it's coordinates.
        /// </summary>
        /// <returns>The cell.</returns>
        /// <param name="coord">Coordinate.</param>
        public Cell GetCell((int, int, int) coord)
        {
            if (!coord.InCube())
                return null;

            return Cells[coord.Item1, coord.Item2, coord.Item3];
        }

        /// <summary>
        /// Sets the cell content by it's identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="content">Content.</param>
        public void SetCell(int id, Content content)
        {
            if (!AllCells.ContainsKey(id))
                throw new Exception($"Cell with identifier {id} dont exists.");

            AllCells[id].Content = content;
        }

        /// <summary>
        /// Sets the cell content by it's coordinates.
        /// </summary>
        /// <param name="coord">Coordinate.</param>
        /// <param name="content">Content.</param>
        public void SetCell((int, int, int) coord, Content content)
        {
            if (!coord.InCube())
                throw new Exception($"Cell with coordinate {coord} dont exists.");

            Cells[coord.Item1, coord.Item2, coord.Item3].Content = content;
        }

        /// <summary>
        /// Queue of Cells for computing domination
        /// </summary>
        Queue<CellContent> q = new Queue<CellContent>();

        /// <summary>
        /// Computes the domination of the Cube by specific player
        /// </summary>
        /// <returns>The domination.</returns>
        /// <param name="player">Player.</param>
        public CubeScore ComputeCubeScore(Content player)
        {
            if (player == Content.Empty)
                throw new Exception("Compute Domination for player one or player two");

            q.Clear();
            var opponent = player.GetOpponent();
            domPlayer = domOpponent = nbPlayer = nbOpponent = 0;

            for (int i = 0; i < 24; ++i)
            {
                var cc = CellContents[i];
                cc.Content = GetCell(i).Content;
                cc.AltContent = Content.Empty;
                cc.Step = 0;

                if (cc.Content != Content.Empty)
                {
                    q.Enqueue(cc);
                    cc.AltContent = cc.Content;

                    if (cc.Content == player)
                    {
                        domPlayer += cc.Power;
                        ++nbPlayer;
                    }
                    if (cc.Content == opponent)
                    {
                        domOpponent += cc.Power;
                        ++nbOpponent;
                    }
                }
            }

            while (q.Count != 0)
            {
                var c = q.Dequeue();
                foreach (var n in c.Neighbors)
                {
                    if (n.Content == Content.Empty)
                    {
                        n.Content = c.Content;
                        n.Step = c.Step + 1;
                        q.Enqueue(n);

                        if (n.Content == player) domPlayer += n.Power;
                        if (n.Content == opponent) domOpponent += n.Power;
                    }
                    else if (n.Content != c.Content && n.AltContent == Content.Empty && n.Step == c.Step + 1)
                    {
                        n.AltContent = c.Content;
                        if (n.Content == opponent && c.Content == player)
                        {
                            domPlayer += n.Power;
                            domOpponent -= n.Power;
                        }
                    }
                }
            }

            CubeScore = new CubeScore(player, nbPlayer, nbOpponent, domPlayer, domOpponent);
            return CubeScore;
        }

        /// <summary>
        /// Display Cube domination to console.
        /// </summary>
        public void DisplayDomination(Content player)
        {
            foreach (var c in AllCells.Values)
                GridConsole.FillCell(c.XY, ' ');

            foreach (var c in CellContents.Values)
            {
                var xy = GetCell(c.Id).XY;
                if (c.Content == Content.P1 && c.AltContent == Content.P1) GridConsole.FillCell(xy, 'X');
                if (c.Content == Content.P1 && c.AltContent == Content.P2) GridConsole.FillCell(xy, '*');
                if (c.Content == Content.P1 && c.AltContent == Content.Empty) GridConsole.FillCell(xy, 'x');
                if (c.Content == Content.P2 && c.AltContent == Content.P2) GridConsole.FillCell(xy, 'O');
                if (c.Content == Content.P2 && c.AltContent == Content.P1) GridConsole.FillCell(xy, '*');
                if (c.Content == Content.P2 && c.AltContent == Content.Empty) GridConsole.FillCell(xy, 'o');
            }

            var opponent = player.GetOpponent();
            GridConsole.DisplayConsole(false);
            Console.WriteLine($"Player:{player} {domPlayer,2}; Opponent:{opponent} {domOpponent,2}");
        }

        /// <summary>
        /// Exports the cube as a string of 24 characters.
        /// </summary>
        /// <returns>The state.</returns>
        public string ExportState()
        {
            var s = "";
            for(int i = 0; i < 24; ++i)
            {
                var c = AllCells[i].Content;
                if (c == Content.Empty) s += '0';
                else if (c == Content.P1) s += '1';
                else s += '2';
            }

            return s;
        }

        /// <summary>
        /// Imports the cube content from a string of 24 characters.
        /// </summary>
        /// <param name="s">String.</param>
        public void ImportState(string s)
        {
            if (s.Length != 24)
                throw new Exception("State must contains 24 characters.");

            for(int i = 0; i < 24; ++i)
            {
                var c = s[i];
                if (c == '1') AllCells[i].Content = Content.P1;
                else if (c == '2') AllCells[i].Content = Content.P2;
                else AllCells[i].Content = Content.Empty;
            }
        }
    }
}
