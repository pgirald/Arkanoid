using Arkanoid.Application.App.Game.Scenarios;
using Arkanoid.Application.App.Graphics.CollisionBehaviours;
using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.Game.DynamicDrawing;
using System.Collections.Generic;

namespace Arkanoid.Application.App.Graphics.Effects
{
    public abstract class EffectItem : AnimatedTexture, IScenarioDraw
    {
        public override string ParentPath => "Effects/";

        public EffectItem()
        {
            Speed = 200f;
        }

        public EffectCommand Effect { get; private set; }

        protected abstract EffectCommand CreateEffect(ArkanoidScenarioUtils utils);

        public LinkedListNode<IScenarioDraw> ScenarioKey { get; set; }

        public void Draw(ScenarioUtils utils)
        {
            ArkanoidScenarioUtils arkUtils = (ArkanoidScenarioUtils)utils;
            Effect = CreateEffect(arkUtils);
            arkUtils.Scenario.AddAnimated(this);
            utils.CM.Subscribe(this);
            utils.CM.SubscribeToComponents(this, arkUtils.Paddle, arkUtils.Scenario);
            utils.CM.AddSpecial(this, arkUtils.Paddle, new ApplyEffectBehaviour(this));
        }

        public void Erase(ScenarioUtils utils)
        {
            ArkanoidScenario scenario = (ArkanoidScenario)utils.Scenario;
            scenario.RemoveAnimated(this);
            utils.CM.Unsuscribe(this);
        }

        public override void Move(GameInfo info)
        {
            AbsoluteY += info.ComputedSpeed;
        }

        public override void OnCollision(Component collideable, CollisionInfo info)
        {
            Destroyed?.Invoke(this, null);
        }
    }
}
