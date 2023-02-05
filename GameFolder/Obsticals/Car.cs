using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root66.GameFolder.Obsticals
{
    internal class Car : Obstical
    {
        public Car(Texture2D pSpriteTexture, int pScreenWidth, int pScreenHeight, int pDrawWidth, int pDrawHeight) : base(pSpriteTexture, pScreenWidth, pScreenHeight, pDrawWidth, pDrawHeight) { }

        public override void Effect(Player pPlayer)
        {
            pPlayer.GiveHealth(-100 * pPlayer.speedRatio);
        }

        public override void Update(GameTime deltaTime)
        {
            xChange = xChange * 1.8f;
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            rectangle.X = (int)Math.Round(xPosition);
            rectangle.Y = (int)Math.Round(yPosition);
            spriteBatch.Draw(texture, rectangle, null, colour, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
        }
    }
}
