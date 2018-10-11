using DumbUI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DumbUI
{
    class PlayerUI
    {
        internal List<Panel> Panels { get; private set; }

        Panel selectedPanel;
        int selectedNumber;

        Texture2D cursorTex;

        public PlayerUI(Texture2D cursor)
        {
            Panels = new List<Panel>();

            cursorTex = cursor;
        }

        // Chooses which Panel to set as selected
        internal void SelectPanel(Panel panel)
        {
            selectedPanel = panel;
            selectedNumber = 0;
            //selectedElement = selectedPanel.GetElement(0);
        }

        // Draw all Panels this player is holding and the cursor
        internal void Draw(SpriteBatch spriteBatch)
        {
            foreach(Panel x in Panels)
            {
                x.Draw(spriteBatch);
            }

            // Draws the cursor if anything is selected
            if(selectedPanel != null)
            {
                var ele = selectedPanel.GetElement(selectedNumber);
                spriteBatch.Draw(cursorTex, new Rectangle(
                    (int)(ele.Position.X + (ele.GetSize().X / 2) - 8),
                    (int)(ele.Position.Y + ele.GetSize().Y),
                    16, 16),
                    Color.White);
            }
        }

        // Tell all Panels to update their and their element's positions
        internal void UpdatePositions(Vector2 screenSize, bool vOffset, bool hSplit)
        {
            // If in split screen mode, need to cut it's screen region in half
            if(hSplit)
            {
                screenSize.Y /= 2;
            }

            Console.WriteLine(vOffset + ": " + screenSize);

            foreach(Panel x in Panels)
            {
                x.UpdatePositions(screenSize, vOffset);
            }
        }

        // On receiving input, send it properly to the correct Panel
        internal void OnInputReceived(InputActions action)
        {
            switch(action)
            {
                // If it was any movement direction, move the cursor
                case InputActions.Left:
                    selectedPanel.MoveCursor(action, ref selectedNumber, ref selectedPanel);
                    break;
                case InputActions.Right:
                    selectedPanel.MoveCursor(action, ref selectedNumber, ref selectedPanel);
                    break;
                case InputActions.Up:
                    selectedPanel.MoveCursor(action, ref selectedNumber, ref selectedPanel);
                    break;
                case InputActions.Down:
                    selectedPanel.MoveCursor(action, ref selectedNumber, ref selectedPanel);
                    break;

                // Otherwise, just let the Panel handle it themself
                default:
                    selectedPanel.CheckInput(action, selectedNumber);
                    break;
            }
        }
    }
}
