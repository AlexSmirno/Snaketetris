using System;
using System.Collections.Generic;

namespace Snaketetris.GameLogic
{
    public class Snake
    {
        public int Length { get; set; }
        public Position[] Tiles { get; private set; }
        public Position StartHead;
        public Direction CurrentDirection { get; }
        public int Id { get; set; }
        public string Color { get; set; }

        public Snake(int row, int column, int length)
        {
            StartHead = new Position(row, column);
            Length = length;
            Tiles = new Position[Length];
            Reset();
            CurrentDirection = new Direction();
            CurrentDirection.MoveDown();
        }

        public void Move()
        {
            for (int i = 0; i < Length - 1; i++)
            {
                Tiles[i].Row = Tiles[i + 1].Row;
                Tiles[i].Column = Tiles[i + 1].Column;
            }

            Tiles[Length - 1].Row += CurrentDirection.Y;
            Tiles[Length - 1].Column += CurrentDirection.X;
        }

        public Position GetNextStep()
        {
            return new Position(Tiles[Length - 1].Row + CurrentDirection.Y,
                                Tiles[Length - 1].Column + CurrentDirection.X);
        }

        public void Reset()
        {
            Tiles[Length - 1] = StartHead;
            for (int i = Length - 2; i >= 0; i--)
            {
                Tiles[i] = new Position(Tiles[i + 1].Row - 1, Tiles[Length - 1].Column);
            }
        }

    }
}
