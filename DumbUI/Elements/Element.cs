using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DumbUI.Elements
{
    public abstract class Element
    {
        public Vector2 Position { get; set; }

        internal abstract void Draw(SpriteBatch spriteBatch);
        internal abstract Vector2 GetSize();
    }
}
