using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam.Entities;
using MonoGameJam.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Scenes
{
    class PlayScene : BaseScene
    {
        protected SpriteBatch Batch;
        protected RenderTarget2D Target;

        //protected Texture2D floor_tile;
        protected Level Level;
        protected Vector2 TileOffset = new Vector2(32, 32);
        protected Vector2 TileXY = new Vector2(18, 9);

        protected Player Player;
        private HUD HUD;
        protected List<Sprite> sprites;

        protected Vector2 Center;

        public PlayScene(MGJamGame Game, GraphicsDevice Graphics) : base(Game, Graphics)
        {
            Target = new RenderTarget2D(Graphics, GameVars.GAME_WIDTH, GameVars.GAME_HEIGHT,
                false, Graphics.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            Batch = new SpriteBatch(Graphics);

        }


        #region Initialization

        public void Initialize()
        {
            InputHelper.Update();
        }

        public void LoadContent()
        {
            //floor_tile = Game.Content.Load<Texture2D>("tiles/floor");
            Center = new Vector2(GameVars.GAME_WIDTH / 2, GameVars.GAME_HEIGHT / 2);
            
            HUD = new HUD(Graphics, Game.Content.Load<SpriteFont>("fonts/HUD"));



            

            ContentManager.AddImage("Projectile", Game.Content.Load<Texture2D>("projectiles/projectile"));
            ContentManager.AddImage("Tileset", Game.Content.Load<Texture2D>("tiles/dungeon_sheet"));
            ContentManager.AddImage("Knight", Game.Content.Load<Texture2D>("tiles/knight_proper"));

            Level = new Level(Graphics);

            sprites = new List<Sprite>();

            //sprites.Add(new Sprite(Utilities.Block.ColorBlock(24, Color.Purple, Graphics), Center - new Vector2(32, 32)));
            Player = new Player(new Rectangle(0, 0, 16, 16), ContentManager.GetImage("Knight"), Center);
            sprites.AddRange(Level.Initialize());
            sprites.Add(Player);
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            InputHelper.Update();

            var pc = InputHelper.GetPlayerController();

            var Projectiles = Player.Update(pc, sprites, gameTime);
            foreach(var sprite in sprites)
            {
                if (sprite == Player)
                    continue;
                sprite.Update(gameTime);
            }

            if (Projectiles != null && Projectiles.Count() > 0)
            {
                foreach (var proj in Projectiles)
                {
                    sprites.Add(proj);
                }
            }
            HUD.Update(gameTime);
        }

        public RenderTarget2D Draw(GameTime gameTime)
        {
            Graphics.SetRenderTarget(Target);
            Graphics.Clear(new Color(6, 6, 6));

            Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);

            {
                #region Floor Rendering
                Level.Draw(gameTime);
                
                #endregion

                foreach (var sprite in sprites)
                {
                    sprite.Draw(gameTime, Batch);
                }

            }

            Batch.End();

            HUD.Draw(gameTime);

            Graphics.SetRenderTarget(null);
            return Target;
        }
    }
}
