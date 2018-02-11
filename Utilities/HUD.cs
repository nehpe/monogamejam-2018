using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Utilities
{
    class HUD
    {
        protected SpriteBatch Batch;
        protected SpriteFont Font;

        public HUD(GraphicsDevice Graphics, SpriteFont Font)
        {
            Batch = new SpriteBatch(Graphics);
            this.Font = Font;
        }

        public void Draw(GameTime gameTime)
        {
            Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);
            {
                Batch.DrawString(Font, string.Format("Health: {0}/{1}", GameState.PlayerHealth.ToString(), GameState.MaxPlayerHealth.ToString()), new Vector2(0, 0), Color.White);
            }
            Batch.End();
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
