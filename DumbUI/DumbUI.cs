using DumbUI.Elements;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DumbUI
{
    public static class DumbUIManager
    {
        static PlayerUI[] players = new PlayerUI[2];

        public static void Draw(SpriteBatch spriteBatch, bool beginBatch = true)
        {
            if(beginBatch)
                spriteBatch.Begin();

            {
                /*int pcount = 0;
                foreach(PlayerUI x in players)
                {
                    if(x != null)
                    {
                        pcount++;
                    }
                }*/

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
            if(players[player] == null)
            {
                players[player] = new PlayerUI();
            }

            players[player].Elements.Add(element);
        }
    }
}
