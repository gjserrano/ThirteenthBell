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
        enum State
        {
            South = 2,
            East = 3,
            West = 1,
            North = 0,
            Idle = 2,
        }

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
        /// How quickly the animation should advance frames (1/8 second as milliseconds)
        /// </summary>
        const int ANIMATION_FRAME_RATE = 64;

        /// <summary>
        /// How quickly the player should move
        /// </summary>
        const float PLAYER_SPEED = 250;

        /// <summary>
        /// The width of the animation frames
        /// </summary>
        const int FRAME_WIDTH = 130;

        /// <summary>
        /// The hieght of the animation frames
        /// </summary>
        const int FRAME_HEIGHT = 130;

        TimeSpan timer;
        State state;
        int frame;

        /// <summary>
        /// Creates a player
        /// </summary>
        /// <param name="game">The game this player belongs to</param>
        public Player(Game1 game)
        {
            this.game = game;
            timer = new TimeSpan(0);
            Bounds = new BoundingRectangle();
            state = State.Idle;
        }

        /// <summary>
        /// Initializes the player, setting its initial size 
        /// and centering it on the left side of the screen.
        /// </summary>
        public void Initialize()
        {
            Bounds.Width = FRAME_WIDTH;
            Bounds.Height = FRAME_HEIGHT;
            Bounds.X = game.GraphicsDevice.Viewport.Width / 2;
            Bounds.Y = 0;
        }

        /// <summary>
        /// Loads the player's content
        /// </summary>
        /// <param name="content">The ContentManager to use</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("C3ZwL2");
        }

        /// <summary>
        /// Updates the player
        /// </summary>
        /// <param name="gameTime">The game's GameTime</param>
        public void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update the player state based on input
            /*if (keyboard.IsKeyDown(Keys.Up))
            {
                state = State.North;
                Bounds.Y -= delta * PLAYER_SPEED;
            }*/
            /*else*/ if (keyboard.IsKeyDown(Keys.Left))
            {
                state = State.West;
                Bounds.X -= delta * PLAYER_SPEED;
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                state = State.East;
                Bounds.X += delta * PLAYER_SPEED;
            }
            /*else if (keyboard.IsKeyDown(Keys.Down))
            {
                state = State.South;
                Bounds.Y += delta * PLAYER_SPEED;
            }*/
            else state = State.Idle;

            // Update the player animation timer when the player is moving
            if (state != State.Idle) timer += gameTime.ElapsedGameTime;

            // Determine the frame should increase.  Using a while 
            // loop will accomodate the possiblity the animation should 
            // advance more than one frame.
            while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                // increase by one frame
                frame++;
                // reduce the timer by one frame duration
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }

            // Keep the frame within bounds (there are four frames)
            frame %= 4;
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
            // determine the source rectangle of the sprite's current frame
            var source = new Rectangle(
                frame * FRAME_WIDTH, // X value 
                (int)state % 4 * FRAME_HEIGHT, // Y value
                FRAME_WIDTH, // Width 
                FRAME_HEIGHT // Height
                );

            // render the sprite
            spriteBatch.Draw(texture, Bounds, source, Color.White);

            // render the sprite's coordinates in the upper-right-hand corner of the screen
            //spriteBatch.DrawString(font, $"X:{position.X} Y:{position.Y}", Vector2.Zero, Color.White);
            //spriteBatch.Draw(texture, Bounds, Color.White);
        }
    }
}


