using Arkanoid.Application.App.Game.Scenarios;
using Arkanoid.Application.App.Graphics;
using Arkanoid.Application.App.Graphics.CollisionBehaviours;
using Arkanoid.Application.App.Graphics.Effects;
using Arkanoid.Application.App.Graphics.Textures.Blocks;
using Arkanoid.Application.App.Graphics.Textures.Paddles;
using Arkanoid.Application.App.Graphics.Textures.Projectiles;
using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.Game.DynamicDrawing;
using Arkanoid.Application.Utils.GeneralExtensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public class ArkanoidScenario : ScenarioTemplate
    {
        public ArkanoidScenario()
        {
            ApplyiedEffects = new LinkedList<KeyValuePair<string, EffectCommand>>();
            ApplyiedCheckableEffects = new LinkedList<CheckableEffectCommand>();
        }

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
            Blocks = CreateSimpleBlocks();
            yield return Paddle;
            yield return Projectile;
            yield return Blocks;
        }

        protected override void SetGameItems()
        {
            Projectile.Destroyed += OnProjectileDestroyed;
            Blocks.Destroyed += OnBlocksDestroyed;
            Paddle.Align(Alignment.BottomCenter);
            Blocks.Align(Alignment.TopCenter);
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
            CurrentState = new GameFinished(this);
            Clear();
        }

        private BlockSet CreateSimpleBlocks()
        {
            BlockSet blocks = new BlockSet();
            for (int i = 6; i > 0; i--)
            {
                for (int j = 5; j > 0; j--)
                {
                    blocks.AddBlock<Block>(block => block.Color = Color.LightCoral);
                }
                blocks.next();
            }
            return blocks;
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
            return new Running();
        }
    }
}