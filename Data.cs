using System;
using System.Diagnostics;

namespace Snake;

abstract record Data(int X, int Y, char Image) {
    public int X { get; protected set; } = X;
    public int Y { get; protected set; } = Y;
    public char Image { get; } = Image;
}

record Player(int X, int Y, char Image) : Data(X, Y, Image) {
    ConsoleKey direction;
    Stopwatch time = null!;

    public int Length { get; set; } = 1;
    int Interval => 1_000 - Length * 100 + 100;

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

record Berry(int X, int Y, char Image) : Data(X, Y, Image);
