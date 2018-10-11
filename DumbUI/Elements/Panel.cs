using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DumbUI.Elements
{
    /// <summary>
    /// Basic Panel object that all Panels extend from.
    /// </summary>
    public abstract class Panel
    {
        public bool Visible { get; set; } = true;
        internal Vector2 Position{ get; set; }
        protected Vector2 Size { get; set; }

        public delegate void UIEventHandler(Panel panel, int selected);
        public event UIEventHandler UIAcceptEvent;

        protected List<Element> elements = new List<Element>();
        protected int elementsMargin;

        float topAnchor;
        float leftAnchor;

        // Which Panels are connected to which sides
        Panel leftSide;
        Panel rightSide;
        Panel topSide;
        Panel bottomSide;

        // Used to add the Element to it's list, usually moving other Elements to fit
        public abstract void AddElement(Element elem);

        // Lets the Panel re-set up it's Elements based off screen size changes or anything else
        internal abstract void UpdatePositions(Vector2 screenSize, bool offset);

        // Moves the cursor, being smart about switching between Panels based on it's type
        internal abstract void MoveCursor(InputActions action, ref int selected, ref Panel selectedPanel);

        internal void Draw(SpriteBatch spriteBatch)
        {
            if(!Visible)
                return;

            foreach(Element e in elements)
            {
                e.Draw(spriteBatch);
            }
        }

        // Sends a basic event from input
        internal void CheckInput(InputActions action, int selected)
        {
            if(action == InputActions.Accept)
            {
                UIAcceptEvent?.Invoke(this, selected);
            }
        }

        internal Element GetElement(int element)
        {
            return elements[element];
        }

        internal int GetElementCount()
        {
            return elements.Count;
        }

        internal void ClearEvents()
        {
            UIAcceptEvent = null;
        }

        // All the methods used to connect Panels together
        // If the Top of Panel 1 is being connected to Panel 2, it will connect the Bottom of Panel 2 to Panel 1, etc
        #region Attachment Setting
        public void SetLeft(Panel panel)
        {
            leftSide = panel;
            panel.SetRightNoBack(this);
        }

        public void SetRight(Panel panel)
        {
            rightSide = panel;
            panel.SetLeftNoBack(this);
        }

        public void SetTop(Panel panel)
        {
            topSide = panel;
            panel.SetBottomNoBack(this);
        }

        public void SetBottom(Panel panel)
        {
            bottomSide = panel;
            panel.SetTopNoBack(this);
        }

        void SetLeftNoBack(Panel panel)
        {
            leftSide = panel;
        }

        void SetRightNoBack(Panel panel)
        {
            rightSide = panel;
        }

        void SetTopNoBack(Panel panel)
        {
            topSide = panel;
        }

        void SetBottomNoBack(Panel panel)
        {
            bottomSide = panel;
        }

        internal Panel GetLeft()
        {
            return leftSide;
        }

        internal Panel GetRight()
        {
            return rightSide;
        }

        internal Panel GetTop()
        {
            return topSide;
        }

        internal Panel GetBottom()
        {
            return bottomSide;
        }
        #endregion

        // Methods for setting the anchors, binds them between 0 and 1
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
