using System;
using System.Collections.Generic;
using System.Linq;

namespace Snaketetris.GameLogic
{
    public class GameState
    {
        private Snake currentSnake;

        public Snake CurrentSnake
        {
            get => currentSnake;
            private set
            {
                currentSnake = value;
                currentSnake.Reset();
                ColorGenerate();
                currentSnake.Id = ++previousId;
            }
        }

        public string[] colors = { "#008000", "#0000FF", "#FF0000", "#DAA520" };
        public GameGrid GameGrid { get; }
        public Status StatusGame { get; private set; }
        public Snake DiedSnake { get; private set; }
        public int Score { get; set; }
        private const int startLength = 4;
        private const int scoreByApple = 1;
        private const double multipleLength = 0.1;
        private int snakeLength;
        public List<string> snakesColores = new List<string>() { "#000000", "#FF00FF" };

        private int previousId = 1;
        private const int borderRow = 3;
        private Position applePos;
        private Position start = new Position(0, 5);

        public GameState()
        {
            snakeLength = startLength;
            GameGrid = new GameGrid(20, 10);
            CurrentSnake = new Snake(start.Row, start.Column, snakeLength);
            Score = 0;
        }

        private bool SnakeFits()
        {
            return GameGrid.IsEmpty(CurrentSnake.GetNextStep());
        }

        public void Move()
        {
            if (!SnakeFits())
            {
                StatusGame = Status.Lose;
                return;
            }

            DeleteSnake();

            if (GameGrid.IsApple(CurrentSnake.GetNextStep()))
            {
                CurrentSnake.Move();
                FreezeSnake();
                GameGrid.DeleteApple(applePos);
                Score += scoreByApple;
                snakeLength = startLength + (int)(Score * multipleLength);
                SnakeFall();
                applePos = null;
                return;
            }

            CurrentSnake.Move();
            CreateSnake();
        }

        private void DeleteSnake()
        {
            GameGrid.DeleteSnake(CurrentSnake.Tiles.Where(c => c.Row > -1).ToArray());
        }
        private void CreateSnake()
        {
            GameGrid.CreateSnake(CurrentSnake.Tiles.Where(c => c.Row > -1).ToArray(), CurrentSnake.Id);
        }

        public void FreezeSnake()
        {
            CurrentSnake.CurrentDirection.Stop();
        }

        public void MoveSnakeLeft()
        {
            CurrentSnake.CurrentDirection.MoveLeft();
        }

        public void MoveSnakeRight()
        {
            CurrentSnake.CurrentDirection.MoveRight();
        }

        public void MoveSnakeUp()
        {
            CurrentSnake.CurrentDirection.MoveUp();
        }

        public void MoveSnakeDown()
        {
            CurrentSnake.CurrentDirection.MoveDown();
        }

        public Position CreateApple()
        {
            if (applePos != null)
            {
                return applePos;
            }
            Random rand = new Random();
            int x, y;
            do
            {
                x = rand.Next(GameGrid.Columns);
                y = rand.Next(GameGrid.Rows);
            } while (!GameGrid.IsPlaceForApple(new Position(y, x), CurrentSnake.Id));

            applePos = new Position(y, x);

            GameGrid.AddApple(applePos);

            return applePos;
        }

        public void SnakeFall()
        {
            DiedSnake = CurrentSnake;
            CurrentSnake = new Snake(start.Row, start.Column, snakeLength);

            bool isContinue = true;

            while (isContinue)
            {
                for (int i = 0; i < DiedSnake.Tiles.Length; i++)
                {
                    if (DiedSnake.Tiles[i].Row < 0)
                    {
                        continue;
                    }
                    if (!GameGrid.IsEmpty(DiedSnake.Tiles[i].Row + 1, DiedSnake.Tiles[i].Column) ||
                        GameGrid.IsThisSnake(new Position(DiedSnake.Tiles[i].Row + 1, DiedSnake.Tiles[i].Column), DiedSnake.Id))
                    {
                        isContinue = false;
                    }
                }

                for (int i = 0; i < DiedSnake.Tiles.Length; i++)
                {
                    DiedSnake.Tiles[i].Row += 1;
                    if (DiedSnake.Tiles[i].Column < 0)
                    {
                        DiedSnake.Tiles[i].Column = start.Column;
                    }
                }
            }

            for (int i = DiedSnake.Tiles.Length - 1; i >= 0; i--)
            {
                DiedSnake.Tiles[i].Row -= 1;
            }

            GameGrid.CreateSnake(DiedSnake.Tiles, DiedSnake.Id);
        }

        public int[,] GetGrid()
        {
            return GameGrid.getGrid();
        }

        public void ColorGenerate()
        {
            Random rand = new Random();
            CurrentSnake.Color = colors[rand.Next(colors.Length)];
            snakesColores.Add(colors[rand.Next(colors.Length)]);
        }

        public void EndGame()
        {
            CurrentSnake.Reset();
            GameGrid.ClearGrid();
            StatusGame = Status.Finished;
        }
        public void PauseGame()
        {
            if (StatusGame == Status.Pause)
            {
                StatusGame = Status.Game;
            }
            else if (StatusGame == Status.Game)
            {
                StatusGame = Status.Pause;
            }
        }

        public void Lower()
        {
            GameGrid.Lower(borderRow, GameGrid.Rows - 1, CurrentSnake.Id);
            GameGrid.RemoveFulColumns(CurrentSnake.Id);
        }
    }
}
