using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DumbUI.Elements
{
    public class Label : Element
    {
        SpriteFont font;
        string text;

        public Label(SpriteFont font, string text = "")
        {
            this.font = font;
            this.text = text;
        }

        internal override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.DrawString(font, text, position - (GetSize() / 2), Color.White);
        }

        internal override Vector2 GetSize()
        {
            return font.MeasureString(text);
        }
    }
}
