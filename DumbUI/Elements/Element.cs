using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DumbUI.Elements
{
    public abstract class Element
    {
        Vector2 position;

        float topAnchor;
        float leftAnchor;

        internal abstract void Draw(SpriteBatch spriteBatch, Vector2 position);

        internal abstract Vector2 GetSize();

        public void UpdatePosition(Vector2 screenRegion)
        {
            position = new Vector2(screenRegion.X * leftAnchor, screenRegion.Y * topAnchor);
        }

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
