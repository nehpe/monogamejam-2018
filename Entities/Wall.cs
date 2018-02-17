using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Entities
{
    class Wall : Sprite
    {
        protected Rectangle SourceRec;
        public override Rectangle Rectangle
        {
            get => 
                new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)SourceRec.Width,
                    (int)SourceRec.Height
                );
        }

        public Wall(Rectangle SourceRec, Texture2D SpriteImage, Vector2 Position) : base(SpriteImage, Position)
        {
            this.SourceRec = SourceRec;
        }

        public void Update(GameTime gameTime, List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (this.IsTouchingBottom(sprite) ||
                    this.IsTouchingLeft(sprite) ||
                    this.IsTouchingRight(sprite) ||
                    this.IsTouchingTop(sprite))
                {
                    sprite.IsDead = true;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            batch.Draw(SpriteImage,
                new Rectangle((int)Position.X, (int)Position.Y, SourceRec.Width, SourceRec.Height),
                SourceRec,
                Color.White
            );
        }
    }
}
