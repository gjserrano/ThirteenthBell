using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// A class representing a player
    /// </summary>
    public class Player
    {
        /// <summary>
        /// The game object
        /// </summary>
        Game1 game;

        /// <summary>
        /// This paddle's bounds
        /// </summary>
        public BoundingRectangle Bounds;

        /// <summary>
        /// This player's texture
        /// </summary>
        Texture2D texture;

        /// <summary>
        /// Creates a player
        /// </summary>
        /// <param name="game">The game this player belongs to</param>
        public Player(Game1 game)
        {
            this.game = game;
        }

        /// <summary>
        /// Initializes the player, setting its initial size 
        /// and centering it on the left side of the screen.
        /// </summary>
        public void Initialize()
        {
            Bounds.Width = 200;
            Bounds.Height = 200;
            Bounds.X = 0;
            Bounds.Y = game.GraphicsDevice.Viewport.Height / 2 - Bounds.Height / 2;
        }

        /// <summary>
        /// Loads the player's content
        /// </summary>
        /// <param name="content">The ContentManager to use</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Gunslinger");
        }

        /// <summary>
        /// Updates the player
        /// </summary>
        /// <param name="gameTime">The game's GameTime</param>
        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            // Move the paddle up if the up key is pressed
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                // move up
                Bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            // Move the paddle down if the down key is pressed
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                // move down
                Bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            // Move the player left if the left key is pressed
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Bounds.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            // Move the player right if the left key is pressed
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Bounds.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            // Stop the player from going off-screen
            if (Bounds.Y < 0)
            {
                Bounds.Y = 0;
            }
            if (Bounds.Y > game.GraphicsDevice.Viewport.Height - Bounds.Height)
            {
                Bounds.Y = game.GraphicsDevice.Viewport.Height - Bounds.Height;
            }
            if (Bounds.X < 0)
            {
                Bounds.X = 0;
            }
            if (Bounds.X > game.GraphicsDevice.Viewport.Width - Bounds.Width)
            {
                Bounds.X = game.GraphicsDevice.Viewport.Width - Bounds.Width;
            }
        }

        /// <summary>
        /// Draw the player
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch to draw the paddle with.  This method should 
        /// be invoked between SpriteBatch.Begin() and SpriteBatch.End() calls.
        /// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, Color.White);
        }
    }
}
