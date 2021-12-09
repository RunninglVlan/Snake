using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Snake;

record Data(int X, int Y, char Image) {
    public int X { get; set; } = X;
    public int Y { get; set; } = Y;
    public char Image { get; } = Image;

    public bool PositionEquals(Data other) => X == other.X && Y == other.Y;
}

record Player(int X, int Y, char Image) : Data(X, Y, Image) {
    const float VERTICAL_MULTIPLIER = 1.5f;

    ConsoleKey direction;
    Stopwatch time = null!;
    readonly List<Data> tail = new();

    public int Length => tail.Count + 1;

    int Interval {
        get {
            var interval = Math.Max(800 - Length * 100, 100);
            return direction is ConsoleKey.UpArrow or ConsoleKey.DownArrow
                ? (int)(interval * VERTICAL_MULTIPLIER)
                : interval;
        }
    }

    public bool TimeToMove() => time.ElapsedMilliseconds >= Interval;

    public bool Move(ConsoleKey newDirection) {
        SetDirection();
        time = Stopwatch.StartNew();
        return SetPosition();

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

        bool SetPosition() {
            for (var index = tail.Count - 1; index >= 0; index--) {
                var part = tail[index];
                var nextPart = index - 1 >= 0 ? tail[index - 1] : this;
                part.X = nextPart.X;
                part.Y = nextPart.Y;
            }

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

            return tail.All(part => X != part.X || Y != part.Y);
        }
    }

    public Data IncreaseTail() {
        var last = tail.Count > 0 ? tail.Last() : this;
        var part = new Data(last.X, last.Y, 'o');
        tail.Add(part);
        return part;
    }

    public void Clear(Grid grid) {
        foreach (var part in tail) {
            grid.Remove(part);
        }

        tail.Clear();
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
