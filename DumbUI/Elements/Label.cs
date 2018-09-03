using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DumbUI.Elements
{
    public class Label : Element
    {
        SpriteFont font;
        string text;

        public Label(SpriteFont font, string text = "", float topA = 0, float leftA = 0) : base(topA, leftA)
        {
            this.font = font;
            this.text = text;
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, new Vector2(50, 50), Color.White);
        }
    }
}
