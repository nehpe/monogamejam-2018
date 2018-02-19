using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Scenes
{
    class DeathScene : BaseScene
    {
        protected RenderTarget2D _target;
        protected SpriteFont _font;
        protected SpriteBatch _batch;
        protected Vector2 _center;

        protected String _play = "Press [ENTER] To Play";
        protected String _gameName = "You died. lulz.";
        protected Vector2 _playPos;
        protected Vector2 _gameNamePos;

        public DeathScene(MGJamGame Game, GraphicsDevice Graphics) : base(Game, Graphics)
        {
        }

        public override void Initialize()
        {
            InputHelper.Update();
            _target = new RenderTarget2D(Graphics, GameVars.GAME_WIDTH, GameVars.GAME_HEIGHT);
            _batch = new SpriteBatch(Graphics);
        }

        public override void LoadContent()
        {
            _font = Game.Content.Load<SpriteFont>("fonts/HUD");

            _center = new Vector2(GameVars.GAME_WIDTH / 2, GameVars.GAME_HEIGHT / 2);
            var measure = _font.MeasureString(_play);
            _playPos = new Vector2(_center.X - (measure.X / 2), (float)((_center.Y - (measure.Y / 2)) / 2 * 4));
            measure = _font.MeasureString(_gameName);
            _gameNamePos = new Vector2(_center.X - (measure.X / 2), _center.Y - (measure.Y / 2) * 3);
        }

        public override void Update(GameTime gameTime)
        {
            InputHelper.Update();

            if (InputHelper.IsKeyJustPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                GameState.PlayerHealth = GameState.MaxPlayerHealth;
                Game.SetScene(new PlayScene(Game, Graphics));
            }
        }
        public override RenderTarget2D Draw(GameTime gameTime)
        {
            Graphics.SetRenderTarget(_target);
            Graphics.Clear(new Color(6, 6, 6));

            _batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);
            {

                _batch.DrawString(_font, _gameName, _gameNamePos, Color.White);
                _batch.DrawString(_font, _play, _playPos, Color.White);
            }
            _batch.End();

            Graphics.SetRenderTarget(null);
            return _target;
        }
    }
}
