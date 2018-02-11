using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Entities
{
    class Player : Sprite
    {
        float SPEED = 5;

        public Player(Texture2D Image, Vector2 Position) : base(Image, Position)
        { }

        public void Update(PlayerController pc, List<Sprite> sprites, GameTime gameTime)
        {
            this.Velocity = Vector2.Zero;
            if (pc.MovementDirection != Vector2.Zero)
            {
                Velocity.X += (int)(pc.MovementDirection.X * SPEED * gameTime.ElapsedGameTime.TotalMilliseconds / 16f);
                Velocity.Y += (int)(pc.MovementDirection.Y * SPEED * gameTime.ElapsedGameTime.TotalMilliseconds / 16f);
            }

            if (this.Velocity != Vector2.Zero)
            {
                foreach (var sprite in sprites)
                {
                    if (sprite == this)
                        continue;

                    if (this.IsTouchingBottom(sprite))
                    {
                        this.Velocity.Y = 0;
                    }
                    if (this.IsTouchingTop(sprite))
                    {
                        this.Velocity.Y = 0;
                    }

                    if (this.IsTouchingRight(sprite))
                    {
                        this.Velocity.X = 0;
                    }
                    if (this.IsTouchingLeft(sprite))
                    {
                        this.Velocity.X = 0;
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
