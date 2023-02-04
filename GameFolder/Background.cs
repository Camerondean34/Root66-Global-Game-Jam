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

        private Sprite secondSprite;

        public Background(int pScreenWidth, int pScreenHeight, Texture2D pSpriteTexture, float pSpeed) : base(pSpriteTexture, pScreenHeight * (pSpriteTexture.Width / pSpriteTexture.Height), pScreenHeight, pScreenHeight * (pSpriteTexture.Width / pSpriteTexture.Height), 0)
        {
            screenWidth = pScreenWidth;
            speed = pSpeed;
            width = pScreenHeight * (pSpriteTexture.Width / pSpriteTexture.Height);
            secondSprite = new Sprite(pSpriteTexture, width, pScreenHeight, width, 0);
            SetPosition(0, 0);
        }

        public override void Update(float deltaTime)
        {
            SetPosition(xPosition - speed, yPosition);
            secondSprite.SetPosition(secondSprite.xPosition - speed, yPosition);

            if (xPosition < 5 - width)
            {
                Reset();
            }
            else if (secondSprite.xPosition < 5 - width)
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
    }
}
