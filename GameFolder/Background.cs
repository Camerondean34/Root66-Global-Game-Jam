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
        private int width;

        public float speed { get; private set; }
        private float _SpeedRatio = 1;
        public float SpeedRatio { get { return _SpeedRatio; } set { if (value >= 0 && value <= 1) { _SpeedRatio = value; } } }

        private Sprite secondSprite;

        public Background(int pScreenHeight, Texture2D pSpriteTexture, float pSpeed) : base(pSpriteTexture, (int)(pScreenHeight * ((float)pSpriteTexture.Width / pSpriteTexture.Height)), pScreenHeight, 0, 0)
        {
            speed = pSpeed;
            width = (int)(pScreenHeight * ((float)pSpriteTexture.Width / pSpriteTexture.Height));
            secondSprite = new Sprite(pSpriteTexture, width, pScreenHeight, width, 0);
        }

        public override void Update(GameTime deltaTime)
        {
            float xChange = speed * SpeedRatio;
            xPosition -= xChange;
            secondSprite.SetPosition(secondSprite.xPosition - xChange, secondSprite.yPosition);

            if (xPosition < -width)
            {
                SetPosition(xPosition + (2 * width), 0);
            }
            if (secondSprite.xPosition < -width)
            {
                secondSprite.SetPosition(secondSprite.xPosition + (2 * width), 0);
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
