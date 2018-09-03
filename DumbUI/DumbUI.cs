using DumbUI.Elements;
using Microsoft.Xna.Framework.Graphics;

namespace DumbUI
{
    public static class DumbUIManager
    {
        static PlayerUI[] players = new PlayerUI[4];

        public static void Draw(SpriteBatch spriteBatch, bool beginSB = true)
        {
            if(beginSB)
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
            if(beginSB)
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
