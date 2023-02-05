using Microsoft.Xna.Framework.Graphics;

namespace Root66.GameFolder.Obsticals
{
    internal class Engine : Obstical
    {
        public Engine(Texture2D pSpriteTexture, int pScreenWidth, int pScreenHeight, int pDrawWidth, int pDrawHeight) : base(pSpriteTexture, pScreenWidth, pScreenHeight, pDrawWidth, pDrawHeight) { }

        public override void Effect(Player pPlayer)
        {
            pPlayer.GiveHealth(30);
        }

    }
}
