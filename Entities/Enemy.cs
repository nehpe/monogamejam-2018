using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam.Utilities;

namespace MonoGameJam.Entities
{
    class Enemy : Wall
    {
        public int Health = 5;
        public EnemyState enemyState = EnemyState.AGGRO;
        public Rectangle? IntersectionCollision;
        public Sprite target = null;
        const float SPEED = 2f;
        float initTimer = 100f;
        float firingTimer = 100f;

        public Enemy(Rectangle SourceRec, Texture2D SpriteImage, Vector2 Position) : base(SourceRec, SpriteImage, Position)
        {
        }


        public List<EnemyProjectile> Update(GameTime gameTime, List<Sprite> sprites, Player player)
        {
            if (this.target == null)
                this.target = player;

            this.Velocity = Vector2.Zero;

            followTarget(gameTime, sprites);
            List<EnemyProjectile> enemyProjectiles = fireAtTarget(gameTime);

            var Projectiles = sprites.Where(x => x is Projectile).ToList();

            checkIfHit(Projectiles);

            base.Update(gameTime);

            return enemyProjectiles;
        }

        private List<EnemyProjectile> fireAtTarget(GameTime gameTime)
        {
            List<EnemyProjectile> proj = new List<EnemyProjectile>();

            firingTimer -= (float)(1.0f * gameTime.ElapsedGameTime.TotalMilliseconds / 16f);

            if (firingTimer <= 0f)
            {
                var rect = this.target.Rectangle;
                Vector2 firingDirection = new Vector2(rect.X, rect.Y) - Position;
                firingDirection.Normalize();
                firingTimer = initTimer;
                proj.Add(new EnemyProjectile(
                    Position,
                    firingDirection
                    ));
            }

            return proj;
        }

        private void followTarget(GameTime gameTime, List<Sprite> sprites)
        {
            var rect = this.target.Rectangle;
            var direction = new Vector2(rect.X, rect.Y) - this.Position;
            direction.Normalize();
            Velocity.X += (float)(direction.X * SPEED * gameTime.ElapsedGameTime.TotalMilliseconds / 16f);
            Velocity.Y += (float)(direction.Y * SPEED * gameTime.ElapsedGameTime.TotalMilliseconds / 16f);

            if (this.Velocity != Vector2.Zero)
            {
                foreach (var sprite in sprites)
                {
                    if (sprite == this || sprite is Projectile)
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
        }

        private void checkIfHit(List<Sprite> projectiles)
        {
            foreach (Projectile p in projectiles)
            {
                if (this.IsTouchingBottom(p))
                {
                    this.Velocity = p.Directionality * p.SPEED;
                    this.takeDamage(p);
                    p.Kill();
                }
                else if (this.IsTouchingTop(p))
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
        }

        private void takeDamage(Projectile p)
        {
            SoundManager.PlaySound("hit");

            this.Health -= p.Damage;
            if (this.Health < 0)
            {
                this.IsDead = true;
            }
        }
    }

    public enum EnemyState
    {
        IDLE,
        AGGRO
    }
}
