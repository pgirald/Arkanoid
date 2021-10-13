using Arkanoid.Application.App.Components;
using Arkanoid.Application.App.Components.Textures.Paddles;
using Arkanoid.Application.App.Components.Textures.Projectiles;
using Arkanoid.Application.Utils;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public abstract class ScenarioTemplate : Container, ITexturesContainer
    {
        public ScenarioTemplate()
        {
            MovingComponents = new LinkedList<CollideableComponent>();
            Initialize();
        }

        private AppCollisionManager CM;

        public IEnumerable<TextureComponent> Textures => CurrentState.StateTextures(this);

        public IEnumerable<TextureComponent> GameTextures => GameItems;

        public IScenarioState CurrentState { get; set; }

        public BlockSet Blocks { get; protected set; }

        public Paddle Paddle { get; protected set; }

        public Projectile Projectile { get; protected set; }

        public IEnumerable<CollideableComponent> GetMovingComponents() => MovingComponents;

        public void CheckForCollisions()
        {
            CM.CheckScenarioCollisions();
        }

        protected LinkedList<CollideableComponent> MovingComponents { get; set; }

        protected abstract void SetGameItems();

        public virtual void Initialize()
        {
            SetGameItems();
            CM = new AppCollisionManager(this);
            MovingComponents.AddLast(Paddle);
            MovingComponents.AddLast(Projectile);
            AddChilds(MovingComponents);
            Blocks.Container = this;
            Paddle.Container = this;
            Projectile.Container = this;
            Blocks.Destroyed += OnBlocksDestroyed;
            Projectile.Destroyed += OnProjectileDestroyed;
            CM.Subscribe(Paddle, Subscription.Container);
            CM.Subscribe(Projectile, Subscription.Container);
            CM.Subscribe(Projectile, Subscription.Paddle);
            CM.Subscribe(Projectile, Subscription.Block);
            CM.Subscribe(Projectile, Subscription.MovingComps);
            CurrentState = new Running();
        }

        protected virtual IEnumerable<TextureComponent> GameItems
        {
            get
            {
                yield return Paddle;
                yield return Projectile;
                foreach (TextureComponent texture in Blocks.Textures)
                {
                    yield return texture;
                }
                foreach (TextureComponent texture in MovingComponents)
                {
                    yield return texture;
                }
            }
        }

        private void OnProjectileDestroyed(object sender, EventArgs args)
        {
            Projectile.Destroyed -= OnProjectileDestroyed;
            CurrentState = new GameOver(this);
        }

        private void OnBlocksDestroyed(object sender, EventArgs args)
        {
            Blocks.Destroyed -= OnBlocksDestroyed;
            CurrentState = new GameFinished(this);
            Clear();
        }

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
            Blocks.Clear();
            MovingComponents.Clear();
            Paddle = null;
            Projectile = null;
            base.Clear();
        }
    }
}
