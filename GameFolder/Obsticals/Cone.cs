using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root66.GameFolder.Obsticals
{
    internal class Cone : Obstical
    {
        public Cone(Texture2D pSpriteTexture, int pScreenWidth, int pScreenHeight, int pDrawWidth, int pDrawHeight) : base(pSpriteTexture, pScreenWidth, pScreenHeight, pDrawWidth, pDrawHeight) { }

        public override void Effect(Player pPlayer)
        {
            pPlayer.GiveHealth(-10);
        }
    }
}
