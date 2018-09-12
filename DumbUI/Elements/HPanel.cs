using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DumbUI.Elements
{
    public class HPanel : Element
    {
        List<Element> elements;

        float totalWidth;
        int margin;

        public HPanel(int margin = 10)
        {
            elements = new List<Element>();

            this.margin = margin;
        }

        internal override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            float space = 0;
            // Must be 33% because Labels will also move themself
            position.X -= totalWidth * .33f;

            foreach(Element e in elements)
            {
                e.Draw(spriteBatch, position + new Vector2(space, 0));
                space += e.GetSize().X + margin;
            }
        }

        public void AddElement(Element elem)
        {
            elements.Add(elem);
            totalWidth += elem.GetSize().X + margin;
        }

        internal override Vector2 GetSize()
        {
            return new Vector2();
        }
    }
}
