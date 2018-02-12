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
        public override Rectangle Rectangle { get => new Rectangle((int) Position.X,
                     (int) Position.Y,
                     (int) SourceRec.Width,
                     (int) SourceRec.Height); }

        protected Rectangle SourceRec;
        public Wall(Rectangle SourceRec, Texture2D SpriteImage, Vector2 Position) : base(SpriteImage, Position)
        {
            this.SourceRec = SourceRec;
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
