﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DumbUI.Elements
{
    public abstract class Panel
    {
        public Vector2 Position{ get; set; }
        protected Vector2 Size { get; set; }

        public event UIEventHandler UIAcceptEvent;
        public delegate void UIEventHandler(Panel panel, int selected);

        protected List<Element> elements = new List<Element>();
        protected int elementsMargin;

        float topAnchor;
        float leftAnchor;
        Panel leftSide;
        Panel rightSide;
        Panel topSide;
        Panel bottomSide;

        public abstract void AddElement(Element elem);
        internal abstract void UpdatePositions(Vector2 screenSize, bool offset);
        internal abstract void MoveCursor(InputActions action, ref int selected, ref Panel selectedPanel);

        internal void Draw(SpriteBatch spriteBatch)
        {
            foreach(Element e in elements)
            {
                e.Draw(spriteBatch);
            }
        }

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
