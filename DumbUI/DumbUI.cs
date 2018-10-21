using DumbUI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbUI
{
    /// <summary>
    /// Main class for interacting with the UI. Used to set up the system, and add Panels to players.
    /// </summary>
    public static class DumbUIManager
    {
        static InputEvents input = new InputEvents();
        static PlayerUI[] players = new PlayerUI[4];
        static Viewport[] viewports = new Viewport[4];

        static Viewport fullscreen;
        static Texture2D cursorTex;

        /// <summary>
        /// (WIP) Load a design from file.
        /// </summary>
        /// <param name="content">The ContentManager to load the files from.</param>
        public static void LoadDesign(ContentManager content)
        {
            cursorTex = content.Load<Texture2D>("UI/Cursor");
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
        public static void Draw(SpriteBatch spriteBatch)
        {
            if(fullscreen.Bounds.Size != spriteBatch.GraphicsDevice.Viewport.Bounds.Size)
            {
                Console.WriteLine("UPDATING POSITIONS");
                fullscreen = spriteBatch.GraphicsDevice.Viewport;

                UpdatePositions();
            }

            for(int x = 0; x <= players.Length - 1; x++)
            {
                if(players[x] != null)
                {
                    spriteBatch.GraphicsDevice.Viewport = viewports[x];

                    spriteBatch.Begin();
                    players[x].Draw(spriteBatch);
                    spriteBatch.End();
                }
            }

            spriteBatch.GraphicsDevice.Viewport = fullscreen;
        }

        /// <summary>
        /// Assign a Panel to a player, making sure the player object exists.
        /// Creates the player, and updates positions of all elements if it doesn't exist.
        /// </summary>
        /// <param name="player">Which player to assign the Panel to, 0 based.</param>
        /// <param name="panel">The Panel to assign.</param>
        /// <param name="select">Should the Panel select itself after assignment.</param>
        public static void AddPanel(int player, Panel panel, string tag, bool select = false)
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

            panel.Tag = tag;
            players[player].Panels.Add(panel);

            if(created)
                UpdatePositions();

            if(select)
                players[player].SelectPanel(panel);
        }

        public static void RemovePanel(int player, Panel panel)
        {
            if(player >= players.Length)
                return;

            if(players[player] == null)
                return;

            players[player].RemovePanel(panel);
        }

        // TODO: Some sort of null protection?
        public static Panel GetPanel(int player, string tag)
        {
            return players[player].Panels.Find(x => x.Tag == tag);
        }

        // Removes the player's UI and then updates all positions
        public static void RemovePlayer(int player)
        {
            if(player >= players.Length)
                return;

            players[player] = null;
            UpdatePositions();
        }   

        // TODO: Fix it crashing if you skip a player slot
        // Update the position of all UI elements amongst all players
        static void UpdatePositions()
        {
            var pList = players.ToList();
            pList.RemoveAll(x => x == null);

            if(pList.Count >= 2)
            {
                var v1 = fullscreen;
                v1.Height /= 2;

                var v2 = v1;
                v2.Y = v1.Height;

                if(pList.Count >= 3)
                {
                    v2.Width /= 2;

                    var v3 = v2;
                    v3.X = v3.Width;

                    if(pList.Count >= 4)
                    {
                        v1.Width /= 2;

                        var v4 = v1;
                        v4.X = v1.Width;

                        viewports[0] = v1;
                        viewports[1] = v4;
                        viewports[2] = v2;
                        viewports[3] = v3;
                    }
                    else
                    {
                        viewports[0] = v1;
                        viewports[1] = v2;
                        viewports[2] = v3;
                    }
                }
                else
                {
                    viewports[0] = v1;
                    viewports[1] = v2;
                }
            }
            else
            {
                viewports[0] = fullscreen;
            }

            for(int x = 0; x <= players.Length - 1; x++)
            {
                if(players[x] != null)
                {
                    players[x].UpdatePositions(viewports[x]);
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
