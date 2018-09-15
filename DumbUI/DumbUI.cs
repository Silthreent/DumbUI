using DumbUI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DumbUI
{
    public static class DumbUIManager
    {
        static Vector2 screenSize;
        
        static PlayerUI[] players = new PlayerUI[2];

        public static void Draw(SpriteBatch spriteBatch, bool beginBatch = true)
        {
            var screen = new Vector2(spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height);

            if(screenSize != screen)
            {
                Console.WriteLine("UPDATING");
                screenSize = screen;

                // TODO: Update UI positions
            }

            if(beginBatch)
                spriteBatch.Begin();

            {
                bool vOffset = false;
                bool split = players[0] != null && players[1] != null;

                for(int x = 0; x <= players.Length - 1; x++)
                {
                    if(players[x] != null)
                    {
                        players[x].Draw(spriteBatch, vOffset, split);
                        vOffset = !vOffset;
                    }
                }
            }

            if(beginBatch)
                spriteBatch.End();
        }

        public static void AddElement(int player, Element element)
        {
            if(player >= players.Length)
                return;

            if(players[player] == null)
            {
                players[player] = new PlayerUI();
            }

            players[player].Elements.Add(element);
        }
    }
}
