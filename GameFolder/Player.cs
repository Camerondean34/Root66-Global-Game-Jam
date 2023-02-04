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
        float speed;
        float drag;

        public Player(int pScreenHeight, Texture2D pSpriteTexture, float pSpeed, Background pBg) : base(pSpriteTexture, (pScreenHeight / 10) * (pSpriteTexture.Width / pSpriteTexture.Height), pScreenHeight / 10, 0, pScreenHeight / 2)
        {
            screenHeight = pScreenHeight;
            speed = pSpeed;
            colour = Color.Blue;
            bg = pBg;
        }

        public override void Update(float deltaTime)
        {
            if (yPosition < 105 || yPosition > 348 - (screenHeight / 10))
            {
                drag = 0.5f;
                bg.SetSlow(true);
            }
            else
            {
                drag = 1;
                bg.SetSlow(false);
            }

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                yPosition -= speed * drag;
            }
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                yPosition += speed * drag;
            }
            base.Update(deltaTime);
        }

    }
}
