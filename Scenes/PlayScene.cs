﻿using Microsoft.Xna.Framework;
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

        protected Texture2D floor_tile;
        protected Vector2 TileOffset = new Vector2(32, 32);
        protected Vector2 TileXY = new Vector2(18, 9);

        protected Player Player;

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
            floor_tile = Game.Content.Load<Texture2D>("tiles/floor");
            Center = new Vector2(GameVars.GAME_WIDTH / 2, GameVars.GAME_HEIGHT / 2);
            Player = new Player(Utilities.Block.ColorBlock(24, Color.Chocolate, Graphics), Center);

            sprites = new List<Sprite>();
            sprites.Add(Player);

            sprites.Add(new Sprite(Utilities.Block.ColorBlock(24, Color.Purple, Graphics), Center - new Vector2(32, 32)));

        }

        #endregion

        public void Update(GameTime gameTime)
        {
            InputHelper.Update();

            var pc = InputHelper.GetPlayerController();

            Player.Update(pc, sprites, gameTime);
        }

        public RenderTarget2D Draw(GameTime gameTime)
        {
            Graphics.SetRenderTarget(Target);
            Graphics.Clear(new Color(6, 6, 6));

            Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);

            {
                #region Floor Rendering
                for (var w = 0; w < TileXY.X; w++)
                {
                    for (var h = 0; h < TileXY.Y; h++)
                    {
                        Batch.Draw(floor_tile, new Rectangle((int)(TileOffset.X + (w * 32)), (int)(TileOffset.Y + (h * 32)), 32, 32), Color.White * 0.5f);
                    }
                }
                #endregion

                foreach (var sprite in sprites)
                {
                    sprite.Draw(gameTime, Batch);
                }

                //Batch.Draw(Player, new Rectangle((int)(Center.X-12), (int)(Center.Y-12), 24, 24), Color.White * 0.5f);
            }

            Batch.End();

            Graphics.SetRenderTarget(null);
            return Target;
        }
    }
}