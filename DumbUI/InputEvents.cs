using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DumbUI
{
    class InputEvents
    {
        internal delegate void InputKeyHandler(int player, InputActions action);
        internal event InputKeyHandler InputKeyEvent;

        KeyboardState keyboard;
        KeyboardState lastKeyboard;

        // List containing every player's possible Keys and their actions
        Dictionary<InputActions, Keys>[] playerKeys;

        public InputEvents()
        {
            InputKeyEvent += DumbUIManager.OnInputEvent;

            // Set up the players with their actions + key bindings combinations
            playerKeys = new Dictionary<InputActions, Keys>[2]
            {
                new Dictionary<InputActions, Keys>()
                {
                    {InputActions.Left, Keys.A },
                    {InputActions.Right, Keys.D },
                    {InputActions.Up, Keys.W },
                    {InputActions.Down, Keys.S },
                    {InputActions.Accept, Keys.Space },
                },
                new Dictionary<InputActions, Keys>()
                {
                    {InputActions.Left, Keys.Left },
                    {InputActions.Right, Keys.Right },
                    {InputActions.Up, Keys.Up },
                    {InputActions.Down, Keys.Down },
                    {InputActions.Accept, Keys.Enter },
                }
            };
        }

        internal void Update()
        {
            lastKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            CheckKeys();
        }

        // Go through every player's keys and check if any were hit
        void CheckKeys()
        {
            for(int c = 0; c <= playerKeys.Length - 1; c++)
            {
                foreach(KeyValuePair<InputActions, Keys> x in playerKeys[c])
                {
                    if(lastKeyboard.IsKeyDown(x.Value) && keyboard.IsKeyUp(x.Value))
                    {
                        InputKeyEvent?.Invoke(c, x.Key);
                    }
                }
            }
        }
    }

    // Possible actions keys can be assigned to
    enum InputActions
    {
        Left,
        Right,
        Up,
        Down,
        Accept,
        Back
    }
}
