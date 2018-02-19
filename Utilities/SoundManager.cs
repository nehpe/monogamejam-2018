using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Utilities
{
    public static class SoundManager
    {
        static Dictionary<string, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();

        public static void LoadSounds(Dictionary<string, SoundEffect> Sounds)
        {
            SoundManager.Sounds = Sounds;
        }

        public static void PlaySound(String Name)
        {
            SoundManager.Sounds[Name].CreateInstance().Play();
        }
    }
}
