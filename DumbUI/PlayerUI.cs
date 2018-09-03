using DumbUI.Elements;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Element x in Elements)
            {
                x.Draw(spriteBatch);
            }
        }
    }
}
