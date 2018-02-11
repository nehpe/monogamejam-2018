using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Scenes
{
    abstract class BaseScene
    {
        protected MGJamGame Game;
        protected GraphicsDevice Graphics;

        public BaseScene(MGJamGame Game, GraphicsDevice Graphics)
        {
            this.Game = Game;
            this.Graphics = Graphics;
        }
    }
}
