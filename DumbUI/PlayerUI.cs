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

        Panel selectedPanel;
        int selectedNumber;
        Element selectedElement;

        Texture2D cursorTex;

        public PlayerUI(Texture2D cursor)
        {
            Elements = new List<Panel>();

            cursorTex = cursor;
        }

        internal void SelectPanel(Panel panel)
        {
            selectedPanel = panel;
            selectedNumber = 0;
            selectedElement = selectedPanel.GetElement(0);
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            foreach(Panel x in Elements)
            {
                x.Draw(spriteBatch);
            }

            if(selectedPanel != null)
            {
                spriteBatch.Draw(cursorTex, new Rectangle(
                    (int)(selectedElement.Position.X + (selectedElement.GetSize().X / 2) - 8),
                    (int)(selectedElement.Position.Y + selectedElement.GetSize().Y),
                    16, 16),
                    Color.White);
            }
        }

        internal void UpdatePositions(Vector2 screenSize, bool vOffset, bool hSplit)
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
