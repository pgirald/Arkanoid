using Arkanoid.Application.App.Graphics.Effects;
using Arkanoid.Application.App.Graphics.Textures.Blocks;
using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game.DynamicDrawing;
using Arkanoid.Application.Utils.Textures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.App.Graphics
{
    public class BlockSet : TexturesContainer, ICollideable, IDrawer
    {
        private float _currentXPosition = 0;
        private float _currentYPosition = 0;

        private string ErrorMessage => "It is not possible to change the dimensions for this class";

        private void _integrate(Block block)
        {
            ConfigBlock(block);
            Resize(block);
        }

        private void ConfigBlock(Block block)
        {
            block.Container = this;
            block.Destroyed += OnChildBlockDestroyed;
            block.ItemDropped += OnItemDropped;
            block.Left = _currentXPosition;
            block.Left += block.MarginLeft;
            block.Top = _currentYPosition;
            block.Top += block.MarginTop;
        }

        private void OnItemDropped(object sender, ItemDroppedEventArgs args)
        {
            DrawComponent?.Invoke(this, new DrawEventArgs { Draw = args.Effect });
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
            destroyedBlock.ItemDropped -= OnItemDropped;
            RemoveChild((Component)sender);
            if (_childs.Count == 0)
            {
                Destroyed?.Invoke(this, null);
            }
        }

        public CollisionInfo IntersectedWith(Component collideable)
        {
            if (!CollisionOps.AreIntersected(this, collideable))
            {
                return null;
            }
            CollisionInfo info = null;
            foreach (Block child in _childs)
            {
                if (CollisionOps.AreIntersected(collideable, child))
                {
                    child.Hit();
                    info = CollisionOps.GetCollisionInfo(collideable, child);
                    break;
                }
            }
            return info;
        }

        public sealed override float Width { get => base.Width; set => throw new Exception(ErrorMessage); }

        public sealed override float Height { get => base.Height; set => throw new Exception(ErrorMessage); }

        public sealed override Vector2 Size { get => base.Size; set => throw new Exception(ErrorMessage); }
        
        public LinkedListNode<CollideableInfo> CMKey { get; set; }

        public LinkedListNode<IDrawer> ScenarioKey { get; set; }

        public EventHandler<DrawEventArgs> DrawComponent { get; set; }
    }
}
