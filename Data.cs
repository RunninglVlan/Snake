using System;

namespace Snake;

abstract record Data(int X, int Y, char Image) {
    public int X { get; protected set; } = X;
    public int Y { get; protected set; } = Y;
    public char Image { get; } = Image;
}

record Player(int X, int Y, char Image) : Data(X, Y, Image) {
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

record Berry(int X, int Y, char Image) : Data(X, Y, Image);
