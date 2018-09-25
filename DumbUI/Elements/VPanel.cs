using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DumbUI.Elements
{
    public class VPanel : Panel
    {
        public VPanel(int margin = 3)
        {
            elementsMargin = margin;
        }

        public override void AddElement(Element elem)
        {
            elem.Position = new Vector2(Position.X, Position.Y + Size.Y + elementsMargin);
            elements.Add(elem);

            var eSize = elem.GetSize();
            if(elements.Count == 1)
            {
                Size = eSize;
                return;
            }

            Size = new Vector2((eSize.X > Size.X ? eSize.X : Size.X), Size.Y + elementsMargin + eSize.Y);
        }

        internal override void MoveCursor(InputActions action, ref int selected, ref Panel selectedPanel)
        {
            switch(action)
            {
                case (InputActions.Up):
                    selected--;
                    if(selected < 0)
                    {
                        if(GetTop() != null)
                        {
                            selectedPanel = GetTop();
                        }

                        selected = selectedPanel.GetElementCount() - 1;
                    }
                    break;
                case (InputActions.Down):
                    selected++;
                    if(selected >= GetElementCount())
                    {
                        if(GetBottom() != null)
                        {
                            selectedPanel = GetBottom();
                        }

                        selected = 0;
                    }
                    break;
                case (InputActions.Left):
                    if(GetLeft() != null)
                    {
                        selectedPanel = GetLeft();
                        selected = 0;
                    }
                    break;
                case (InputActions.Right):
                    if(GetRight() != null)
                    {
                        selectedPanel = GetRight();
                        selected = 0;
                    }
                    break;
            }
        }

        internal override void UpdatePositions(Vector2 screenSize, bool offset)
        {
            Position = new Vector2((screenSize.X * LeftAnchor) - (Size.X * .5f), screenSize.Y * TopAnchor + (offset ? screenSize.Y : 0));

            float space = 0;
            float offscreen = -1;
            foreach(Element e in elements)
            {
                e.Position = new Vector2(Position.X - (e.GetSize().X / 2), Position.Y + space);
                if(e.Position.Y + (e.GetSize().Y) > screenSize.Y + (offset ? screenSize.Y : 0))
                {
                    offscreen = (e.Position.Y + (e.GetSize().Y)) - (screenSize.Y + (offset ? screenSize.Y : 0));
                }

                space += e.GetSize().Y + elementsMargin;
            }

            if(offscreen > -1)
            {
                foreach(Element e in elements)
                {
                    e.Position = e.Position - new Vector2(0, offscreen);
                }
            }
        }
    }
}
