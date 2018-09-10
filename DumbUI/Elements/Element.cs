using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DumbUI.Elements
{
    public abstract class Element
    {
        float topAnchor;
        float leftAnchor;

        internal abstract void Draw(SpriteBatch spriteBatch, Vector2 position);

        internal abstract Vector2 GetSize();

        public float TopAnchor
        {
            get
            {
                return topAnchor;
            }
            set
            {
                topAnchor = LockValue(value);
            }
        }

        public float LeftAnchor
        {
            get
            {
                return leftAnchor;
            }
            set
            {
                leftAnchor = LockValue(value);
            }
        }

        float LockValue(float value)
        {
            return Math.Min(Math.Max(value, 0), 1);
        }
    }
}
