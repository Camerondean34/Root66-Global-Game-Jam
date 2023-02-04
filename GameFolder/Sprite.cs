using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Root66.GameFolder
{
    public class Sprite
    {
        private Texture2D texture;
        protected Rectangle rectangle;
        protected Color colour = Color.White;

        public float xPosition { get; protected set; }
        public float yPosition { get; protected set; }

        private float xResetPosition;
        private float yResetPosition;

        public Sprite(int pScreenWidth, int pScreenHeight, Texture2D pSpriteTexture, int pDrawWidth, float pResetX, float pResetY)
        {
            texture = pSpriteTexture;
            xResetPosition = pResetX;
            yResetPosition = pResetY;

            float aspect = pSpriteTexture.Width / pSpriteTexture.Height;
            int height = (int)Math.Round(pDrawWidth / aspect);
            rectangle = new Rectangle(0, 0, pDrawWidth, height);

            Reset();
        }

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

        public virtual void Update(float deltaTime)
        {

        }

        public virtual void Reset()
        {
            SetPosition(xResetPosition, yResetPosition);
        }
    }
}
