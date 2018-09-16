using DumbUI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DumbUI
{
    class PlayerUI
    {
        public List<Panel> Elements { get; private set; }

        public PlayerUI()
        {
            Elements = new List<Panel>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Panel x in Elements)
            {
                x.Draw(spriteBatch);
            }
        }

        public void UpdatePositions(Vector2 screenSize, bool vOffset, bool hSplit)
        {
            if(hSplit)
            {
                screenSize.Y /= 2;
            }

            Console.WriteLine(vOffset + ": " + screenSize);

            foreach(Panel x in Elements)
            {
                x.UpdatePositions(screenSize, vOffset);
            }
        }
    }
}
