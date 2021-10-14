using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.Utils.Components
{
    public class Component
    {
        public Component()
        {
            _origin = new Vector2(0, 0);
            _position = new Vector2(0, 0);
        }

        private Vector2 _position;
        private Vector2 _origin;
        private Alignment _originAlignment = Alignment.TopLeft;
        private float _width = 0;
        private float _height = 0;
        private Container _container;
        private LinkedListNode<Component> _reference;
        private Vector2 _previusOrigin;
        public EventHandler Destroyed;

        protected virtual LinkedListNode<Component> _addChildSecretly(Component child)
        {
            throw new NotImplementedException();
        }

        protected virtual void _removeChildScretly(LinkedListNode<Component> reference)
        {
            throw new NotImplementedException();
        }

        public virtual Container Container
        {
            get => _container;
            set
            {
                _container?._removeChildScretly(_reference);
                _container = value;
                _reference = _container?._addChildSecretly(this);
            }
        }

        public Vector2 AbsolutePosition
        {
            get => _position;
        }

        public virtual float X
        {
            get
            {
                return _position.X - Container.AbsoluteLeft;
            }
            set
            {
                _position.X = Container.AbsoluteLeft + value;
            }
        }

        public virtual float Y
        {
            get
            {
                return _position.Y - Container.AbsoluteTop;
            }
            set
            {
                _position.Y = Container.AbsoluteTop + value;
            }
        }

        public virtual float AbsoluteX
        {
            get
            {
                return _position.X;
            }
            set
            {
                _position.X = value;
            }
        }

        public virtual float AbsoluteY
        {
            get
            {
                return _position.Y;
            }
            set
            {
                _position.Y = value;
            }
        }

        public virtual float Width
        {
            get => _width;
            set
            {
                _width = value;
                _computeOrigin(_originAlignment, ref _origin);
            }
        }

        public virtual float Height
        {
            get => _height;
            set
            {
                _height = value;
                _computeOrigin(_originAlignment, ref _origin);
            }
        }

        public virtual Vector2 Size
        {
            get => new Vector2(Width, Height);
            set
            {
                _width = value.X;
                _height = value.Y;
                _computeOrigin(_originAlignment, ref _origin);
            }
        }

        public Vector2 Origin
        {
            get => _origin;
        }

        public Alignment OriginAlignment
        {
            set
            {
                _originAlignment = value;
                _previusOrigin = _origin;
                _computeOrigin(value, ref _origin);
                _position.X += _origin.X - _previusOrigin.X;
                _position.Y += _origin.Y - _previusOrigin.Y;
            }
        }

        public float Left
        {
            get
            {
                return X - Origin.X;
            }
            set
            {
                X = value + Origin.X;
            }
        }

        public float Right
        {
            get
            {
                return X + Width - Origin.X;
            }
            set
            {
                X = value - Width + Origin.X;
            }
        }

        public float Top
        {
            get
            {
                return Y - Origin.Y;
            }
            set
            {
                Y = value + Origin.Y;
            }
        }

        public float Bottom
        {
            get
            {
                return Y + Height - Origin.Y;
            }
            set
            {
                Y = value - Height + Origin.Y;
            }
        }

        public float HorizontalCenter
        {
            get
            {
                return X + Width / 2 - Origin.X;
            }
            set
            {
                X = Origin.X + value - Width / 2;
            }
        }

        public float VerticalCenter
        {
            get
            {
                return Y + Height / 2 - Origin.Y;
            }
            set
            {
                Y = Origin.Y + value - Height / 2;
            }
        }

        public float AbsoluteLeft
        {
            get
            {
                return AbsoluteX - Origin.X;
            }
            set
            {
                AbsoluteX = value + Origin.X;
            }
        }

        public float AbsoluteRight
        {
            get
            {
                return AbsoluteX + Width - Origin.X;
            }
            set
            {
                AbsoluteX = value - Width + Origin.X;
            }
        }

        public float AbsoluteTop
        {
            get
            {
                return AbsoluteY - Origin.Y;
            }
            set
            {
                AbsoluteY = value + Origin.Y;
            }
        }

        public float AbsoluteBottom
        {
            get
            {
                return AbsoluteY + Height - Origin.Y;
            }
            set
            {
                AbsoluteY = value - Height + Origin.Y;
            }
        }

        public float AbsoluteHorizontalCenter
        {
            get
            {
                return AbsoluteX + Width / 2 - Origin.X;
            }
            set
            {
                AbsoluteX = Origin.X + value - Width / 2;
            }
        }

        public float AbsoluteVerticalCenter
        {
            get
            {
                return AbsoluteY + Height / 2 - Origin.Y;
            }
            set
            {
                AbsoluteY = Origin.Y + value - Height / 2;
            }
        }

        private void _computeOrigin(Alignment mode, ref Vector2 origin)
        {
            ComponentOps.ComputeLocation(mode, Width, Height, 0, 0, ref origin);
        }

        private void _computePosition(Alignment mode, float zeroX, float zeroY, ref Vector2 position)
        {
            ComponentOps.ComputeLocation(mode, Container.Width, Container.Height, zeroX, zeroY, ref position);
        }
    }
}
