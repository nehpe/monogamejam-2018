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
    class Player : Wall
    {
        float SPEED = 2;

        public Player(Rectangle SourceRec, Texture2D Image, Vector2 Position) : base(SourceRec, Image, Position)
        { }

        public List<Projectile> Update(PlayerController pc, List<Sprite> sprites, GameTime gameTime)
        {
            #region Player Movement
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
            #endregion

            #region Shooting stuffs

            var Projectiles = new List<Projectile>();

            if (pc.ShootDirection != Vector2.Zero)
            {
                Projectiles.Add(new Projectile(Position)
                {
                    Directionality = pc.ShootDirection
                });
            }
            #endregion

            base.Update(gameTime);

            return Projectiles;
        }
    }
}
