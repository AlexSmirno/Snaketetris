using Snaketetris.Command;
using Snaketetris.GameLogic;
using Snaketetris.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Snaketetris.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private const int maxDelay = 200;
        private const int tileSize = 25;
        private const int Rows = 20;
        private const int Columns = 10;

        public ObservableCollection<PartOfSnakeBody> Snakes { get; set; }
        public ObservableCollection<Apple> Apples { get; set; }

        private GameState currentGame;
        public GameState CurrentGame
        {
            get { return currentGame; }
            set
            {
                currentGame = value;
                OnPropertyChanged(nameof(CurrentGame));
            }
        }

        private string score = "Счет: ";
        public string Score
        {
            get { return score; }
            set
            {
                score = "Счет: " + value;
                OnPropertyChanged(nameof(Score));
            }
        }

        private ICommand startCommand;
        public ICommand StartCommand
        {
            get
            {
                if (startCommand == null)
                { startCommand = new RelayCommand<object>(StartCommand_Execute); }
                return startCommand;
            }
        }

        private async void StartCommand_Execute(object parametr)
        {
            if (CurrentGame != null)
            {
                CurrentGame.EndGame();
                int[,] sad = CurrentGame.GetGrid();
            }
            ClearField();
            GameState gameState = new GameState();
            CurrentGame = gameState;
            await Task.Delay(maxDelay);
            await GameLoop(gameState);
        }

        private ICommand pauseCommand;
        public ICommand PauseCommand
        {
            get
            {
                if (pauseCommand == null)
                { pauseCommand = new RelayCommand<object>(PauseCommand_Execute); }
                return pauseCommand;
            }
        }

        private async void PauseCommand_Execute(object parametr)
        {
            CurrentGame.PauseGame();
        }

        private ICommand keyCommand;
        public ICommand KeyCommand
        {
            get
            {
                if (keyCommand == null)
                { keyCommand = new RelayCommand<object>(KeyCommand_Execute); }
                return keyCommand;
            }
        }

        private async void KeyCommand_Execute(object parametr)
        {
            if (CurrentGame.StatusGame != Status.Game)
            {
                return;
            }

            switch ((Key)int.Parse(parametr.ToString()))
            {
                case Key.Left:
                    CurrentGame.MoveSnakeLeft();
                    break;
                case Key.Right:
                    CurrentGame.MoveSnakeRight();
                    break;
                case Key.Down:
                    CurrentGame.MoveSnakeDown();
                    break;
                case Key.Up:
                    CurrentGame.MoveSnakeUp();
                    break;
                default:
                    return;
            }
        }

        public GameViewModel()
        {
            Snakes = new ObservableCollection<PartOfSnakeBody>();
            Apples = new ObservableCollection<Apple>();
        }

        private async Task GameLoop(GameState CurrentGame)
        {
            Draw(CurrentGame);

            while (CurrentGame.StatusGame != Status.Finished && CurrentGame.StatusGame != Status.Lose)
            {
                if (CurrentGame.StatusGame == Status.Game)
                {
                    CurrentGame.Move();
                    CurrentGame.Lower();
                    Score = CurrentGame.Score.ToString();
                    ClearField();
                    Draw(CurrentGame);

                }
                await Task.Delay(maxDelay);
            }

            if (CurrentGame.StatusGame == Status.Lose)
            {
                MessageBox.Show("Вы лох!");
            }
            CurrentGame.EndGame();
            Draw(CurrentGame);
        }

        private void DrawSquare(int x, int y, string color)
        {
            Snakes.Add(new PartOfSnakeBody()
            {
                Y = y * tileSize,
                X = x * tileSize,
                Height = tileSize,
                Width = tileSize,
                Color = color
            });

            /*Rectangle rectangle = new Rectangle();
            BrushConverter converter = new BrushConverter();
            rectangle.Fill = converter.ConvertFromString(color) as Brush;
            rectangle.Stroke = Brushes.White;
            rectangle.Width = tileSize;
            rectangle.Height = tileSize;

            GameCanvas.Children.Add(rectangle);
            Canvas.SetTop(rectangle, y * tileSize);
            Canvas.SetLeft(rectangle, x * tileSize);*/
        }

        private void DrawApple(Position pos)
        {
            Apples.Add(new Apple()
            {
                Y = pos.Row * tileSize,
                X = pos.Column * tileSize,
                Height = tileSize - 5,
                Width = tileSize - 5,
                Color = "RoyalBlue"
            });

            /*Ellipse ellipse = new Ellipse();
            ellipse.Name = "apple";
            BrushConverter converter = new BrushConverter();
            ellipse.Fill = Brushes.RoyalBlue;

            ellipse.Width = tileSize - 5;
            ellipse.Height = tileSize - 5;

            GameCanvas.Children.Add(ellipse);
            Canvas.SetTop(ellipse, pos.Row * tileSize);
            Canvas.SetLeft(ellipse, pos.Column * tileSize);*/
        }

        private void DrawSnakes(int[,] grid)
        {
            int i, j;
            try
            {
                for (i = 0; i < Rows; i++)
                {
                    for (j = 0; j < Columns; j++)
                    {
                        if (grid[i, j] > 1)
                        {
                            DrawSquare(j, i, CurrentGame.snakesColores[grid[i, j]]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void Draw(GameState gameState)
        {
            DrawApple(gameState.CreateApple());
            DrawSnakes(gameState.GetGrid());
        }
        
        private void ClearField()
        {
            Snakes.Clear();
            Apples.Clear();
        }

    }
}
