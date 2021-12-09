﻿using System;
using System.Diagnostics;

namespace Snake;

abstract record Data(int X, int Y, char Image) {
    public int X { get; protected set; } = X;
    public int Y { get; protected set; } = Y;
    public char Image { get; } = Image;

    public bool PositionEquals(Data other) => X == other.X && Y == other.Y;
}

record Player(int X, int Y, char Image) : Data(X, Y, Image) {
    const float VERTICAL_MULTIPLIER = 1.5f;

    ConsoleKey direction;
    Stopwatch time = null!;

    public int Length { get; set; } = 1;

    int Interval {
        get {
            var interval = Length < 10 ? 1_000 - Length * 100 + 100 : 100;
            return direction is ConsoleKey.UpArrow or ConsoleKey.DownArrow
                ? (int)(interval * VERTICAL_MULTIPLIER)
                : interval;
        }
    }

    public bool TimeToMove() => time.ElapsedMilliseconds >= Interval;

    public void Move(ConsoleKey newDirection) {
        SetDirection();
        SetPosition();
        time = Stopwatch.StartNew();

        void SetDirection() {
            switch (direction) {
                case ConsoleKey.LeftArrow when newDirection == ConsoleKey.RightArrow:
                case ConsoleKey.RightArrow when newDirection == ConsoleKey.LeftArrow:
                case ConsoleKey.UpArrow when newDirection == ConsoleKey.DownArrow:
                case ConsoleKey.DownArrow when newDirection == ConsoleKey.UpArrow:
                    return;
                default:
                    direction = newDirection;
                    break;
            }
        }

        void SetPosition() {
            switch (direction) {
                case ConsoleKey.LeftArrow:
                    X--;
                    break;
                case ConsoleKey.RightArrow:
                    X++;
                    break;
                case ConsoleKey.UpArrow:
                    Y--;
                    break;
                case ConsoleKey.DownArrow:
                    Y++;
                    break;
            }
        }
    }
}

record Berry(int gridWidth, int gridHeight, char Image) : Data(0, 0, Image) {
    readonly int gridWidth = gridWidth;
    readonly int gridHeight = gridHeight;

    readonly Random random = new();
    Stopwatch time = null!;
    long life;

    public void Move() {
        X = random.Next(0, gridWidth);
        Y = random.Next(0, gridHeight);
        time = Stopwatch.StartNew();
        life = random.Next(3_000, 7_000);
    }

    public bool Alive() => time.ElapsedMilliseconds < life;
}
