using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root66.GameFolder
{
    internal class DisplayBar : Sprite
    {
        private float _ProgressRatio;
        public float ProgressRatio { get { return _ProgressRatio; } set { if (value >= 0 && value <= 1) { _ProgressRatio = value; } } }

        private Color displayColour;

        public DisplayBar(Texture2D pTexture, int pDrawWidth, int pDrawHeight, float pX, float pY, Color pColour) : base(pTexture, pDrawWidth, pDrawHeight, pX, pY)
        {
            displayColour = pColour;
            colour = Color.Black;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Rectangle displayRect = rectangle;
            displayRect.Width = (int)(rectangle.Width * ProgressRatio);
            spriteBatch.Draw(texture, displayRect, displayColour);
        }

        public override void Reset()
        {
            base.Reset();
            _ProgressRatio = 1;
        }
    }
}
