﻿using DumbUI.Elements;
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
            //selectedElement = selectedPanel.GetElement(0);
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            foreach(Panel x in Elements)
            {
                x.Draw(spriteBatch);
            }

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

        internal void OnInputReceived(InputActions action)
        {
            switch(action)
            {
                case InputActions.Left:
                    selectedNumber--;
                    if(selectedNumber < 0)
                    {
                        CheckSides(true);
                    }
                    break;
                case InputActions.Right:
                    selectedNumber++;
                    if(selectedNumber >= selectedPanel.GetElementCount())
                    {
                        CheckSides(false);
                    }
                    break;

                default:
                    selectedPanel.CheckInput(action, selectedNumber);
                    break;
            }
        }

        void CheckSides(bool left)
        {
            if(left)
            {
                if(selectedPanel.GetLeft() != null)
                {
                    var panel = selectedPanel.GetLeft();
                    selectedPanel = panel;
                    selectedNumber = panel.GetElementCount() - 1;
                }
                else
                {
                    selectedNumber = selectedPanel.GetElementCount() - 1;
                }
            }
            else
            {
                if(selectedPanel.GetRight() != null)
                {
                    var panel = selectedPanel.GetRight();
                    selectedPanel = panel;
                    selectedNumber = 0;
                }
                else
                {
                    selectedNumber = 0;
                }
            }
        }
    }
}
