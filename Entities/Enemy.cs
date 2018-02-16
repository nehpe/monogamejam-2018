using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJam.Entities
{
    class Enemy : Wall
    {
        public int Health = 5;

        public Enemy(Rectangle SourceRec, Texture2D SpriteImage, Vector2 Position) : base(SourceRec, SpriteImage, Position)
        {
        }

        public void Update(GameTime gameTime, List<Sprite> Projectiles)
        {
            this.Velocity = Vector2.Zero;

            foreach(Projectile p in Projectiles)
            {
                if (this.IsTouchingBottom(p))
                {
                    this.Velocity = p.Directionality * p.SPEED;
                    this.takeDamage(p);
                    p.Kill();
                } else if (this.IsTouchingTop(p))
                {
                    this.Velocity = p.Directionality * p.SPEED;
                    this.takeDamage(p);
                    p.Kill();
                }
                else if (this.IsTouchingRight(p))
                {
                    this.Velocity = p.Directionality * p.SPEED;
                    this.takeDamage(p);
                    p.Kill();
                }
                else if (this.IsTouchingLeft(p))
                {
                    this.Velocity = p.Directionality * p.SPEED;
                    this.takeDamage(p);
                    p.Kill();
                }
            }
            base.Update(gameTime);
        }

        private void takeDamage(Projectile p)
        {
            this.Health -= p.Damage;
            if (this.Health < 0)
            {
                this.IsDead = true;
            }
        }
    }
}
