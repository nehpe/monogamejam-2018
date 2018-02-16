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

        static Vector2 lastShootDirection;
        static int shootCounter = 5;
        static int initShootCounter = 5;

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

            #region Movement stuffs
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

            #endregion

            #region Shooting Stuffs

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

            if (shootDirection != Vector2.Zero)
            {
                shootCounter = initShootCounter;
            }
            shootCounter--;

            shootDirection = new Vector2(
                MathHelper.Clamp(shootDirection.X + lastShootDirection.X, -1, 1),
                MathHelper.Clamp(shootDirection.Y + lastShootDirection.Y, -1, 1)
                );

            lastShootDirection = shootDirection;

            if (shootCounter == 0)
            {
                shootCounter = initShootCounter;
                lastShootDirection = Vector2.Zero;
                pc.ShootDirection = shootDirection;
            } else
            {
                pc.ShootDirection = Vector2.Zero;
            }

            #endregion

            #region Mouse Stuffs
            var mouse = Mouse.GetState();
            var mouseState = new nMouseState();


            mouseState.Position = mouse.Position;
            mouseState.Position.X /= GameVars.SCALE;
            mouseState.Position.Y /= GameVars.SCALE;
            mouseState.LeftClicked = (mouse.LeftButton == ButtonState.Pressed);
            pc.Mouse = mouseState;
            #endregion

            return pc;
        }
    }

    public class PlayerController
    {
        public Vector2 MovementDirection = Vector2.Zero;
        public Vector2 ShootDirection = Vector2.Zero;

        public nMouseState Mouse;
        
        public PlayerController()
        {

        }
    }

    public class nMouseState
    {
        public Point Position;
        public bool LeftClicked = false;
    }
}
