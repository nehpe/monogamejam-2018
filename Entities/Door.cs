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
    class Door : Item
    {
        private bool IsOpen = false;

        public virtual Rectangle Rectangle { 
            get => new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)SourceRec.Width,
                (int)SourceRec.Height+16
            ); 
        }
        public Door(Rectangle SourceRec, Texture2D SpriteImage, Vector2 Position) : base(SourceRec, SpriteImage, Position)
        {
        }

        public bool Update(Player player, GameTime gameTime)
        {
            // Check if player is colliding with me, if so, MOVE ON
            if (IsOpen)
                if (this.Rectangle.Intersects(player.Rectangle))
                {
                    return true;
                }
            base.Update(gameTime);
            return false;
        }

        internal void Open()
        {
            if (!IsOpen)
            {
                SoundManager.PlaySound("win");
                SourceRec.X += 32;
                IsOpen = true;
            }
        }
    }
}
