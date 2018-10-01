using Microsoft.Xna.Framework;

namespace DumbUI.Elements
{
    /// <summary>
    /// Horizontal Panel. Arranges it's Elements in a row, centered.
    /// </summary>
    public class HPanel : Panel
    {
        public HPanel(int margin = 10)
        {
            elementsMargin = margin;
        }

        // Adds the Element to it's list, and updates everything's positioning
        public override void AddElement(Element elem)
        {
            elem.Position = new Vector2(Position.X + Size.X + elementsMargin, Position.Y);
            elements.Add(elem);

            var eSize = elem.GetSize();
            if(elements.Count == 1)
            {
                // If this is the first Element, it must be the total size
                Size = eSize;
                return;
            }

            // Add the Element X + margin, and see if it's taller than any current Elements
            Size = new Vector2(Size.X + elementsMargin + eSize.X, (eSize.Y > Size.Y ? eSize.Y : Size.Y));
        }

        internal override void MoveCursor(InputActions action, ref int selected, ref Panel selectedPanel)
        {
            // TODO: Adding better logic for switching Panels so it doesn't visually go from the middle of the Panel to the begin/end
            switch(action)
            {
                // If the cursor is moving sideways, move it and then check if it's past the end to switch Panels
                case (InputActions.Left):
                    selected--;
                    if(selected < 0)
                    {
                        if(GetLeft() != null)
                        {
                            selectedPanel = GetLeft();
                        }

                        selected = selectedPanel.GetElementCount() - 1;
                    }
                    break;
                case (InputActions.Right):
                    selected++;
                    if(selected >= GetElementCount())
                    {
                        if(GetRight() != null)
                        {
                            selectedPanel = GetRight();
                        }

                        selected = 0;
                    }
                    break;

                // If it's trying to move up/down, just try to switch Panels
                case (InputActions.Up):
                    if(GetTop() != null)
                    {
                        selectedPanel = GetTop();
                        selected = 0;
                    }
                    break;
                case (InputActions.Down):
                    if(GetBottom() != null)
                    {
                        selectedPanel = GetBottom();
                        selected = 0;
                    }
                    break;
            }
        }

        // TODO: Add what VPanel has and don't let it go out of it's screen region
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
