using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root66.GameFolder
{
    internal sealed class Player : Sprite
    {
        public const int MaxHealth = 100;
        public float Health { get; private set; }

        public const int MaxFuel = 100;
        public float Fuel { get; private set; }

        public bool Alive { get { return Health > 0 && Fuel > 0; } }

        private int screenHeight;
        private int drawHeight;
        public float speed { get; private set; }
        public float speedRatio { get; private set; }

        public Player(int pScreenHeight, Texture2D pSpriteTexture, int pDrawHeight, float pSpeed) : base(pSpriteTexture, pDrawHeight * (pSpriteTexture.Width / pSpriteTexture.Height), pDrawHeight, 10, pScreenHeight / 2)
        {
            screenHeight = pScreenHeight;
            drawHeight = pDrawHeight;
            speed = pSpeed;
            colour = Color.CornflowerBlue;
        }

        public override void Update(GameTime deltaTime)
        {
            if (yPosition < screenHeight * 0.22 || yPosition > (screenHeight * 0.725) - drawHeight)
            {
                speedRatio = 0.5f;
                GiveHealth(-0.2f);
            }
            else
            {
                speedRatio = 1;
            }
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
            GiveFuel(-0.05f * speedRatio);
        }

        public override void Reset()
        {
            Health = MaxHealth;
            Fuel = MaxFuel;
            base.Reset();
        }

        public void GiveHealth(float pHealth)
        {
            Health += pHealth;
            if (Health > MaxHealth) Health = MaxHealth;
        }

        public void GiveFuel(float pFuel)
        {
            Fuel += pFuel;
            if (Fuel > MaxFuel) Fuel = MaxFuel;
        }
    }
}
