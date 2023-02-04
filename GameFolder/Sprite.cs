using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Root66.GameFolder
{
    internal class Sprite
    {
        protected Texture2D texture;
        protected Rectangle rectangle;
        protected Color colour = Color.White;

        public float xPosition { get; protected set; }
        public float yPosition { get; protected set; }

        private float xResetPosition;
        private float yResetPosition;

        public Sprite(Texture2D pSpriteTexture, int pDrawWidth, int pDrawHeight, float pResetX, float pResetY)
        {
            texture = pSpriteTexture;
            xResetPosition = pResetX;
            yResetPosition = pResetY;
            rectangle = new Rectangle(0, 0, pDrawWidth, pDrawHeight);

            Reset();
        }

        public void SetPosition(float x, float y)
        {
            xPosition = (int)Math.Round(x);
            yPosition = (int)Math.Round(y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            rectangle.X = (int)Math.Round(xPosition);
            rectangle.Y = (int)Math.Round(yPosition);
            spriteBatch.Draw(texture, rectangle, colour);
        }

        public virtual void Update(GameTime deltaTime)
        {

        }

        public virtual void Reset()
        {
            SetPosition(xResetPosition, yResetPosition);
        }

        public bool IntersectsWith(Sprite s)
        {
            return rectangle.Intersects(s.rectangle);
        }
    }
}
