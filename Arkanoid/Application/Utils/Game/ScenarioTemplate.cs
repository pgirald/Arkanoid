using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game.DynamicDrawing;
using Arkanoid.Application.Utils.GeneralExtensions;
using Arkanoid.Application.Utils.Textures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arkanoid.Application.Utils.Game
{
    public abstract class ScenarioTemplate : Container, IDrawable, ICollideable
    {
        public ScenarioTemplate()
        {
            AnimatedComponents = new LinkedList<Component>();
            Drawers = new LinkedList<IDrawer>();
            Draws = new LinkedList<IScenarioDraw>();
            CM = new CollisionManager();
            Utils = CreateScenarioUtils();
        }

        private ScenarioUtils Utils;

        protected virtual ScenarioUtils CreateScenarioUtils()
        {
            return new ScenarioUtils()
            {
                CM = CM,
                AnimatedComponents = AnimatedComponents,
                Scenario = this
            };
        }

        private CollisionManager CM;

        public virtual IEnumerable<TextureComponent> Textures => CurrentState.StateTextures(this);

        public virtual IEnumerable<Component> GameTextures => _childs;

        public IScenarioState CurrentState { get; set; }

        public void CheckForCollisions()
        {
            CM.LookForCollisions();
        }

        public LinkedList<Component> AnimatedComponents { get; private set; }

        protected abstract IEnumerable<Component> CreateGameItems();

        protected abstract void SetGameItems();

        protected abstract void AddAnimatedComponents();

        protected abstract void SubscribeToCM(CollisionManager CM);

        protected abstract IScenarioState GetInitialState();

        private LinkedList<IScenarioDraw> Draws;

        private LinkedList<IDrawer> Drawers;

        public void AddDrawer(IDrawer drawer)
        {
            drawer.ScenarioKey = Drawers.AddLast(drawer);
            drawer.DrawComponent += OnComponentDrawn;
        }

        public void AddDrawers(params IDrawer[] drawers)
        {
            foreach (IDrawer drawer in drawers)
            {
                AddDrawer(drawer);
            }
        }

        public void RemoveDrawer(IDrawer drawer)
        {
            Drawers.Remove(drawer.ScenarioKey);
            drawer.ScenarioKey = null;
            drawer.DrawComponent -= OnComponentDrawn;
        }

        protected virtual void OnComponentDrawn(object sender, DrawEventArgs args)
        {
            args.Draw.ScenarioKey = Draws.AddLast(args.Draw);
            args.Draw.Draw(Utils);
            ((Component)args.Draw).Destroyed += OnDrawDestroyed;
            ((Component)args.Draw).Container = this;
        }

        private void OnDrawDestroyed(object sender, EventArgs args)
        {
            IScenarioDraw draw = (IScenarioDraw)sender;
            RemoveDraw(draw);
        }

        private void RemoveDraw(IScenarioDraw draw)
        {
            draw.Erase(Utils);
            Draws.Remove(draw.ScenarioKey);
            draw.ScenarioKey = null;
            ((Component)draw).Destroyed -= OnDrawDestroyed;
            ((Component)draw).Container = null;
        }

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
            ClearDrawers();
            ClearDraws();
            AnimatedComponents.Clear();
            CM.Clear();
            base.Clear();
        }

        private void ClearDrawers()
        {
            Drawers.ForAll(drawer => RemoveDrawer(drawer.Value));
        }

        private void ClearDraws()
        {
            Draws.ForAll(draw => RemoveDraw(draw.Value));
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
