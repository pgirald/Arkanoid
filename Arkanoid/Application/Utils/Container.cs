using System;
using System.Collections.Generic;

namespace Arkanoid.Application.Utils
{
    public class Container : Component
    {
        public Container()
        {//Initially setup the position to avoid move each child
            //you can also change the _position property to be protected
            _childs = new LinkedList<Component>();
        }

        protected LinkedList<Component> _childs;

        public IEnumerable<Component> Childs => _childs;

        protected override LinkedListNode<Component> _addChildSecretly(Component child)
        {
            LinkedListNode<Component> reference = new LinkedListNode<Component>(child);
            _childs.AddLast(child);
            return reference;
        }

        protected override void _removeChildScretly(LinkedListNode<Component> reference)
        {
            _childs.Remove(reference);
        }

        public virtual void AddChild(Component child)
        {
            child.Container = this;
        }

        public virtual void RemoveChild(Component child)
        {
            if (child.Container != this)
            {
                throw new Exception("The specified child doesn't belong to this container");
            }
            child.Container = null;
        }

        public override float X
        {
            get => base.X;
            set
            {
                _updateChildsX(value - X);
                base.X = value;
            }
        }

        public override float Y
        {
            get => base.Y;
            set
            {
                _updateChildsY(value - Y);
                base.Y = value;
            }
        }
        protected float OwnX
        {
            set
            {
                base.X = value;
            }
        }

        protected float OwnY
        {
            set
            {
                base.Y = value;
            }
        }

        public override float AbsoluteX
        {
            get => base.AbsoluteX;
            set
            {

                _updateChildsX(value - AbsoluteX);
                base.AbsoluteX = value;
            }
        }

        public override float AbsoluteY
        {
            get => base.AbsoluteY;
            set
            {
                _updateChildsY(value - AbsoluteY);
                base.AbsoluteY = value;
            }
        }

        private void _updateChildsX(float diff)
        {
            foreach (Component child in Childs)
            {
                child.AbsoluteX += diff;
            }
        }

        private void _updateChildsY(float diff)
        {
            foreach (Component child in Childs)
            {
                child.AbsoluteY += diff;
            }
        }
    }
}
