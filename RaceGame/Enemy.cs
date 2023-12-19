using System;

namespace RaceGame
{
    internal class Enemy : TextureDraw
    {
        public float[] xCoordinates = new float[] { 0f, 0.15f, 0.15f, 0f };
        public float[] yCoordinates = new float[] { 1f, 1f, 1.35f, 1.35f };

        public Random rnd = new Random();

        public bool canMove = true;

        private float[,] texCoordinates = new float[,]
        {
            {0f, 1f, 1f, 0f},
            {0f, 0f, 1f, 1f}
        };

        private float speed = 0.006f;

        public int delay;

        private bool end = false;

        public Enemy(int delay)
        {
            this.delay = delay;
            GenerateCar();
        }

        public void DrawEnenemy(int textureId)
        {
            base.Bind(textureId);

            base.Draw(
                texCoordinates,
                new float[,]
                {
                    {xCoordinates[0], xCoordinates[1], xCoordinates[2], xCoordinates[3] },
                    {yCoordinates[0], yCoordinates[1], yCoordinates[2], yCoordinates[3] }
                }
            );

            if (canMove)
            {
                Move();
            }
        }
        
        public void Move()
        {
            for (int i = 0; i < yCoordinates.Length; i++)
            {
                yCoordinates[i] -= speed;
            }

            if (yCoordinates[2] < -1f)
            {
                end = true;
            }
        }

        public void GenerateCar()
        {
            int pos = rnd.Next(-5, 6);
            float position = (-1 * pos) % 10 * -0.1f;

            xCoordinates = new float[] { (position - 0.075f) * -1, (position + 0.075f) * -1, (position + 0.075f) * -1, (position - 0.075f) * -1 };

        }

        public void CheckEnd(Score score)
        {
            if (end)
            {
                GenerateCar();
                yCoordinates = new float[] { 1f, 1f, 1.35f, 1.35f };
                end = false;
                score.IncrementCounter();
            }
        }
    }
}