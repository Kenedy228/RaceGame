using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;

namespace RaceGame
{
    class Program
    {
        static void Main()
        {
            GameWindowSettings gSettings = new GameWindowSettings()
            {
                UpdateFrequency = 60.0,
            };
            NativeWindowSettings nSettings = new NativeWindowSettings()
            {
                Title = "Гонки",
                Size = (800, 600),
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Compatability,
            };

            Game game = new Game(gSettings, nSettings);
            game.Run();
        }
    }
}