using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using ChaxGame.Moves;

namespace ChaxGame
{
    class MainClass
    {
        public static Random Random = new Random();

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            DisplayDemo.Moves1();

            //Benchmark.BruteForce(4);
        }
    }
}
