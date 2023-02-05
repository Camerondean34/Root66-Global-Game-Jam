using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root66.GameFolder.Obsticals
{
    enum Direction { Up, Down }
    internal class Person : Obstical
    {
        private Direction Direction;

        public Person(Texture2D pSpriteTexture, int pScreenWidth, int pScreenHeight, int pDrawWidth, int pDrawHeight, Direction pDirection) : base(pSpriteTexture, pScreenWidth, pScreenHeight, pDrawWidth, pDrawHeight)
        {
            Direction = pDirection;
        }

        public override void Update(GameTime deltaTime)
        {
            int yChange = 2;
            if (Direction == Direction.Up) yChange = -yChange;
            yPosition += yChange;
            base.Update(deltaTime);
        }

        public override void Effect(Player pPlayer)
        {
            throw new NotImplementedException();
        }
    }
}
