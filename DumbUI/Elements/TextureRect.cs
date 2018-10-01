using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DumbUI.Elements
{
    public class TextureRect : Element
    {
        Texture2D texture;
        Point size;

        public TextureRect(Texture2D tex, int width, int height)
        {
            SetTexture(tex);
            SetSize(width, height);
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(Position.ToPoint(), size), Color.White);
        }

        internal override Vector2 GetSize()
        {
            return new Vector2(size.X, size.Y);
        }

        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public void SetSize(int width, int height)
        {
            size = new Point(width, height);
        }
    }
}
