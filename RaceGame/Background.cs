using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace RaceGame
{
    internal class Background : TextureDraw
    {
        private float[,] coordinates =
        {
            {-1, 1, 1, -1},
            {-1, -1, 1, 1}
        };

        public Background()
        {

        }

        public void DrawBackground(int textureId)
        {
            base.Bind(textureId);

            base.Draw(
                new float[,] { { 0f, 1f, 1f, 0f }, { 1f, 1f, 0f, 0f } },
                coordinates
            );
        }
    }
}