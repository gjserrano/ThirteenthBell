using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Bullet bullet;
        Player player;

        Color color = Color.Tan;

        public Random Random = new Random();

        SpriteFont spriteFont;

        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            bullet = new Bullet(this);
            player = new Player(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Set the game screen size
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
            bullet.Initialize();
            player.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("defaultFont");
            bullet.LoadContent(Content);
            player.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            newKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (newKeyboardState.IsKeyDown(Keys.Escape))
                Exit();

            bullet.Update(gameTime);
            player.Update(gameTime);

            if (player.Bounds.CollidesBottom(bullet.Bounds))
            {
                //bullet.Bounds.X = (float)Random.Next(graphics.PreferredBackBufferWidth - (int)bullet.Bounds.Width);
                //bullet.Bounds.Y = graphics.PreferredBackBufferHeight + bullet.Bounds.Height;
                //bullet.Bounds.X = 2000;
                //bullet.Bounds.Y = 200;
                color = Color.PaleVioletRed;
                bullet.bulletHitFX.Play();
            }
            else
                color = Color.Tan;

            // TODO: Add your update logic here

            oldKeyboardState = newKeyboardState;
            //var size = spriteFont.MeasureString("Use Arrow Keys to Navigate");

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(color);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.DrawString(
                spriteFont,
                "Use Arrow Keys to Move\nDon't get hit!!",
                new Vector2(graphics.PreferredBackBufferWidth/2 - 100, graphics.PreferredBackBufferHeight - 200),
                Color.White
                );
            //spriteBatch.DrawString()
            bullet.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
