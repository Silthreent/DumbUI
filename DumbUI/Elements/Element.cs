using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DumbUI.Elements
{
    /// <summary>
    /// Base class all UI elements expand from.
    /// </summary>
    public abstract class Element
    {
        public Vector2 Position { get; set; }

        internal abstract void Draw(SpriteBatch spriteBatch);
        internal abstract Vector2 GetSize();
    }
}
