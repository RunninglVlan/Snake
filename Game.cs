using System;
using System.Diagnostics;

namespace Snake;

class Game {
    readonly Grid grid;
    readonly Player player;
    Stopwatch time = null!;
    long interval;
    long lastDraw;
    bool running;
    ConsoleKey direction;
    int controlsHeight;

    public Game(int width, int height) {
        grid = new Grid(width, height);
        grid.Add(player = new Player(0, grid.Height / 2, 'O'));
        interval = 1_000;
        lastDraw = -interval;
    }

    public void Play() {
        DrawControls();
        time = Stopwatch.StartNew();
        running = true;
        player.Move(direction = ConsoleKey.RightArrow);
        while (running) {
            ReadInput();
            if (time.ElapsedMilliseconds - lastDraw <= interval) {
                continue;
            }

            player.Move(direction);
            if (PlayerOutOfGrid()) {
                DrawGameOver();
                break;
            }
            DrawGame();
        }
        time.Stop();

        bool PlayerOutOfGrid() => player.X < 0 || player.X > grid.Width || player.Y < 0 || player.Y > grid.Height;
    }

    void DrawControls() {
        int x = grid.Width + 2, y = 0;
        Console.SetCursorPosition(x, y);
        Console.Write("################");
        Console.SetCursorPosition(x, ++y);
        Console.Write("    Controls   #");
        Console.SetCursorPosition(x, ++y);
        Console.Write(" S      - Exit #");
        Console.SetCursorPosition(x, ++y);
        Console.Write(" Arrows - Move #");
        controlsHeight = y;
    }

    void ReadInput() {
        if (!Console.KeyAvailable) {
            return;
        }
        var keyInfo = Console.ReadKey(true);
        switch (keyInfo.Key) {
            case ConsoleKey.S:
                running = false;
                break;
            case ConsoleKey.LeftArrow or ConsoleKey.RightArrow or ConsoleKey.UpArrow or ConsoleKey.DownArrow:
                direction = keyInfo.Key;
                break;
        }
    }

    void DrawGame() {
        Console.SetCursorPosition(0, 0);
        grid.Draw();
        DrawScore();
        lastDraw = time.ElapsedMilliseconds;
        Console.SetCursorPosition(0, grid.Height + 2);

        void DrawScore() {
            int x = grid.Width + 2, y = controlsHeight + 1;
            Console.SetCursorPosition(x, y);
            Console.Write("################");
            Console.SetCursorPosition(x, ++y);
            Console.Write("     Score     #");
            Console.SetCursorPosition(x, ++y);
            Console.Write($"   Length: {player.Length.ToString(),-2}  #");
            Console.SetCursorPosition(x, ++y);
            Console.Write("################");
        }
    }

    void DrawGameOver() {
        const string border = "#############";
        int x = grid.Width / 2 - border.Length / 2, y = grid.Height / 2 - 1;
        Console.SetCursorPosition(x, y);
        Console.Write(border);
        Console.SetCursorPosition(x, ++y);
        Console.Write("# Game over #");
        Console.SetCursorPosition(x, ++y);
        Console.Write(border);
        Console.SetCursorPosition(0, grid.Height + 2);
    }
}
