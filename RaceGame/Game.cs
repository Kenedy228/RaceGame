using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;


namespace RaceGame
{
    class Game : GameWindow
    {
        private int backgroundId, playerId, scoreId, startButtonId,
            exitButtonId, restartButtonId,
            menuButtonId;

        Background background = new Background();
        Score score = new Score();
        Player player = new Player();
        Menu menu = new Menu();

        int delay = 0;

        List<int> enenemiesColors = new List<int>();

        List<Enemy> enemies = new List<Enemy>
        {
            new Enemy(
                0
                ),
            new Enemy(
                80
                ),
            new Enemy(
                160
                ),
        };

        Vector2 cursorPosition = new Vector2();

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeSettings)
            : base(gameWindowSettings, nativeSettings)
        {
            backgroundId = GameTextures.LoadTexture("Textures/background.png");
            playerId = GameTextures.LoadTexture("Textures/car01.png");
            startButtonId = GameTextures.LoadTexture("Textures/startButton.png");
            exitButtonId = GameTextures.LoadTexture("Textures/exitButton.png");
            restartButtonId = GameTextures.LoadTexture("Textures/restartButton.png");
            menuButtonId = GameTextures.LoadTexture("Textures/menuButton.png");
            scoreId = GameTextures.LoadTexture("Textures/numbers.png");

            enenemiesColors.Add(GameTextures.LoadTexture("Textures/car02.png"));
            enenemiesColors.Add(GameTextures.LoadTexture("Textures/car03.png"));
            enenemiesColors.Add(GameTextures.LoadTexture("Textures/car04.png"));
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            if (KeyboardState.IsKeyDown(Keys.Left) && menu.gameStatus == 1)
            {
                player.MoveLeft();
            }

            if (KeyboardState.IsKeyDown(Keys.Right) && menu.gameStatus == 1)
            {
                player.MoveRight();
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButton.Button1)
            {
                menu.MouseClickHandler(cursorPosition);
            }

        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            cursorPosition.X = 2 * e.Position.X / ClientSize.X - 1.0f;
            cursorPosition.Y = -(2 * e.Position.Y / ClientSize.Y - 1.0f);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.Texture2D);

        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            background.DrawBackground(backgroundId);

            switch (menu.gameStatus)
            {
                case 0:
                    menu.DrawMenu(
                        new int[] { startButtonId, exitButtonId },
                        new string[] { "start", "exit" }
                        );

                    player.DrawPlayer(playerId);
                    break;
                case 1:
                    if (menu.restart)
                    {
                        Clear();
                        menu.restart = false;
                    }

                    player.DrawPlayer(playerId);

                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].delay < delay)
                        {
                            enemies[i].DrawEnenemy(enenemiesColors[i]);
                            enemies[i].CheckEnd(score);
                        }
                    }

                    player.CheckFinish(enemies);
                    score.DrawScore(scoreId);

                    delay++;

                    if (player.gameFinish == true)
                    {
                        menu.gameStatus = 2;
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            enemies[i].canMove = false;
                        }
                    }

                    break;
                case 2:

                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].delay < delay)
                        {
                            enemies[i].DrawEnenemy(enenemiesColors[i]);
                        }
                    }

                    player.DrawPlayer(playerId);

                    score.DrawScore(scoreId);

                    menu.DrawMenu(
                        new int[] { restartButtonId, menuButtonId },
                        new string[] { "restart", "menu" }
                    );

                    break;
                case -1:
                    Close();
                    break;
            }

            SwapBuffers();

        }

        protected void Clear()
        {
            enemies = new List<Enemy>
            {
                new Enemy(
                    0
                    ),
                new Enemy(
                    80
                    ),
                new Enemy(
                    160
                    ),
            };

            delay = 0;

            player = new Player();

            score = new Score();
        }
    }
}