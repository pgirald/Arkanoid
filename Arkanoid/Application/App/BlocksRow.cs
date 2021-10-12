using Arkanoid.Application.App.General;
using Arkanoid.Application.Utils;
using Microsoft.Xna.Framework;
using System;

namespace Arkanoid.Application.App
{
    public class BlocksRow : Container
    {
        public BlocksRow(Container container, float X, float Y)
        {
            Container = container;
            OwnX = X;
            OwnY = Y;
        }

        private float _currentXPosition = 0;
        private float _currentHeight = 0;

        public float MaxWidth { get; set; } = float.MaxValue;

        public override void AddChild(Component child)
        {
            throw new NotImplementedException();
        }

        public override void RemoveChild(Component child)
        {
            throw new NotImplementedException();
        }

        public bool AddBlock(float marginLeft = 0, float marginTop = 0)
        {
            generateBlock(marginLeft, marginTop);
            return !(Width > MaxWidth);
        }

        public void Clear()
        {
            foreach (Block child in _childs)
            {
                base.RemoveChild(child);
            }
            _currentXPosition = 0;
            _currentHeight = 0;
        }

        private Block generateBlock(float marginLeft, float marginTop)
        {
            Block block = BlockFactory.Block();
            block.Color = Color.Aqua;
            block.Container = this;
            block.OriginAlignment = Alignment.TopLeft;
            block.X = _currentXPosition;
            block.X += block.MarginLeft;
            block.Y = block.MarginTop;
            _currentXPosition = block.Right;
            Width = _currentXPosition;
            if (block.Bottom > _currentHeight)
            {
                _currentHeight = block.Bottom;
                Height = _currentHeight;
            }
            return block;
        }
    }
}
