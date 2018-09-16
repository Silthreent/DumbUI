using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DumbUI.Elements
{
    public class HPanel : Panel
    {
        public HPanel(Texture2D debug, int margin = 10)
        {
            debugTex = debug;
            elementsMargin = margin;
        }

        public override void AddElement(Element elem)
        {
            elem.Position = new Vector2(Position.X + Size.X + elementsMargin, Position.Y);
            elements.Add(elem);

            var eSize = elem.GetSize();
            if(elements.Count == 1)
            {
                Size = eSize;
                return;
            }

            Size = new Vector2(Size.X + elementsMargin + eSize.X, (eSize.Y > Size.Y ? eSize.Y : Size.Y));
        }

        internal override void UpdatePositions(Vector2 screenSize, bool offset)
        {
            Position = new Vector2((screenSize.X * LeftAnchor) - (Size.X * .5f), screenSize.Y * TopAnchor + (offset ? screenSize.Y : 0));

            float space = 0;
            foreach(Element e in elements)
            {
                e.Position = new Vector2(Position.X + space, Position.Y - (e.GetSize().Y / 2));
                space += e.GetSize().X + elementsMargin;
            }
        }
    }
}
