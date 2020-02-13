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
    /// <summary>
    /// A class representing a bullet
    /// </summary>
    public class Bullet
    {
        /// <summary>
        /// The game object
        /// </summary>
        Game1 game;

        /// <summary>
        /// This bullet's bounds
        /// </summary>
        public BoundingRectangle Bounds;

        /// <summary>
        /// This paddle's texture
        /// </summary>
        Texture2D texture;

        Random Random = new Random();

        public SoundEffect bulletHitFX;

        /// <summary>
        /// Creates a bullet
        /// </summary>
        /// <param name="game">The game this bullet belongs to</param>
        public Bullet(Game1 game)
        {
            this.game = game;
        }

        /// <summary>
        /// Initializes the paddle, setting its initial size 
        /// and centering it on the left side of the screen.
        /// </summary>
        public void Initialize()
        {
            Bounds.Width = 200;
            Bounds.Height = 50;
            Bounds.X = 2000;
            Bounds.Y = game.GraphicsDevice.Viewport.Height / 2 - Bounds.Height / 2;
        }

        /// <summary>
        /// Loads the bullet's content
        /// </summary>
        /// <param name="content">The ContentManager to use</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("bulletLeft");
            bulletHitFX = content.Load<SoundEffect>("hit_hurt");
        }

        /// <summary>
        /// Updates the bullet
        /// </summary>
        /// <param name="gameTime">The game's GameTime</param>
        public void Update(GameTime gameTime)
        {
            Bounds.X -= 14;

            if (Bounds.X + Bounds.Width < 0)
            {
                Bounds.X = 2000; //Bounds.Y = (float)Random.Next(768);
                Bounds.Y = 200;
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
            spriteBatch.Draw(texture, Bounds, Color.White);
        }
    }
}