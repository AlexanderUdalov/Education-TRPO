using System;
using System.Collections.Generic;
using System.IO;

namespace ConectedCell
{
    public class Solution
    {
        public static void Main(string[] args)
        {
            var counter = new CellCounter(
                Convert.ToInt32(Console.ReadLine()),
                Convert.ToInt32(Console.ReadLine()));
            
            var Rand = new Random();
            for (int i = 0; i < counter.Matrix.Length; i++)
            {
                for (int j = 0; j < counter.Matrix[0].Length; j++)
                {
                    var value = Rand.Next(2);
                    counter.Matrix[i][j] = value;
                    //Console.Write(value + " ");
                }
                //Console.WriteLine();
                //counter.Matrix[i] = Array.ConvertAll(Console.ReadLine().Split(' '), matrixTemp => Convert.ToInt32(matrixTemp));
            }

            Console.WriteLine(counter.ConnectedCellCountNotRecursive());
            Console.ReadLine();
        }
        

    }
}
