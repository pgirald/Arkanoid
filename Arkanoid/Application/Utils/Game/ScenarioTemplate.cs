using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Textures;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.Utils.Game
{
    public abstract class ScenarioTemplate : Container, IDrawable, ICollideable
    {
        public ScenarioTemplate()
        {
            AnimatedComponents = new LinkedList<Component>();
            CM = new CollisionManager();
        }

        private CollisionManager CM;

        public virtual IEnumerable<TextureComponent> Textures => CurrentState.StateTextures(this);

        public virtual IEnumerable<Component> GameTextures => _childs;

        public IScenarioState CurrentState { get; set; }

        public IEnumerable<Component> GetAnimatedComponents() => AnimatedComponents;

        public void CheckForCollisions()
        {
            CM.LookForCollisions();
        }

        protected LinkedList<Component> AnimatedComponents { get; private set; }

        protected abstract IEnumerable<Component> CreateGameItems();

        protected abstract void SetGameItems();

        protected abstract void AddAnimatedComponents();

        protected abstract void SubscribeToCM(CollisionManager CM);

        protected abstract IScenarioState GetInitialState();

        public virtual void Initialize()
        {
            foreach (Component component in CreateGameItems())
            {
                component.Container = this;
            }
            SetGameItems();
            AddAnimatedComponents();
            AddChilds(AnimatedComponents);
            SubscribeToCM(CM);
            CurrentState = GetInitialState();
        }

        public LinkedListNode<ICollideable> ManagerKey { get; set; }

        public Guid Key { get; set; }

        public void Restart()
        {
            Clear();
            Initialize();
        }

        public virtual void Run(GameInfo info)
        {
            CurrentState.SetateRun(this, info);
        }

        public override void Clear()
        {
            CM.Clear();
            AnimatedComponents.Clear();
            base.Clear();
        }

        public CollisionInfo IntersectedWith(Component collideable)
        {
            if (collideable.AbsoluteRight > AbsoluteRight)
            {
                return new CollisionInfo { Side = Side.Right, CollisionSidePosition = AbsoluteRight };
            }
            if (collideable.AbsoluteTop < AbsoluteTop)
            {
                return new CollisionInfo { Side = Side.Top, CollisionSidePosition = AbsoluteTop };
            }
            if (collideable.AbsoluteLeft < AbsoluteLeft)
            {
                return new CollisionInfo { Side = Side.Left, CollisionSidePosition = AbsoluteLeft };
            }
            if (collideable.AbsoluteBottom > AbsoluteBottom)
            {
                return new CollisionInfo { Side = Side.Bottom, CollisionSidePosition = AbsoluteBottom };
            }
            return null;
        }
    }
}
