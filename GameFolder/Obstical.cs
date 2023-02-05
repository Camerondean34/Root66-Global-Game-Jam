using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX;
using System;

namespace Root66.GameFolder
{
    internal abstract class Obstical : Sprite
    {
        public float xChange = 0;

        public Obstical(Texture2D pSpriteTexture, int pScreenWidth, int pScreenHeight, int pDrawWidth, int pDrawHeight) : base(pSpriteTexture, pDrawWidth, pDrawHeight, -100, -100)
        {
            SetPosition(pScreenWidth, (float)new Random().Next(pScreenHeight * 220, (pScreenHeight * 725) - (pDrawHeight * 1000)) / 1000);
        }

        public override void Update(GameTime deltaTime)
        {
            xPosition -= (int)xChange;
        }

        public abstract void Effect(Player pPlayer);
    }
}
