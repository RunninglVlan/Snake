using System;

namespace Snake;

abstract record Data(int X, int Y) {
    public int X { get; protected set; } = X;
    public int Y { get; protected set; } = Y;
}

record Player(int X, int Y) : Data(X, Y) {
    ConsoleKey direction;

    public ConsoleKey Direction {
        set {
            switch (direction) {
                case ConsoleKey.LeftArrow when value == ConsoleKey.RightArrow:
                case ConsoleKey.RightArrow when value == ConsoleKey.LeftArrow:
                case ConsoleKey.UpArrow when value == ConsoleKey.DownArrow:
                case ConsoleKey.DownArrow when value == ConsoleKey.UpArrow:
                    return;
                default:
                    direction = value;
                    break;
            }
        }
    }

    public int Length { get; set; } = 1;

    public bool At(int x, int y) => X == x && Y == y;

    public void Move() {
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

record Berry(int X, int Y) : Data(X, Y);
