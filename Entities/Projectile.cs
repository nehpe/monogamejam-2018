using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam.Utilities;

namespace MonoGameJam.Entities
{
    public class Projectile : Sprite
    {
        public Vector2 Directionality = Vector2.Zero;
        public float SPEED = 7f;
        public Projectile(Vector2 Position) : this(ContentManager.GetImage("Projectile"), Position)
        {}

        public Projectile(Texture2D SpriteImage, Vector2 Position) : base(SpriteImage, Position)
        {}

        public override void Update(GameTime gameTime)
        {
            Velocity.X = (float)(Directionality.X * SPEED * gameTime.ElapsedGameTime.TotalMilliseconds / 16f);
            Velocity.Y = (float)(Directionality.Y * SPEED * gameTime.ElapsedGameTime.TotalMilliseconds / 16f);

            base.Update(gameTime);
        }


    }
}