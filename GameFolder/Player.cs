using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root66.GameFolder
{
    internal class Player : Sprite
    {
        Background bg;

        int screenHeight;
        int drawHeight;
        float speed;
        float speedRatio;

        public Player(int pScreenHeight, Texture2D pSpriteTexture, int pDrawHeight, float pSpeed, Background pBg) : base(pSpriteTexture, pDrawHeight * (pSpriteTexture.Width / pSpriteTexture.Height), pDrawHeight, 0, pScreenHeight / 2)
        {
            screenHeight = pScreenHeight;
            drawHeight = pDrawHeight;
            speed = pSpeed;
            colour = Color.Blue;
            bg = pBg;
        }

        public override void Update(GameTime deltaTime)
        {
            if (yPosition < screenHeight * 0.22 || yPosition > (screenHeight * 0.725) - drawHeight)
            {
                speedRatio = 0.5f;
            }
            else
            {
                speedRatio = 1;
            }
            bg.speedRatio = speedRatio;

            float yChange = speed * speedRatio;

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                yPosition -= yChange;
            }
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                yPosition += yChange;
            }
            base.Update(deltaTime);
        }

    }
}
