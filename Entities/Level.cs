using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace MonoGameJam.Entities
{
    public class Level
    {
        protected TmxMap Map;
        protected int TileSize = 16;
        protected Texture2D FloorTiles;
        protected int TilesetWidth;

        // Offset could be set on the TMX file, but probably
        // isn't necessary in the long term
        protected Vector2 Offset = new Vector2(32, 32);

        protected SpriteBatch Batch;
        public Level(GraphicsDevice Graphics)
        {
            Batch = new SpriteBatch(Graphics);
            Map = new TmxMap("Content/map.tmx");

            FloorTiles = ContentManager.GetImage("Tileset");
            TilesetWidth = FloorTiles.Width / 16;
        }

        public List<Sprite> Initialize()
        {
            List<Sprite> Sprites = new List<Sprite>();

            Sprites.AddRange(initWalls());

            Sprites.AddRange(initObjects());

            Sprites.AddRange(initEnemies());

            return Sprites;
        }

        private List<Sprite> initEnemies()
        {
            var Enemies = new List<Sprite>();

            Rectangle tilesetRec;
            for (var i = 0; i < Map.Layers[1].Tiles.Count; i++)
            {
                int gid = Map.Layers[1].Tiles[i].Gid;

                if (gid == 0) // empty tile slot
                {
                    continue;
                }
                else
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % TilesetWidth;
                    int row = (int)Math.Floor((double)tileFrame / (double)TilesetWidth);
                    float x = (i % Map.Width) * TileSize + Offset.X;
                    float y = (float)Math.Floor(i / (double)Map.Width) * TileSize + Offset.Y;

                    tilesetRec = new Rectangle(TileSize * column, TileSize * row, TileSize, TileSize);

                    Enemies.Add(new Enemy(tilesetRec, FloorTiles, new Vector2(x, y)));
                }
            }
            return Enemies;
        }

        private List<Sprite> initWalls()
        {
            var Walls = new List<Sprite>();

            Rectangle tilesetRec;
            for (var i = 0; i < Map.Layers[3].Tiles.Count; i++)
            {
                int gid = Map.Layers[3].Tiles[i].Gid;

                if (gid == 0) // empty tile slot
                {
                    continue;
                }
                else
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % TilesetWidth;
                    int row = (int)Math.Floor((double)tileFrame / (double)TilesetWidth);
                    float x = (i % Map.Width) * TileSize + Offset.X;
                    float y = (float)Math.Floor(i / (double)Map.Width) * TileSize + Offset.Y;

                    tilesetRec = new Rectangle(TileSize * column, TileSize * row, TileSize, TileSize);

                    Walls.Add(new Wall(tilesetRec, FloorTiles, new Vector2(x, y)));
                }
            }
            return Walls;
        }

        private List<Sprite> initObjects()
        {
            var Items = new List<Sprite>();

            Rectangle tilesetRec;
            for (var i = 0; i < Map.Layers[2].Tiles.Count; i++)
            {
                int gid = Map.Layers[2].Tiles[i].Gid;

                if (gid == 0) // empty tile slot
                {
                    continue;
                }
                else
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % TilesetWidth;
                    int row = (int)Math.Floor((double)tileFrame / (double)TilesetWidth);
                    float x = (i % Map.Width) * TileSize + Offset.X;
                    float y = (float)Math.Floor(i / (double)Map.Width) * TileSize + Offset.Y;

                    tilesetRec = new Rectangle(TileSize * column, TileSize * row, TileSize, TileSize);

                    Items.Add(new Item(tilesetRec, FloorTiles, new Vector2(x, y)));
                }
            }
            return Items;
        }

        public void Draw(GameTime gameTime)
        {
            Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);
            {
                DrawFloor();
            }
            Batch.End();
        }

        private void DrawFloor()
        {
            Rectangle tilesetRec;
            for (var i = 0; i < Map.Layers[0].Tiles.Count; i++)
            {
                int gid = Map.Layers[0].Tiles[i].Gid;

                if (gid == 0) // empty tile slot
                {
                    continue;
                }
                else
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % TilesetWidth;
                    int row = (int)Math.Floor((double)tileFrame / (double)TilesetWidth);
                    float x = (i % Map.Width) * TileSize + Offset.X;
                    float y = (float)Math.Floor(i / (double)Map.Width) * TileSize + Offset.Y;

                    tilesetRec = new Rectangle(TileSize * column, TileSize * row, TileSize, TileSize);

                    Batch.Draw(FloorTiles,
                        new Rectangle(
                            (int)x, (int)y, TileSize, TileSize),
                        tilesetRec,
                        Color.White);

                }
            }
        }
    }
}