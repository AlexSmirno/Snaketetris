using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snaketetris.GameLogic
{
    public class FindIslands
    {
        private int Rows, Columns, Id, prevVertex;
        private int[,] Grid;
        private Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
        private bool[] visited;
        private bool result = false;
        private Position startPos = new Position(0, 5);

        public FindIslands(int rows, int columns, int[,] grid, int id)
        {
            Rows = rows;
            Columns = columns;
            Grid = grid;
            Id = id;
            visited = new bool[Rows * Columns];
        }

        private bool IsInside(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }

        private bool IsPlaced(int r, int c)
        {
            return Grid[r, c] <= 1 || Grid[r, c] == Id;
        }

        public bool IsIsland(Position pos)
        {

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (IsPlaced(i, j))
                    {
                        graph[i * Columns + j] = new List<int>();
                    }
                }
            }

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (IsPlaced(i, j))
                    {
                        if (IsInside(i, j + 1) && IsPlaced(i, j + 1))
                        {
                            graph[i * Columns + j].Add(i * Columns + j + 1);
                        }

                        if (IsInside(i, j - 1) && IsPlaced(i, j - 1))
                        {
                            graph[i * Columns + j].Add(i * Columns + j - 1);
                        }

                        if (IsInside(i + 1, j) && IsPlaced(i + 1, j))
                        {
                            graph[i * Columns + j].Add((i + 1) * Columns + j);
                        }

                        if (IsInside(i - 1, j) && IsPlaced(i - 1, j))
                        {
                            graph[i * Columns + j].Add((i - 1) * Columns + j);
                        }
                    }
                }
            }

            Search(pos.Row * Columns + pos.Column);

            return result;
        }


        private void Search(int vertex)
        {
            if (vertex == startPos.Row * Columns + startPos.Column || result == true)
            {
                result = true;
                return;
            }

            visited[vertex] = true;

            for (int i = 0; i < graph[vertex].Count; i++)
            {
                if (!visited[graph[vertex][i]])
                {
                    prevVertex = vertex;
                    Search(graph[vertex][i]);
                }
            }
        }
    }
}
