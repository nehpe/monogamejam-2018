using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Entities
{
    class Item : Sprite
    {
        public Item(Rectangle SourceRec, Texture2D SpriteImage, Vector2 Position) : base(SourceRec, SpriteImage, Position)
        { }
    }
}
