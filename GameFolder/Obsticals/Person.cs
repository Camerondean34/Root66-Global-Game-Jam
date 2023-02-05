using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX;
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
        static public Texture2D BloodTexture;
        private bool Alive = true;
        private Direction Direction;

        public Person(Texture2D pSpriteTexture, int pScreenWidth, int pScreenHeight, int pDrawWidth, int pDrawHeight, Direction pDirection) : base(pSpriteTexture, pScreenWidth, pScreenHeight, pDrawWidth, pDrawHeight)
        {
            Direction = pDirection;
            int yPos;
            if (Direction == Direction.Up) yPos = new Random().Next(pScreenHeight * 600, (pScreenHeight * 800)) / 1000;
            else yPos = new Random().Next(pScreenHeight * 200, pScreenHeight * 400) / 1000;
            SetPosition(pScreenWidth, yPos);
        }

        public override void Update(GameTime deltaTime)
        {
            if (Alive)
            {
                int yChange = 1;
                if (Direction == Direction.Up) yChange = -yChange;
                yPosition += yChange;
            }
            base.Update(deltaTime);
        }

        public override void Effect(Player pPlayer)
        {
            if (Alive)
            {
                Alive = false;
                texture = BloodTexture;
                pPlayer.GiveHealth(-25);
                pPlayer.GiveFuel(10);
            }
        }
    }
}
