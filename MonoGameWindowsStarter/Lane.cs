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
    public class Lane
    {
        Game1 game;

        public BoundingRectangle Bounds;

        public int laneID;

        Texture2D texture;

        Vector2 buildingLocation;

        public Lane(Game1 game, int id)
        {
            this.game = game;
            laneID = id;
            Bounds = new BoundingRectangle();
        }

        public void Initialize()
        {
            Bounds.Width = game.GraphicsDevice.Viewport.Width / 5;
            Bounds.Height = game.GraphicsDevice.Viewport.Height;
            Bounds.X = Bounds.Width * laneID;
            Bounds.Y = 0;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("LanesTest");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            buildingLocation = new Vector2(Bounds.X + Bounds.Width - 50, Bounds.Y);
            spriteBatch.Draw(texture, buildingLocation, Color.White);
        }
    }
}
