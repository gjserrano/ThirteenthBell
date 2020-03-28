using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace MonoGameWindowsStarter
{

    enum Type
    {
        Fast,
        Bomb,
        Normal,
        //Teleport,
    }

    /// <summary>
    /// A class representing a bullet
    /// </summary>
    public class Bullet
    {
        Type bulletType;

        Color bulletTint;

        /// <summary>
        /// The game object
        /// </summary>
        Game1 game;

        int ySpeed = 0;

        /// <summary>
        /// This bullet's bounds
        /// </summary>
        public BoundingRectangle Bounds;

        /// <summary>
        /// This paddle's texture
        /// </summary>
        Texture2D texture;

        Random random = new Random(4);

        public SoundEffect bulletHitFX;

        /// <summary>
        /// Creates a bullet
        /// </summary>
        /// <param name="game">The game this bullet belongs to</param>
        public Bullet(Game1 game, int id)
        {
            this.game = game;

            if (id == 1)
            {
                bulletType = Type.Normal;
                bulletTint = Color.White;
            }
            else if (id == 2)
            {
                bulletType = Type.Fast;
                bulletTint = Color.Blue;
            }
            else if (id == 3)
            {
                bulletType = Type.Bomb;
                bulletTint = Color.Red;
            }

            switch (bulletType)
            {
                case Type.Normal:
                    ySpeed = -14;
                    break;
                case Type.Fast:
                    ySpeed = -20;
                    break;
                case Type.Bomb:
                    ySpeed = -5;
                    break;
            }
        }

        /// <summary>
        /// Initializes the paddle, setting its initial size 
        /// and centering it on the left side of the screen.
        /// </summary>
        public void Initialize()
        {
            Bounds.Width = 50;
            Bounds.Height = 200;
            Bounds.X = ((game.GraphicsDevice.Viewport.Width / 5) * random.Next(1, 6)) - ((game.GraphicsDevice.Viewport.Width / 5) / 2) - (Bounds.Width / 2);
            Bounds.Y = 1080;
        }

        /// <summary>
        /// Loads the bullet's content
        /// </summary>
        /// <param name="content">The ContentManager to use</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("bulletUp");
            bulletHitFX = content.Load<SoundEffect>("hit_hurt");
        }

        /// <summary>
        /// Updates the bullet
        /// </summary>
        /// <param name="gameTime">The game's GameTime</param>
        public void Update(GameTime gameTime)
        {
            Bounds.Y += ySpeed;

            if (Bounds.Y + Bounds.Height < 0)
            {
                Bounds.Y = 1000;
                Bounds.X = ((game.GraphicsDevice.Viewport.Width / 5) * random.Next(1, 6)) - ((game.GraphicsDevice.Viewport.Width / 5) / 2) - (Bounds.Width / 2);
            }
        }

        /// <summary>
        /// Draw the bullet
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch to draw the bullet with.  This method should 
        /// be invoked between SpriteBatch.Begin() and SpriteBatch.End() calls.
        /// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, bulletTint);
        }
    }
}