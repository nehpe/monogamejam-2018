using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Entities
{
    class Player : Sprite
    {
        float SPEED = 2;

        float ChangeDirectionCounter = 0f;

        Dictionary<AnimationDirection, Animation> Animations = new Dictionary<AnimationDirection, Animation>();

        private AnimationDirection CurrentAnimation = AnimationDirection.NORTH;

        private int tileSize = 16;

        public override Rectangle Rectangle { get => 
                new Rectangle(
                    (int) Position.X,
                    (int) Position.Y,
                    tileSize,
                    tileSize); }

        protected Vector2 Center { get => new Vector2(Rectangle.X + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height / 2); }

        public Player(Texture2D Image, Vector2 Position) : base(Image, Position)
        {

            #region Add Animation Directions

            Animations.Add(AnimationDirection.NORTH, new Animation()
            {
                totalFrames = Image.Width/tileSize,
                StartRect = new Rectangle(0,0,tileSize,tileSize)
            });
            Animations.Add(AnimationDirection.NORTHEAST, new Animation()
            {
                totalFrames = Image.Width/tileSize,
                StartRect = new Rectangle(0,16,tileSize,tileSize)
            });
            Animations.Add(AnimationDirection.EAST, new Animation()
            {
                totalFrames = Image.Width/tileSize,
                StartRect = new Rectangle(0,32,tileSize,tileSize)
            });
            Animations.Add(AnimationDirection.SOUTHEAST, new Animation()
            {
                totalFrames = Image.Width/tileSize,
                StartRect = new Rectangle(0,48,tileSize,tileSize)
            });
            Animations.Add(AnimationDirection.SOUTH, new Animation()
            {
                totalFrames = Image.Width/tileSize,
                StartRect = new Rectangle(0,64,tileSize,tileSize)
            });
            Animations.Add(AnimationDirection.SOUTHWEST, new Animation()
            {
                totalFrames = Image.Width/tileSize,
                StartRect = new Rectangle(0,80,tileSize,tileSize)
            });
            Animations.Add(AnimationDirection.WEST, new Animation()
            {
                totalFrames = Image.Width/tileSize,
                StartRect = new Rectangle(0,96,tileSize,tileSize)
            });
            Animations.Add(AnimationDirection.NORTHWEST, new Animation()
            {
                totalFrames = Image.Width/tileSize,
                StartRect = new Rectangle(0,112,tileSize,tileSize)
            });

            #endregion

        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            batch.Draw(SpriteImage,
               new Rectangle((int)Position.X, (int)Position.Y, tileSize, tileSize),
               Animations[CurrentAnimation].Rectangle,
               Color.White
           );
        }

        public void TeleportTo(Vector2 newPosition)
        {
            this.Position = newPosition;
        }

        public List<Projectile> Update(PlayerController pc, List<Sprite> sprites, GameTime gameTime)
        {
            AnimationDirection? tmpCurrentAnimation = null;

            #region Player Movement
            this.Velocity = Vector2.Zero;
            if (pc.MovementDirection != Vector2.Zero)
            {
                Velocity.X += (int)(pc.MovementDirection.X * SPEED * gameTime.ElapsedGameTime.TotalMilliseconds / 16f);
                Velocity.Y += (int)(pc.MovementDirection.Y * SPEED * gameTime.ElapsedGameTime.TotalMilliseconds / 16f);
            }

            if (this.Velocity != Vector2.Zero)
            {
                foreach (var sprite in sprites)
                {
                    if (sprite == this)
                        continue;

                    if (this.IsTouchingBottom(sprite))
                    {
                        this.Velocity.Y = 0;
                    }
                    if (this.IsTouchingTop(sprite))
                    {
                        this.Velocity.Y = 0;
                    }

                    if (this.IsTouchingRight(sprite))
                    {
                        this.Velocity.X = 0;
                    }
                    if (this.IsTouchingLeft(sprite))
                    {
                        this.Velocity.X = 0;
                    }
                }
            }

            // Update current animation
            if (Velocity != Vector2.Zero)
            {
                tmpCurrentAnimation = GetAnimationDirection(Velocity);
                Animations[CurrentAnimation].Update(gameTime);
            }
            #endregion

            #region Shooting stuffs

            var Projectiles = new List<Projectile>();

            if (pc.ShootDirection != Vector2.Zero)
            {
                Projectiles.Add(new Projectile(Center, pc.ShootDirection));

                tmpCurrentAnimation = GetAnimationDirection(pc.ShootDirection);
                if (tmpCurrentAnimation != CurrentAnimation)
                {
                    ChangeDirectionCounter = 900f;
                }

                CurrentAnimation = (AnimationDirection)tmpCurrentAnimation;
                tmpCurrentAnimation = null;
            }
            #endregion

            if (ChangeDirectionCounter <= 0 && tmpCurrentAnimation != null)
            {
                CurrentAnimation = (AnimationDirection)tmpCurrentAnimation;
            }

            if (ChangeDirectionCounter > 0)
            {
                ChangeDirectionCounter -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            base.Update(gameTime);

            return Projectiles;
        }

        #region Helpers

        private AnimationDirection GetAnimationDirection(Vector2 Velocity)
        {
            var AnimationDirectionality = AnimationDirection.SOUTH;
            if (Velocity.X > 0)
            {
                if (Velocity.Y == 0)
                {
                    AnimationDirectionality = AnimationDirection.EAST;
                } else if (Velocity.Y < 0)
                {
                    AnimationDirectionality = AnimationDirection.NORTHEAST;
                } else if (Velocity.Y > 0)
                {
                    AnimationDirectionality = AnimationDirection.SOUTHEAST;
                }
            } else if (Velocity.X < 0)
            {
                if (Velocity.Y == 0)
                {
                    AnimationDirectionality = AnimationDirection.WEST;
                } else if (Velocity.Y < 0)
                {
                    AnimationDirectionality = AnimationDirection.NORTHWEST;
                } else if (Velocity.Y > 0)
                {
                    AnimationDirectionality = AnimationDirection.SOUTHWEST;
                }
            } else if (Velocity.Y < 0)
            {
                AnimationDirectionality = AnimationDirection.NORTH;
            } else if (Velocity.Y > 0)
            {
                AnimationDirectionality = AnimationDirection.SOUTH;
            }

            return AnimationDirectionality;
        }

        #endregion
    }
}
