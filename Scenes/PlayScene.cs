using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
        Door door;
        private HUD HUD;
        protected List<Sprite> sprites;

        protected Texture2D sq;

        public PlayScene(MGJamGame Game, GraphicsDevice Graphics) : base(Game, Graphics)
        {
            Target = new RenderTarget2D(Graphics, GameVars.GAME_WIDTH, GameVars.GAME_HEIGHT,
                false, Graphics.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            Batch = new SpriteBatch(Graphics);
            sq = Utilities.Block.ColorBlock(2, Color.Red, Graphics);
        }

        #region Initialization

        public void Initialize()
        {
            InputHelper.Update();
        }

        public void LoadContent()
        {
            HUD = new HUD(Graphics, Game.Content.Load<SpriteFont>("fonts/HUD"));

            // Load Images
            {
                ContentManager.AddImage("Projectile", Game.Content.Load<Texture2D>("projectiles/fireball"));
                ContentManager.AddImage("EnemyProjectile", Game.Content.Load<Texture2D>("projectiles/enemy_fireball"));
                ContentManager.AddImage("Tileset", Game.Content.Load<Texture2D>("tiles/dungeon_sheet"));
                ContentManager.AddImage("Knight", Game.Content.Load<Texture2D>("tiles/knight_proper"));
                ContentManager.AddImage("Portal", Game.Content.Load<Texture2D>("tiles/portal"));
            }
            // Load audio
            {
                var music_dark = Game.Content.Load<Song>("music/dark");
                MediaPlayer.Volume = 0.8f;
                MediaPlayer.Play(music_dark);
                ContentManager.AddSound("enemy_fire", Game.Content.Load<SoundEffect>("sfx/enemy_fire"));
                ContentManager.AddSound("fire", Game.Content.Load<SoundEffect>("sfx/fire"));
                ContentManager.AddSound("hit", Game.Content.Load<SoundEffect>("sfx/hit"));
                ContentManager.AddSound("warp", Game.Content.Load<SoundEffect>("sfx/warp"));
                ContentManager.AddSound("win", Game.Content.Load<SoundEffect>("sfx/win"));

                SoundManager.LoadSounds(ContentManager.GetAllSounds());
            }

            Level = new Level(Graphics);

            sprites = new List<Sprite>();

            sprites.AddRange(Level.Initialize());
            door = (Door)sprites.Where(x => x is Door).First();
            Player = new Player(ContentManager.GetImage("Knight"), Level.StartPosition);
            sprites.Add(Player);
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            InputHelper.Update();

            var pc = InputHelper.GetPlayerController();

            var Projectiles = Player.Update(pc, sprites, gameTime);

            var enProjectiles = new List<EnemyProjectile>();

            bool touching;
            touching = door.Update(Player, gameTime);
            if (touching)
            {
                ClearAndLoad(Level.NextLevel());
            }

            foreach (var sprite in sprites)
            {
                if (sprite == Player)
                    continue;

                if (sprite is Enemy)
                {
                    var enemy = (Enemy)sprite;
                    enProjectiles.AddRange(enemy.Update(gameTime, sprites, Player));
                }
                else if (sprite is Wall)
                {
                    var wall = (Wall)sprite;
                    wall.Update(gameTime, sprites.Where(x => x is Projectile).ToList());
                } else if (sprite is Door)
                {
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
                        SoundManager.PlaySound("warp");
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
                SoundManager.PlaySound("fire");
                foreach (var proj in Projectiles)
                {
                    sprites.Add(proj);
                }
            }
            if (enProjectiles != null && enProjectiles.Count() > 0)
            {
                SoundManager.PlaySound("fire");
                foreach (var proj in enProjectiles)
                {
                    sprites.Add(proj);
                }
            }

            checkIfAllEnemiesAreDead();

            HUD.Update(gameTime);
        }

        private void ClearAndLoad(List<Sprite> newSprites)
        {
            newSprites.Add(Player);
            Player.TeleportTo(Level.StartPosition);
            sprites = newSprites;
            door = (Door)sprites.Where(x => x is Door).First();
        }

        private void checkIfAllEnemiesAreDead()
        {
            if (sprites.Where(x => x is Enemy).Count() == 0)
            {
                Door door = (Door)sprites.Where(x => x is Door).First();
                door.Open();
            }
        }

        public RenderTarget2D Draw(GameTime gameTime)
        {
            Graphics.SetRenderTarget(Target);
            Graphics.Clear(new Color(6, 6, 6));

            Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.Default, RasterizerState.CullNone);
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
