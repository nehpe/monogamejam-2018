using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam
{
    public static class GameState
    {
        public static int PlayerHealth = 3;
        public static int MaxPlayerHealth = 3;
        public static bool isDead { get => (GameState.PlayerHealth <= 0); }
        public static bool isFinished = false;
    }
}
