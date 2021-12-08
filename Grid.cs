﻿using System;

namespace Snake;

class Grid {
    const char BORDER = '#', PLAYER = 'P';
    public int Width { get; }
    public int Height { get; }
    readonly char[,] grid;

    public Grid(int width, int height) {
        Width = width;
        Height = height;
        grid = new char[width, height];
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

    public void Draw(Player player, Berry berry) {
        for (var y = 0; y < Height; y++) {
            Console.SetCursorPosition(1, 1 + y);
            for (var x = 0; x < Width; x++) {
                Console.Write(player.At(x, y) ? PLAYER : grid[x, y]);
            }
        }
    }
}