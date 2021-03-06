﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DumbUI.Elements
{
    /// <summary>
    /// Basic text Element
    /// </summary>
    public class Label : Element
    {
        SpriteFont font;
        string text;

        public Label(SpriteFont font, string text = "")
        {
            this.font = font;
            this.text = text;
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, Position, Color.White);
        }

        internal override Vector2 GetSize()
        {
            return font.MeasureString(text);
        }
    }
}
