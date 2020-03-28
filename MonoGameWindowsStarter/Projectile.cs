using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
    enum ProjectileAnimState
    {
        Idle, 
        North, 
        South,
        East, 
        West
    }
    
    class Projectile
    {
        const int FRAME_RATE = 100;
        int speed = 5;
        int currentFrame = 0;

        Random random = new Random();

        Sprite[] frames;

        ProjectileAnimState animState = ProjectileAnimState.Idle;

        TimeSpan animationTimer;

        SpriteEffects spriteEffects = SpriteEffects.None;

        Color color = Color.White;

        Vector2 origin = new Vector2(25, 0);
        public Vector2 position = new Vector2(0, 0);

        public BoundingRectangle Bounds => new BoundingRectangle(position - 1.8f * origin, 50, 200);

        public Projectile(IEnumerable<Sprite> frames)
        {
            this.frames = frames.ToArray();
            animState = ProjectileAnimState.North;
            position.X = (410 * random.Next(5)) + 200;
        }

        public void Update(GameTime gameTime)
        {
            
            

            switch(animState)
            {
                case ProjectileAnimState.Idle:
                    currentFrame = 0;
                    animationTimer = new TimeSpan(0);
                    break;

                case ProjectileAnimState.North:
                    animationTimer += gameTime.ElapsedGameTime;
                    if (animationTimer.TotalMilliseconds > FRAME_RATE * 8)
                    {
                        animationTimer = new TimeSpan(0);
                    }
                    //currentFrame = (int)Math.Floor(animationTimer.TotalMilliseconds / FRAME_RATE) + 1;
                    position.Y -= 10;
                    break;
            }

            if(position.Y + Bounds.Height < 0)
            {
                position.Y = 1080;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            frames[currentFrame].Draw(spriteBatch, position, color, 0, origin, 2, spriteEffects, 1);
        }
    }
}
