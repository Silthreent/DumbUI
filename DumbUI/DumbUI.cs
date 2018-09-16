using DumbUI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DumbUI
{
    public static class DumbUIManager
    {
        static PlayerUI[] players = new PlayerUI[2];

        static Vector2 screenSize;

        public static void Draw(SpriteBatch spriteBatch, bool beginBatch = true)
        {
            var screen = new Vector2(spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height);

            if(screenSize != screen)
            {
                Console.WriteLine("UPDATING");
                screenSize = screen;

                UpdatePositions();
            }

            if(beginBatch)
                spriteBatch.Begin();

            {
                for(int x = 0; x <= players.Length - 1; x++)
                {
                    if(players[x] != null)
                    {
                        players[x].Draw(spriteBatch);
                    }
                }
            }

            if(beginBatch)
                spriteBatch.End();
        }

        public static void AddPanel(int player, Panel panel)
        {
            if(player >= players.Length)
                return;

            bool created = false;
            if(players[player] == null)
            {
                Console.WriteLine("ADDED PLAYER " + player);
                players[player] = new PlayerUI();
                created = true;
            }

            players[player].Elements.Add(panel);

            if(created)
                UpdatePositions();
        }

        static void UpdatePositions()
        {
            bool vOffset = false;
            bool split = players[0] != null && players[1] != null;

            for(int x = 0; x <= players.Length - 1; x++)
            {
                if(players[x] != null)
                {
                    players[x].UpdatePositions(screenSize, vOffset, split);
                    vOffset = !vOffset;
                }
            }
        }
    }
}
