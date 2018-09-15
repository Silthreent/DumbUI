using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DumbUI.Elements
{
    public class HPanel : Panel
    {
        public HPanel(int margin = 10)
        {
            this.margin = margin;

            elements = new List<Element>();
            size = new Vector2();
        }

        internal override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // Must be 33% because Labels will also center themself
            position.X -= size.X * .33f;

            float space = 0;
            foreach(Element e in elements)
            {
                e.Draw(spriteBatch, position + new Vector2(space, 0));
                space += e.GetSize().X + margin;
            }
        }

        public void AddElement(Element elem)
        {
            elements.Add(elem);

            var eSize = elem.GetSize();

            size.X += eSize.X + margin;
            if(eSize.Y > size.Y)
                size.Y = eSize.Y;
        }
    }
}
