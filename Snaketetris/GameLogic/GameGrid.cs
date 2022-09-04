using System.Collections.Generic;

namespace Snaketetris.GameLogic
{
    public class GameGrid
    {
        private int[,] grid;
        public int Rows { get; }
        public int Columns { get; }

        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];
        }

        private bool IsInside(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }

        public bool IsEmpty(int r, int c)
        {
            return IsInside(r, c) && grid[r, c] <= 1;
        }

        public bool IsEmpty(Position pos)
        {
            return IsInside(pos.Row, pos.Column) && grid[pos.Row, pos.Column] <= 1;
        }

        public bool IsPlaceForApple(Position pos, int id)
        {
            FindIslands find = new FindIslands(Rows, Columns, grid, id);
            return IsEmpty(pos) && (pos.Row > 2 || pos.Row < 2 && pos.Column < 3 && pos.Column > 6) && find.IsIsland(pos);
        }

        public bool IsApple(Position pos)
        {
            return grid[pos.Row, pos.Column] == 1;
        }

        public bool IsRowFull(int r, int id)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] <= 1 || grid[r, c] == id)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsRowEmpty(int r, int id)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (!IsEmpty(r, c) && grid[r, c] != id)
                {
                    return false;
                }
            }

            return true;
        }

        public void ClearGrid()
        {
            grid = new int[Rows, Columns];
        }

        public void DeleteSnake(Position[] snakeBody)
        {
            for (int i = 0; i < snakeBody.Length; i++)
            {
                grid[snakeBody[i].Row, snakeBody[i].Column] = 0;
            }
        }

        public void CreateSnake(Position[] snakeBody, int id)
        {
            for (int i = 0; i < snakeBody.Length; i++)
            {
                if (snakeBody[i].Row < 0)
                {
                    continue;
                }
                else
                {
                    grid[snakeBody[i].Row, snakeBody[i].Column] = id;
                }
            }
        }

        public void AddApple(Position pos)
        {
            grid[pos.Row, pos.Column] = 1;
        }
        public void DeleteApple(Position pos)
        {
            grid[pos.Row, pos.Column] = 0;
        }

        public bool IsThisSnake(Position pos, int id)
        {
            return IsInside(pos.Row, pos.Column) && grid[pos.Row, pos.Column] == id;
        }

        public int[,] getGrid()
        {
            return grid;
        }

        private void DownOneRow(int endRow, int id)
        {
            for (int i = endRow - 1; i >= 0; i--)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (i == 0)
                    {
                        grid[i, c] = 0;
                    }

                    if (grid[i, c] != id && grid[i + 1, c] != id)
                    {
                        grid[i + 1, c] = grid[i, c];
                    }
                }
            }
        }

        public void Lower(int startRow, int endRow, int id)
        {
            while (!IsRowEmpty(startRow, id))
            {
                DownOneRow(endRow, id);
            }
        }

        public void RemoveFulColumns(int id)
        {
            for (int i = 0; i < Rows; i++)
            {
                if (IsRowFull(i, id))
                {
                    DownOneRow(i, id);
                }
            }
        }
    }
}
