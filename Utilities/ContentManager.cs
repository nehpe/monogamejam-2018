using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Utilities
{
    static class ContentManager
    {
        public static Dictionary<String, Texture2D> Images = new Dictionary<string, Texture2D>();

        public static void AddImage(String name, Texture2D texture)
        {
            Images.Add(name, texture);
        }

        public static Texture2D GetImage(String name)
        {
            System.Diagnostics.Debug.Assert(condition: Images.ContainsKey(name), message: string.Format("Image not found {0}", name));
            return Images[name];
        }
    }
}
