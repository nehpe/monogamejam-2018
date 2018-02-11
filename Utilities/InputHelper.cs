using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Utilities
{
    static class InputHelper
    {
        static Keys[] LastKeys;
        static Keys[] PressedKeys;

        public static void Update()
        {
            LastKeys = PressedKeys;
            PressedKeys = Keyboard.GetState().GetPressedKeys();
        }

        public static bool IsKeyPressed(Keys key)
        {
            return PressedKeys.Contains(key);
        }

        public static bool IsKeyReleased(Keys key)
        {
            return !PressedKeys.Contains(key);
        }

        public static bool IsKeyJustPressed(Keys key)
        {
            return PressedKeys.Contains(key) && !LastKeys.Contains(key);
        }

        public static bool IsKeyJustReleased(Keys key)
        {
            return !PressedKeys.Contains(key) && LastKeys.Contains(key);
        }

        public static PlayerController GetPlayerController()
        {
            var pc = new PlayerController();
            var movementDirection = Vector2.Zero;

            if (IsKeyPressed(Keys.A))
            {
                movementDirection.X = -1;
            }
            if (IsKeyPressed(Keys.D))
            {
                movementDirection.X = 1;
            }

            if (IsKeyPressed(Keys.W))
            {
                movementDirection.Y = -1;
            }
            if (IsKeyPressed(Keys.S))
            {
                movementDirection.Y = 1;
            }

            pc.MovementDirection = movementDirection;

            var shootDirection = Vector2.Zero;

            if (IsKeyJustPressed(Keys.Left))
            {
                shootDirection.X = -1;
            }
            if (IsKeyJustPressed(Keys.Right))
            {
                shootDirection.X = 1;
            }

            if (IsKeyJustPressed(Keys.Up))
            {
                shootDirection.Y = -1;
            }
            if (IsKeyJustPressed(Keys.Down))
            {
                shootDirection.Y = 1;
            }

            pc.ShootDirection = shootDirection;

            return pc;
        }
    }

    public class PlayerController
    {
        public Vector2 MovementDirection = Vector2.Zero;
        public Vector2 ShootDirection = Vector2.Zero;
        public PlayerController()
        {

        }
    }
}
