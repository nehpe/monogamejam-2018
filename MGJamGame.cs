using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameJam.Scenes;

namespace MonoGameJam
{
    public class MGJamGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PlayScene CurrentScene;
        
        public MGJamGame()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferHeight = GameVars.SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = GameVars.SCREEN_WIDTH;

            graphics.ApplyChanges();

            Window.Title = "Change Is Goooooood";

            IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            CurrentScene = new PlayScene(this, graphics.GraphicsDevice);
            CurrentScene.Initialize();
            CurrentScene.LoadContent();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            CurrentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            var output = CurrentScene.Draw(gameTime); 
            GraphicsDevice.Clear(new Color(6,6,6));

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);
            {
                spriteBatch.Draw(output, new Rectangle(0, 0, GameVars.SCREEN_WIDTH, GameVars.SCREEN_HEIGHT), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
