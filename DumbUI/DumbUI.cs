using DumbUI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DumbUI
{
    /// <summary>
    /// Main class for interacting with the UI. Used to set up the system, and add Panels to players.
    /// </summary>
    public static class DumbUIManager
    {
        static InputEvents input = new InputEvents();
        static PlayerUI[] players = new PlayerUI[2];

        static Vector2 screenSize;
        static Texture2D cursorTex;

        /// <summary>
        /// (WIP) Load a design from file.
        /// </summary>
        /// <param name="content">The ContentManager to load the files from.</param>
        public static void LoadDesign(ContentManager content)
        {
            cursorTex = content.Load<Texture2D>("Debug");
        }

        /// <summary>
        /// Update any UI elements that need it, and poll for input.
        /// </summary>
        /// <param name="delta"></param>
        public static void Update(GameTime delta)
        {
            input.Update();
        }

        /// <summary>
        /// Draw the UI elements on screen. If the screen has changed size, will also update all UI element positions.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to.</param>
        /// <param name="beginBatch">Should it begin the batch? Leave default unless you set special draw parameters.</param>
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

        /// <summary>
        /// Assign a Panel to a player, making sure the player object exists.
        /// Creates the player, and updates positions of all elements if it doesn't exist.
        /// </summary>
        /// <param name="player">Which player to assign the Panel to, 0 based.</param>
        /// <param name="panel">The Panel to assign.</param>
        /// <param name="select">Should the Panel select itself after assignment.</param>
        public static void AddPanel(int player, Panel panel, bool select = false)
        {
            if(player >= players.Length)
                return;

            bool created = false;
            if(players[player] == null)
            {
                Console.WriteLine("ADDED PLAYER " + player);
                players[player] = new PlayerUI(cursorTex);
                created = true;
            }

            players[player].Panels.Add(panel);

            if(created)
                UpdatePositions();

            if(select)
                players[player].SelectPanel(panel);
        }

        // Removes the player's UI and then updates all positions
        public static void RemovePlayer(int player)
        {
            if(player >= players.Length)
                return;

            players[player] = null;
            UpdatePositions();
        }   

        // Update the position of all UI elements amongst all players
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

        // Send any input events received to the correct player
        internal static void OnInputEvent(int player, InputActions action)
        {
            Console.WriteLine("Player: " + player + " used " + action);
            players[player].OnInputReceived(action);
        }
    }
}
