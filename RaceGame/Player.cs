using System;

namespace RaceGame
{
    internal class Player : TextureDraw
    {
        public float[] xCoordinates = new float[] { -0.25f, -0.10f, -0.10f, -0.25f};
        public float[] yCoordinates = new float[] { -0.99f, -0.99f, -0.64f, -0.64f};

        private float[,] texCoordinates =
        {
            {0f, 1f, 1f, 0f},
            {1f, 1f, 0f, 0f}
        };

        private float rightBoard = 0.65f;
        private float leftBoard = -0.65f;

        private float speed = 0.01f;
        public bool gameFinish = false;

        public Player()
        {

        }

        public void DrawPlayer(int textureId)
        {
            base.Bind(textureId);

            base.Draw(
                texCoordinates,
                new float[,]
                {
                    {xCoordinates[0], xCoordinates[1], xCoordinates[2], xCoordinates[3] },
                    {yCoordinates[0], yCoordinates[1], yCoordinates[2], yCoordinates[3] },
                }
            );
        }

        public void MoveLeft()
        {
            if (xCoordinates[0] < leftBoard)
            {
                xCoordinates[0] = leftBoard;
                xCoordinates[1] = leftBoard + 0.2f;
                xCoordinates[2] = leftBoard + 0.2f;
                xCoordinates[3] = leftBoard;
            }
            for (int i = 0; i < xCoordinates.Length; i++)
            {
                xCoordinates[i] -= speed;
            }
        }

        public void MoveRight()
        {
            if (xCoordinates[1] > rightBoard)
            {
                xCoordinates[0] = rightBoard - 0.2f;
                xCoordinates[1] = rightBoard;
                xCoordinates[2] = rightBoard;
                xCoordinates[3] = rightBoard - 0.2f;
            }
            for (int i = 0; i < xCoordinates.Length; i++)
            {
                xCoordinates[i] += speed;
            }
        }

        public void CheckFinish(List<Enemy> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (yCoordinates[2] > enemies[i].yCoordinates[0] && yCoordinates[0] < enemies[i].yCoordinates[2])
                {
                    if ((xCoordinates[0] > enemies[i].xCoordinates[1] && xCoordinates[0] < enemies[i].xCoordinates[0]) 
                        || (xCoordinates[1] < enemies[i].xCoordinates[0] && xCoordinates[1] > enemies[i].xCoordinates[1]))
                    {
                        gameFinish = true;
                    }
                }
            }
        }
    }
}