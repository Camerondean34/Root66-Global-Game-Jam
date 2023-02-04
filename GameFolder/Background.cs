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

        private float speed;
        private float _speedRatio = 1;
        public float speedRatio { get { return _speedRatio; } set { if (value >= 0 && value <= 1) { _speedRatio = value; } } }

        private Sprite secondSprite;

        public Background(int pScreenHeight, Texture2D pSpriteTexture, float pSpeed) : base(pSpriteTexture, (int)(pScreenHeight * ((float)pSpriteTexture.Width / pSpriteTexture.Height)), pScreenHeight, (int)(pScreenHeight * ((float)pSpriteTexture.Width / pSpriteTexture.Height)), 0)
        {
            speed = pSpeed;
            width = (int)(pScreenHeight * ((float)pSpriteTexture.Width / pSpriteTexture.Height));
            secondSprite = new Sprite(pSpriteTexture, width, pScreenHeight, width, 0);
        }

        public override void Update(GameTime deltaTime)
        {
            float xChange = speed * speedRatio;
            xPosition -= xChange;
            secondSprite.SetPosition(secondSprite.xPosition - xChange, secondSprite.yPosition);

            if (xPosition < -width)
            {
                SetPosition(width, 0);
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

        public override void Reset()
        {
            SetPosition(0, 0);
            if (secondSprite != null) secondSprite.Reset();
        }
    }
}
