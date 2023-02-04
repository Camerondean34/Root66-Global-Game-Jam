using Microsoft.Xna.Framework.Graphics;

namespace Root66.GameFolder.Obsticals
{
    internal class Fuel : Obstical
    {
        public Fuel(Texture2D pSpriteTexture, int pScreenWidth, int pScreenHeight, int pDrawWidth, int pDrawHeight) : base(pSpriteTexture, pScreenWidth, pScreenHeight, pDrawWidth, pDrawHeight) { }

        public override void Effect(Player pPlayer)
        {
            pPlayer.GiveFuel(30);
        }
    }
}
