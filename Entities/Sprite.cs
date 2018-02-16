using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Entities
{
    public class Sprite
    {
        protected Texture2D SpriteImage;

        public bool IsDead = false;

        protected Vector2 Position;

        protected Vector2 Velocity = Vector2.Zero;

        protected Rectangle SourceRec;

        public virtual Rectangle Rectangle { get => new Rectangle((int)Position.X,
                     (int)Position.Y,
                     (int)SourceRec.Width,
                     (int)SourceRec.Height); }

        public Sprite(Rectangle SourceRec, Texture2D SpriteImage, Vector2 Position)
        {
            this.SpriteImage = SpriteImage;
            this.SourceRec = SourceRec;
            this.Position = Position;
        }

        public Sprite(Texture2D SpriteImage, Vector2 Position)
        {
            this.SpriteImage = SpriteImage;
            this.SourceRec = new Rectangle(0, 0, SpriteImage.Width, SpriteImage.Height);
            this.Position = Position;
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch batch)
        {
            batch.Draw(SpriteImage,
                            new Rectangle((int)Position.X, (int)Position.Y, SourceRec.Width, SourceRec.Height),
                            SourceRec,
                            Color.White
                            );
        }

        public virtual void Update(GameTime gameTime)
        {
            this.Position += this.Velocity;
        }

        #region Collision
        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
                this.Rectangle.Left < sprite.Rectangle.Left &&
                this.Rectangle.Bottom > sprite.Rectangle.Top &&
                this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
                this.Rectangle.Right > sprite.Rectangle.Right &&
                this.Rectangle.Bottom > sprite.Rectangle.Top &&
                this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
                this.Rectangle.Top < sprite.Rectangle.Top &&
                this.Rectangle.Right > sprite.Rectangle.Left &&
                this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
                this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
                this.Rectangle.Right > sprite.Rectangle.Left &&
                this.Rectangle.Left < sprite.Rectangle.Right;
        }
        #endregion
    }
}
