using Arkanoid.Application.App.Graphics;
using Arkanoid.Application.App.Graphics.CollisionBehaviours;
using Arkanoid.Application.App.Graphics.Textures.Blocks;
using Arkanoid.Application.App.Graphics.Textures.Paddles;
using Arkanoid.Application.App.Graphics.Textures.Projectiles;
using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public class ArkanoidScenario : ScenarioTemplate
    {
        private Paddle paddle;
        private Projectile projectile;
        private BlockSet blocks;

        protected override void AddAnimatedComponents()
        {
            AnimatedComponents.AddLast(paddle);
            AnimatedComponents.AddLast(projectile);
        }

        protected override IEnumerable<Component> CreateGameItems()
        {
            paddle = TexturesFactory.GetTextureClone<Paddle>();
            projectile = TexturesFactory.GetTextureClone<Projectile>();
            blocks = CreateSimpleBlocks();
            yield return paddle;
            yield return projectile;
            yield return blocks;
        }

        protected override void SetGameItems()
        {
            projectile.Destroyed += OnProjectileDestroyed;
            blocks.Destroyed += OnBlocksDestroyed;
            paddle.Align(Alignment.BottomCenter);
            blocks.Align(Alignment.TopCenter);
            projectile.PutOn(paddle, Alignment.TopCenter);
            projectile.AbsoluteBottom -= 0.1f;
            paddle.Projectile = projectile;
        }

        protected override void SubscribeToCM(CollisionManager CM)
        {
            CM.SubscribeVarious(projectile, paddle, blocks, this);
            CM.SubscribeToComponents(projectile, paddle, blocks, this);
            CM.SubscribeToComponent(paddle, this);
            CM.AddSpecial(projectile, paddle, new BounceUpBehaviour(projectile));
            CM.AddSpecial(projectile, this, new KeepInsideBoundsBehaviour(projectile));
        }

        private void OnProjectileDestroyed(object sender, EventArgs args)
        {
            projectile.Destroyed -= OnProjectileDestroyed;
            CurrentState = new GameOver(this);
        }

        private void OnBlocksDestroyed(object sender, EventArgs args)
        {
            blocks.Destroyed -= OnBlocksDestroyed;
            CurrentState = new GameFinished(this);
            Clear();
        }

        private BlockSet CreateSimpleBlocks()
        {
            BlockSet blocks = new BlockSet();
            for (int i = 5; i > 0; i--)
            {
                for (int j = 5; j > 0; j--)
                {
                    blocks.AddBlock<Block>(block => block.Color = Color.LightCoral);
                }
                blocks.next();
            }
            return blocks;
        }

        protected override IScenarioState GetInitialState()
        {
            return new Running();
        }
    }
}