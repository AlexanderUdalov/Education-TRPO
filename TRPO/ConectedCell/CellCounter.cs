using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConectedCell
{
    public class CellCounter
    {
        public int[][] Matrix;
        public HashSet<(int, int)> Marks;

        public CellCounter(int n, int m)
        {
            Matrix = new int[n][];
            for (int i = 0; i < n; i++)
            {
                Matrix[i] = new int[m];
            }
        }

        private class Tree
        {
            public HashSet<KeyValuePair<int, int>> LastLineElement = new HashSet<KeyValuePair<int, int>>();
            public int Count;

            public Tree(int count)
            {
                Count = count;
            }
        }

        public int ConnectedCellCountNotRecursive()
        {
            var treeArray = new List<Tree>();
            
            for (int i = 0; i < Matrix.Length; i++)
            {
                for (int j = 0; j < Matrix[0].Length; j++)
                {
                    if (Matrix[i][j] == 0) continue;
                    
                    

                    //Подсчет количества деревьев, в которые входит эл-т
                    var treeIndexArray = new List<int>();
                    for (int k = 0; k < treeArray.Count; k++)
                    {
                        if (treeArray[k].LastLineElement.Contains(new KeyValuePair<int, int>(i, j)))
                            treeIndexArray.Add(k);
                    }

                    //вариант 1
                    if (treeIndexArray.Count == 1)
                    {
                        treeArray[treeIndexArray[0]].Count++;
                        HashSetUpdate(treeArray[treeIndexArray[0]].LastLineElement, i, j);
                    }
                    //вариант 2
                    else if (treeIndexArray.Count == 2)
                    {
                        treeArray[treeIndexArray[0]].Count += treeArray[treeIndexArray[1]].Count + 1;
                        treeArray[treeIndexArray[0]].LastLineElement.UnionWith(treeArray[treeIndexArray[1]].LastLineElement);

                        treeArray.RemoveAt(treeIndexArray[1]);

                        HashSetUpdate(treeArray[treeIndexArray[0]].LastLineElement, i, j);
                    }
                    //вариант 3
                    else if (treeIndexArray.Count == 0)
                    {
                        var newTree = new Tree(1);
                        HashSetUpdate(newTree.LastLineElement, i, j);

                        treeArray.Add(newTree);
                    }
                }
            }
            if (treeArray.Count != 0)
                return treeArray.Max(x => x.Count);
            else return 0;
        }

        private void HashSetUpdate(HashSet<KeyValuePair<int, int>> set, int i, int j)
        {
            if (i == Matrix.Length - 1)
                return;

            if (j == 0)
            {
                if (!set.Contains(new KeyValuePair<int, int>(i + 1, j)))
                    set.Add(new KeyValuePair<int, int>(i + 1, j));

                if (!set.Contains(new KeyValuePair<int, int>(i + 1, j + 1)))
                    set.Add(new KeyValuePair<int, int>(i + 1, j + 1));
            }
            else if (j == Matrix[0].Length - 1)
            {
                if (!set.Contains(new KeyValuePair<int, int>(i + 1, j)))
                    set.Add(new KeyValuePair<int, int>(i + 1, j));

                if (!set.Contains(new KeyValuePair<int, int>(i + 1, j - 1)))
                    set.Add(new KeyValuePair<int, int>(i + 1, j - 1));
            }
            else
            {
                if (!set.Contains(new KeyValuePair<int, int>(i + 1, j - 1)))
                    set.Add(new KeyValuePair<int, int>(i + 1, j - 1));

                if (!set.Contains(new KeyValuePair<int, int>(i + 1, j)))
                    set.Add(new KeyValuePair<int, int>(i + 1, j));

                if (!set.Contains(new KeyValuePair<int, int>(i + 1, j + 1)))
                    set.Add(new KeyValuePair<int, int>(i + 1, j + 1));
            }


        }

        public int ConnectedCellCount()
        {
            //Показывает, просмотрена ли ячейка
            Marks = new HashSet<(int, int)>(Matrix.Length * Matrix[0].Length);

            int maxCount = 0;
            int currentCount = 0;

            for (int i = 0; i < Matrix.Length; i++)
            {
                for (int j = 0; j < Matrix[0].Length; j++)
                {
                    currentCount = CheckCell(i, j);

                    if (currentCount >= maxCount)
                        maxCount = currentCount;
                }
            }
            return maxCount;
        }

        public int CheckCell(int i, int j)
        {
            if (i < 0 || j < 0 || i > Matrix.Length - 1 || j > Matrix[0].Length - 1)
                return 0;
            
            if (!Marks.Contains((i, j)))
            {
                if (Matrix[i][j] == 0)
                {
                    Marks.Add((i, j));
                    return 0;
                }

                Marks.Add((i, j));
                var currentCount = 1;

                currentCount += CheckCell(i - 1, j - 1);
                currentCount += CheckCell(i - 1, j);
                currentCount += CheckCell(i - 1, j + 1);
                currentCount += CheckCell(i, j + 1);
                currentCount += CheckCell(i, j + 1);
                currentCount += CheckCell(i + 1, j - 1);
                currentCount += CheckCell(i + 1, j);
                currentCount += CheckCell(i + 1, j + 1);

                return currentCount;
            }
            else return 0;
        }
    }
}
