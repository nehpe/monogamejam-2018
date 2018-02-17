using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Utilities
{
    public class Animation
    {
        public int tileSize = 16;
        public float speed = 10;
        public int currentFrame = 0;
        public int totalFrames = 0;
        public Rectangle StartRect;
        private float _timer = 0f;
        public Rectangle Rectangle { get =>
                new Rectangle(
                    StartRect.X + (currentFrame * tileSize), StartRect.Y,
                    tileSize, tileSize
                );
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds / 16f;
            if (_timer > speed)
            {
                currentFrame++;
                _timer = 0f;
                if (currentFrame+1 == totalFrames)
                {
                    currentFrame = 0;
                }
            }
        }
    }

    public enum AnimationDirection
    {
        NORTH,
        SOUTH,
        EAST,
        WEST,
        NORTHEAST,
        NORTHWEST,
        SOUTHEAST,
        SOUTHWEST
    }
}
