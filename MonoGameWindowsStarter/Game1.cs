using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteSheet sheet;

        //Bullet bullet;
        Bullet normalBullet;
        Bullet fastBullet;
        Bullet bombBullet;

        Player player;

        Lane lane0;
        Lane lane1;
        Lane lane2;
        Lane lane3;
        Lane lane4;

        String laneInd = "";

        Color color = Color.Tan;

        public Random Random = new Random();

        SpriteFont spriteFont;

        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;

        Texture2D background;
        Rectangle backgroundFrame;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //bullet = new Bullet(this, 3);
            normalBullet = new Bullet(this, 1);
            fastBullet = new Bullet(this, 2);
            bombBullet = new Bullet(this, 3);

            //player = new Player(this);
            //newLane = new Lane(this);
            lane0 = new Lane(this, 0);
            lane1 = new Lane(this, 1);
            lane2 = new Lane(this, 2);
            lane3 = new Lane(this, 3);
            lane4 = new Lane(this, 4);
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
            graphics.PreferredBackBufferWidth = 2048;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            //bullet.Initialize();
            normalBullet.Initialize();
            fastBullet.Initialize();
            bombBullet.Initialize();
            //player.Initialize();
            //newLane.Initialize();
            base.Initialize();
            lane0.Initialize();
            lane1.Initialize();
            lane2.Initialize();
            lane3.Initialize();
            lane4.Initialize();
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
            background = Content.Load<Texture2D>("Sand Wallpaper");
            backgroundFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            //bullet.LoadContent(Content);
            normalBullet.LoadContent(Content);
            fastBullet.LoadContent(Content);
            bombBullet.LoadContent(Content);
            //player.LoadContent(Content);
            //newLane.LoadContent(Content);
            lane0.LoadContent(Content);
            lane1.LoadContent(Content);
            lane2.LoadContent(Content);
            lane3.LoadContent(Content);
            lane4.LoadContent(Content);

            var t = Content.Load<Texture2D>("TestSpriteSheet");
            sheet = new SpriteSheet(t, 79, 85, 0, 0);


            // Create the player with the corresponding frames from the spritesheet
            var playerFrames = from index in Enumerable.Range(0, 9) select sheet[index];
            player = new Player(playerFrames);
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

            //bullet.Update(gameTime);
            normalBullet.Update(gameTime);
            fastBullet.Update(gameTime);
            bombBullet.Update(gameTime);
            player.Update(gameTime);

            if (player.Bounds.CollidesWith(normalBullet.Bounds) || player.Bounds.CollidesWith(fastBullet.Bounds) || player.Bounds.CollidesWith(bombBullet.Bounds))
            {
                color = Color.PaleVioletRed;
                //bullet.bulletHitFX.Play();
            }
            else
                color = Color.Tan;

            Lane[] lanes = { lane0, lane1, lane2, lane3, lane4 };
            for(int i = 0; i < lanes.Length;  i++)
            {
                if(player.Bounds.CheckLane(lanes[i].Bounds))
                {
                    laneInd = i.ToString();
                }
            }
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
                "Player Lane: " + laneInd,
                new Vector2(graphics.PreferredBackBufferWidth/2 - 100, graphics.PreferredBackBufferHeight - 200),
                Color.White
                );
            //Uncomment to check frames
            /*for (var i = 17; i < 30; i++)
            {
                sheet[i].Draw(spriteBatch, new Vector2(i * 25, 25), Color.White);
            }*/

            spriteBatch.Draw(background, Vector2.Zero, backgroundFrame, Color.White, 0f, Vector2.Zero, 0f, SpriteEffects.None, 1f);

            //bullet.Draw(spriteBatch);
            normalBullet.Draw(spriteBatch);
            fastBullet.Draw(spriteBatch);
            bombBullet.Draw(spriteBatch);

            player.Draw(spriteBatch);

            lane0.Draw(spriteBatch);
            lane1.Draw(spriteBatch);
            lane2.Draw(spriteBatch);
            lane3.Draw(spriteBatch);
            lane4.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
