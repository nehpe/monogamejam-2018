using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam.Utilities;

namespace MonoGameJam.Entities
{
    public class EnemyProjectile : Sprite
    {
        public Vector2 Directionality = Vector2.Zero;
        public float SPEED = 3.5f;
        public int Damage = 1;
        public EnemyProjectile(Vector2 Position, Vector2 Directionality) : this(ContentManager.GetImage("EnemyProjectile"), Position, Directionality)
        {}

        public EnemyProjectile(Texture2D SpriteImage, Vector2 Position, Vector2 Directionality) : base(SpriteImage, Position)
        {
            this.Position = new Vector2(Position.X - SpriteImage.Width / 2, Position.Y - SpriteImage.Height / 2);
            this.Directionality = Directionality;
            this.Position += Directionality * SPEED;
        }

        public override void Update(GameTime gameTime)
        {
            Velocity.X = (float)(Directionality.X * SPEED * gameTime.ElapsedGameTime.TotalMilliseconds / 16f);
            Velocity.Y = (float)(Directionality.Y * SPEED * gameTime.ElapsedGameTime.TotalMilliseconds / 16f);
            base.Update(gameTime);
        }

        public void Kill()
        {
            this.IsDead = true;
        }
    }
}