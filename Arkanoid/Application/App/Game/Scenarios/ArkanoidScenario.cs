using Arkanoid.Application.App.Game.Scenarios;
using Arkanoid.Application.App.Game.Scenarios.ScenarioStates;
using Arkanoid.Application.App.Graphics;
using Arkanoid.Application.App.Graphics.CollisionBehaviours;
using Arkanoid.Application.App.Graphics.Effects;
using Arkanoid.Application.App.Graphics.Textures.Paddles;
using Arkanoid.Application.App.Graphics.Textures.Projectiles;
using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.Game.DynamicDrawing;
using Arkanoid.Application.Utils.GeneralExtensions;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public class ArkanoidScenario : ScenarioTemplate
    {
        public ArkanoidScenario(BlockSet blocks, float width, float height)
        {
            Width = width;
            Height = height;
            ApplyiedEffects = new LinkedList<KeyValuePair<string, EffectCommand>>();
            ApplyiedCheckableEffects = new LinkedList<CheckableEffectCommand>();
            Blocks = blocks;
            InitialState = new Running();
            FinalState = new NextStage(this);
        }

        public IScenarioState InitialState { get; set; }

        public IScenarioState FinalState { get; set; }

        public LinkedList<KeyValuePair<string, EffectCommand>> ApplyiedEffects { get; private set; }

        public LinkedList<CheckableEffectCommand> ApplyiedCheckableEffects { get; private set; }

        public Paddle Paddle { get; private set; }

        public Projectile Projectile { get; private set; }

        public BlockSet Blocks { get; private set; }

        public void SetPaddle(Paddle newPaddle)
        {
            PassData(Paddle, newPaddle);
            newPaddle.Projectile = Projectile;
            Paddle.LeaveProjectile();
            Paddle = newPaddle;
        }

        public void SetProjectile(Projectile newProjectile)
        {
            newProjectile.SetDirections(Projectile);
            newProjectile.Resting = Projectile.Resting;
            PassData(Projectile, newProjectile);
            Projectile.Destroyed -= OnProjectileDestroyed;
            newProjectile.Destroyed += OnProjectileDestroyed;
            Paddle.Projectile = newProjectile;
            Projectile = newProjectile;
        }

        private void PassData(Component oldComp, Component newComp)
        {
            oldComp.Container = null;
            newComp.Container = this;
            newComp.AbsoluteX = oldComp.AbsoluteX;
            newComp.AbsoluteY = oldComp.AbsoluteY;
            RemoveAnimated((IAnimated)oldComp);
            AddAnimated((IAnimated)newComp);
            ((IAnimated)newComp).Speed = ((IAnimated)oldComp).Speed;
            CM.Replace((ICollideable)oldComp, (ICollideable)newComp);
        }

        protected override void AddAnimatedComponents()
        {
            AddAnimated(Paddle);
            AddAnimated(Projectile);
        }

        protected override ScenarioUtils CreateScenarioUtils()
        {
            return new ArkanoidScenarioUtils(base.CreateScenarioUtils());
        }

        protected override IEnumerable<Component> CreateGameItems()
        {
            Paddle = TexturesFactory.GetTextureClone<Paddle>();
            Projectile = TexturesFactory.GetTextureClone<Projectile>();
            yield return Paddle;
            yield return Projectile;
            yield return Blocks;
        }

        protected override void SetGameItems()
        {
            Projectile.Destroyed += OnProjectileDestroyed;
            Blocks.Destroyed += OnBlocksDestroyed;
            Paddle.Align(Alignment.BottomCenter);
            //Blocks.Align(Alignment.TopCenter);
            Projectile.PutOn(Paddle, Alignment.TopCenter);
            Projectile.AbsoluteBottom -= 0.1f;
            Paddle.Projectile = Projectile;
            AddDrawer(Blocks);
        }

        protected override void SubscribeToCM(CollisionManager CM)
        {
            CM.SubscribeVarious(Projectile, Paddle, Blocks, this);
            CM.SubscribeToComponents(Projectile, Paddle, Blocks, this);
            CM.SubscribeToComponent(Paddle, this);
            CM.AddSpecial(Projectile, Paddle, new BounceUpBehaviour(Projectile));
            CM.AddSpecial(Projectile, this, new KeepInsideBoundsBehaviour(Projectile));
        }

        private void OnProjectileDestroyed(object sender, EventArgs args)
        {
            CurrentState = new GameOver(this);
        }

        private void OnBlocksDestroyed(object sender, EventArgs args)
        {
            CurrentState = FinalState;
            Clear();
        }

        public override void Clear()
        {
            ApplyiedEffects.ForAll(effect => effect.Value.Value.Disable());
            ApplyiedCheckableEffects.Clear();
            base.Clear();
            Blocks.Destroyed -= OnBlocksDestroyed;
            Projectile.Destroyed -= OnProjectileDestroyed;
        }

        protected override IScenarioState GetInitialState()
        {
            return InitialState;
        }
    }
}