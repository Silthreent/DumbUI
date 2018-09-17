using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DumbUI.Elements
{
    public abstract class Panel
    {
        public Vector2 Position{ get; set; }
        protected Vector2 Size { get; set; }

        protected List<Element> elements = new List<Element>();
        protected int elementsMargin;
        protected Texture2D debugTex;

        float topAnchor;
        float leftAnchor;

        public abstract void AddElement(Element elem);
        internal abstract void UpdatePositions(Vector2 screenSize, bool offset);

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(debugTex, new Rectangle((int)Position.X - 8, (int)Position.Y - 8, 16, 16), Color.White);
            
            foreach(Element e in elements)
            {
                e.Draw(spriteBatch);
            }
        }

        internal Element GetElement(int element)
        {
            return elements[element];
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
