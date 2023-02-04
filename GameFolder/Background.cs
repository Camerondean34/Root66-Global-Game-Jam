using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root66.GameFolder
{
    internal class Background : Sprite
    {
        private int screenWidth;
        private int width;

        private float speed;
        private float speedChange;
        private bool slow;

        private Sprite secondSprite;

        public Background(int pScreenWidth, int pScreenHeight, Texture2D pSpriteTexture, float pSpeed) : base(pSpriteTexture, (int)(pScreenHeight * (pSpriteTexture.Width / (float)pSpriteTexture.Height)), pScreenHeight, pScreenHeight * ((float)pSpriteTexture.Width / (float)pSpriteTexture.Height), 0)
        {
            screenWidth = pScreenWidth;
            speed = pSpeed;
            width = (int)(pScreenHeight * ((float)pSpriteTexture.Width / pSpriteTexture.Height));
            secondSprite = new Sprite(pSpriteTexture, width, pScreenHeight, width, 0);
            SetPosition(0, 0);
        }

        public override void Update(float deltaTime)
        {
            if (slow) { speedChange = 0.5f; }
            else { speedChange = 1f; }

            SetPosition(xPosition - (speed * speedChange), yPosition);
            secondSprite.SetPosition(secondSprite.xPosition - (speed * speedChange), yPosition);

            if (xPosition < -width)
            {
                Reset();
            }
            else if (secondSprite.xPosition < -width)
            {
                secondSprite.Reset();
            }
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            secondSprite.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        public void SetSlow(bool pSlow)
        {
            slow = pSlow;
        }
    }
}
