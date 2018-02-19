using Microsoft.Xna.Framework.Audio;
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
        public static Dictionary<String, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();

        public static void AddImage(String name, Texture2D texture)
        {
            if (!Images.ContainsKey(name))
                Images.Add(name, texture);
        }

        public static Texture2D GetImage(String name)
        {
            System.Diagnostics.Debug.Assert(condition: Images.ContainsKey(name), message: string.Format("Image not found {0}", name));
            return Images[name];
        }

        public static void AddSound(String name, SoundEffect sound)
        {
            if (!Sounds.ContainsKey(name))
            Sounds.Add(name, sound);
        }

        public static SoundEffect GetSound(String name)
        {
            System.Diagnostics.Debug.Assert(condition: Sounds.ContainsKey(name), message: string.Format("Sound not found {0}", name));
            return Sounds[name];
        }

        public static Dictionary<String, SoundEffect> GetAllSounds()
        {
            return Sounds;
        }
    }
}
