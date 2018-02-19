using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Scenes
{
    public abstract class BaseScene
    {
        protected MGJamGame Game;
        protected GraphicsDevice Graphics;

        public BaseScene(MGJamGame Game, GraphicsDevice Graphics)
        {
            this.Game = Game;
            this.Graphics = Graphics;
        }

        public virtual void Initialize() { }
        public virtual void LoadContent() { }

        public virtual void Update(GameTime gameTime) { }
        public abstract RenderTarget2D Draw(GameTime gameTime);
    }
}
