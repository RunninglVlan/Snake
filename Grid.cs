using System;
using System.Collections.Generic;

namespace Snake;

class Grid {
    const char BORDER = '#';
    public int Width { get; }
    public int Height { get; }
    readonly List<Data> data = new();

    public Grid(int width, int height) {
        Width = width;
        Height = height;
        AddBorders();
    }

    void AddBorders() {
        for (var y = 0; y < Height + 2; y++) {
            for (var x = 0; x < Width + 2; x++) {
                Console.Write(Border(x, y) ? BORDER : ' ');
            }

            Console.WriteLine();
        }

        bool Border(int x, int y) => x == 0 || x == Width + 1 || y == 0 || y == Height + 1;
    }

    public void Add(Data element) {
        data.Add(element);
    }

    public void Draw() {
        for (var y = 0; y < Height; y++) {
            Console.SetCursorPosition(1, 1 + y);
            Console.Write(string.Empty.PadRight(Width));
        }

        foreach (var (x, y, image) in data) {
            Console.SetCursorPosition(1 + x, 1 + y);
            Console.Write(image);
        }
    }
}
