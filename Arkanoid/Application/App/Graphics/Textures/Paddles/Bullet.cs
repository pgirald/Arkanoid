using Arkanoid.Application.App.Game.Scenarios;
using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.Game.DynamicDrawing;
using System.Collections.Generic;

namespace Arkanoid.Application.App.Graphics.Textures.Paddles
{
    public class Bullet : AnimatedTexture, IScenarioDraw
    {
        public Bullet()
        {
            Speed = 400f;
        }

        private LinkedListNode<Component> Node;

        public override string ParentPath => Paddle.PaddleParentPath;

        public override string TexturePath => "Bullet";

        public LinkedListNode<IScenarioDraw> ScenarioKey { get; set; }

        public void Draw(ScenarioUtils utils)
        {
            ArkanoidScenarioUtils arkUtils = (ArkanoidScenarioUtils)utils;
            Node = arkUtils.AnimatedComponents.AddLast(this);
            arkUtils.CM.Subscribe(this);
            arkUtils.CM.SubscribeToComponents(this, arkUtils.Blocks, arkUtils.Scenario);
        }

        public void Erase(ScenarioUtils utils)
        {
            ArkanoidScenarioUtils arkUtils = (ArkanoidScenarioUtils)utils;
            arkUtils.AnimatedComponents.Remove(Node);
            Node = null;
            arkUtils.CM.Unsuscribe(this);
        }

        public override void Move(GameInfo info)
        {
            AbsoluteY -= info.ComputedSpeed;
        }

        public override void OnCollision(Component collideable, CollisionInfo info)
        {
            Destroyed?.Invoke(this, null);
        }
    }
}
