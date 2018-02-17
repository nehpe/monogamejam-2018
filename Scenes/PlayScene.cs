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
        protected Level Level;
        protected Vector2 TileOffset = new Vector2(32, 32);
        protected Vector2 TileXY = new Vector2(18, 9);

        protected Player Player;
        private HUD HUD;
        protected List<Sprite> sprites;

        private Camera2D _camera;

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
            _camera = new Camera2D(Graphics.Viewport);
        }

        public void LoadContent()
        {
            Center = new Vector2(GameVars.GAME_WIDTH / 2, GameVars.GAME_HEIGHT / 2);

            HUD = new HUD(Graphics, Game.Content.Load<SpriteFont>("fonts/HUD"));

            ContentManager.AddImage("Projectile", Game.Content.Load<Texture2D>("projectiles/fireball"));
            ContentManager.AddImage("Tileset", Game.Content.Load<Texture2D>("tiles/dungeon_sheet"));
            ContentManager.AddImage("Knight", Game.Content.Load<Texture2D>("tiles/knight_proper"));
            ContentManager.AddImage("Portal", Game.Content.Load<Texture2D>("tiles/portal"));

            Level = new Level(Graphics);

            sprites = new List<Sprite>();

            Player = new Player(ContentManager.GetImage("Knight"), Center + new Vector2(0, 32));
            sprites.AddRange(Level.Initialize());
            sprites.Add(Player);

            sprites.Add(new Portal(ContentManager.GetImage("Portal"), new Vector2(256, 128)));
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            InputHelper.Update();

            var pc = InputHelper.GetPlayerController();

            var Projectiles = Player.Update(pc, sprites, gameTime);
            
            _camera.LookAt(new Vector2(Player.Rectangle.X, Player.Rectangle.Y));

            foreach (var sprite in sprites)
            {
                if (sprite == Player)
                    continue;

                if (sprite is Enemy)
                {
                    var enemy = (Enemy)sprite;
                    enemy.Update(gameTime, sprites.Where(x => x is Projectile).ToList());
                }
                else if (sprite is Wall)
                {
                    var wall = (Wall)sprite;
                    wall.Update(gameTime, sprites.Where(x => x is Projectile).ToList());
                }
                else
                {
                    sprite.Update(gameTime);
                }
            }

            if (pc.Mouse.LeftClicked)
            {
                var portals = sprites.Where(x => x is Portal).ToList();
                foreach (Portal portal in portals)
                {
                    if (portal.Rectangle.Contains(pc.Mouse.Position))
                    {
                        Player.TeleportTo(new Vector2(portal.Rectangle.X, portal.Rectangle.Y));
                        portal.IsDead = true;
                        break;
                    }
                }
            }

            var deadSprites = sprites.Where(x => x.IsDead).ToList();
            foreach (var s in deadSprites)
            {
                if (s is Enemy)
                {
                    sprites.Add(new Portal(ContentManager.GetImage("Portal"), new Vector2(s.Rectangle.X, s.Rectangle.Y)));
                }
                sprites.Remove(s);
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

            Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.Default, RasterizerState.CullNone, transformMatrix: _camera.GetViewMatrix());
            {
                #region Floor Rendering
                Level.Draw(gameTime, Batch);

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
