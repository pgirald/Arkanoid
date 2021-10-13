using Arkanoid.Application.App.Components.Textures.Blocks;
using Arkanoid.Application.App.General;
using Arkanoid.Application.Utils;
using Microsoft.Xna.Framework;
using System;

namespace Arkanoid.Application.App.Components
{
    public class BlockSet : TexturesContainer
    {
        private float _currentXPosition = 0;
        private float _currentYPosition = 0;

        private string ErrorMessage => "It is not possible to change the dimensions for this class";

        public EventHandler Victory;

        private void _integrate(Block block)
        {
            ConfigBlock(block);
            Resize(block);
        }

        private void ConfigBlock(Block block)
        {
            block.Container = this;
            block.Destroyed += OnChildBlockDestroyed;
            block.Left = _currentXPosition;
            block.Left += block.MarginLeft;
            block.Top = _currentYPosition;
            block.Top += block.MarginTop;
        }

        private void Resize(Block block)
        {
            _currentXPosition = block.Right + block.MarginRight;
            if (_currentXPosition > Width)
            {
                base.Width = _currentXPosition;
            }
            if (block.Bottom + block.MarginBottom > Height)
            {
                base.Height = block.Bottom + block.MarginBottom;
            }
        }

        public void next()
        {
            _currentXPosition = 0;
            _currentYPosition = Height;
        }

        public override void AddChild(Component child)
        {
            throw new Exception("Cannot add components to this container with this method");
        }

        public void AddBlock<T>(Action<Block> initialize = null) where T : Block
        {
            Block block = TexturesFactory.GetTexture<T>().Clone<Block>();
            initialize?.Invoke(block);
            _integrate(block);
        }

        private void OnChildBlockDestroyed(object sender, EventArgs args)
        {
            Block destroyedBlock = (Block)sender;
            destroyedBlock.Destroyed -= OnChildBlockDestroyed;
            RemoveChild((Component)sender);
            if (_childs.Count == 0)
            {
                Destroyed?.Invoke(this, null);
            }
        }

        public sealed override float Width { get => base.Width; set => throw new Exception(ErrorMessage); }

        public sealed override float Height { get => base.Height; set => throw new Exception(ErrorMessage); }

        public sealed override Vector2 Size { get => base.Size; set => throw new Exception(ErrorMessage); }
    }
}
