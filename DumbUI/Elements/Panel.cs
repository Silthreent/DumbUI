using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DumbUI.Elements
{
    public abstract class Panel : Element
    {
        protected List<Element> elements;
        protected Vector2 size;
        protected int margin;

        internal override Vector2 GetSize()
        {
            return size;
        }
    }
}
