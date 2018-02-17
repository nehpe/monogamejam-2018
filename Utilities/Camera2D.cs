using System.Linq.Expressions;
using System.Net.NetworkInformation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJam.Utilities
{
    public class Camera2D
    {
        private Viewport _viewport;

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }
        public Vector2 Origin { get; set; }

        public Camera2D(Viewport viewport)
        {
            this._viewport = viewport;

            Rotation = 0;
            Zoom = 1;
            //Origin = new Vector2(
                //viewport.Width / 2f,
                //viewport.Height / 2f
            //);
            Origin = new Vector2(
                GameVars.GAME_WIDTH / 2f,
                GameVars.GAME_HEIGHT / 2f);

            Position = Vector2.Zero;
        }

        public Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                   Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                   Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateScale(Zoom, Zoom, 1) *
                   Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public void LookAt(Vector2 position)
        {
            Position = position - new Vector2(GameVars.GAME_WIDTH / 2f, GameVars.GAME_HEIGHT / 2f);
        }

        public Vector2 WorldToScreen(float x, float y)
        {
            return WorldToScreen(new Vector2(x, y));
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition + new Vector2(_viewport.X, _viewport.Y), GetViewMatrix());
        }

        public Vector2 ScreenToWorld(float x, float y)
        {
            return ScreenToWorld(new Vector2(x, y));
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition - new Vector2(_viewport.X, _viewport.Y),
                Matrix.Invert(GetViewMatrix()));
        }

        public void Update(Vector2 Velocity)
        {
            Position += Velocity;
        }
    }
}