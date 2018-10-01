using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DumbUI.Elements
{
    /// <summary>
    /// Vertical Panel. Arranges it's Elements in a column.
    /// </summary>
    public class VPanel : Panel
    {
        public VPanel(int margin = 3)
        {
            elementsMargin = margin;
        }

        // Adds the Element to it's list, and updates everything's positioning
        public override void AddElement(Element elem)
        {
            elem.Position = new Vector2(Position.X, Position.Y + Size.Y + elementsMargin);
            elements.Add(elem);

            var eSize = elem.GetSize();
            if(elements.Count == 1)
            {
                // If this is the first Element, it must be the total size
                Size = eSize;
                return;
            }

            // Add the Element Y + margin, and see if it's wider than any current Elements
            Size = new Vector2((eSize.X > Size.X ? eSize.X : Size.X), Size.Y + elementsMargin + eSize.Y);
        }

        internal override void MoveCursor(InputActions action, ref int selected, ref Panel selectedPanel)
        {
            // TODO: Adding better logic for switching Panels so it doesn't visually go from the middle of the Panel to the begin/end
            switch(action)
            {
                // If the cursor is moving up/down, move it and then check if it's past the end to switch Panels
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

                // If it's trying to move sideways, just try to switch Panels
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

        // TODO: Don't let it go off screen in more directions than just the bottom
        internal override void UpdatePositions(Vector2 screenSize, bool offset)
        {
            // Get the position of the Panel itself to base everything else off of
            Position = new Vector2((screenSize.X * LeftAnchor) - (Size.X * .5f), screenSize.Y * TopAnchor + (offset ? screenSize.Y : 0));

            // Amount to move the next Element in the cycle by
            float space = 0;
            // Did the Panel go off screen? If so, move the Panel to put it back on screen
            float offscreen = -1;
            foreach(Element e in elements)
            {
                e.Position = new Vector2(Position.X - (e.GetSize().X / 2), Position.Y + space);
                
                // Check if the Panel Elements went off screen on the bottom
                if(e.Position.Y + (e.GetSize().Y) > screenSize.Y + (offset ? screenSize.Y : 0))
                {
                    // If they did, hold how much it's off screen by so we can move it back after
                    offscreen = (e.Position.Y + (e.GetSize().Y)) - (screenSize.Y + (offset ? screenSize.Y : 0));
                }

                space += e.GetSize().Y + elementsMargin;
            }

            // If it did go off screen, move it back
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
