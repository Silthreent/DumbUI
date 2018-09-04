using DumbUI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DumbUI
{
    class PlayerUI
    {
        public List<Element> Elements { get; private set; }

        public PlayerUI()
        {
            Elements = new List<Element>();
        }

        public void Draw(SpriteBatch spriteBatch, bool vOffset = false, bool hSplit = false)
        {
            var scrWidth = spriteBatch.GraphicsDevice.Viewport.Width;
            var scrHeight = spriteBatch.GraphicsDevice.Viewport.Height;

            var width = scrWidth;
            var height = scrHeight;

            if(hSplit)
            {
                height /= 2;
            }

            foreach(Element x in Elements)
            {
                x.Draw(spriteBatch, new Vector2(
                    width * x.LeftAnchor,
                    height * x.TopAnchor + (vOffset ? scrHeight / 2 : 0)));
            }
        }
    }
}
