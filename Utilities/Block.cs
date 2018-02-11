using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Utilities
{
    class Block
    {
        public static Texture2D ColorBlock(int Size, Color color, GraphicsDevice Graphics)
        {
            var texture = new Texture2D(Graphics, Size, Size);
            Color[] cData = new Color[Size*Size];
            for (var i = 0; i < Math.Pow(Size, 2); i++)
            {
                cData[i] = color;
            }
            texture.SetData(cData);
            return texture;
        }
    }
}
