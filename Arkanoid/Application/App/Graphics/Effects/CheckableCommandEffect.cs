using Arkanoid.Application.Utils.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arkanoid.Application.App.Graphics.Effects
{
    public abstract class CheckableCommandEffect: CommandEffect
    {
        protected CheckableCommandEffect(ArkanoidScenario scenario) : base(scenario)
        {
        }

        public abstract void Check(GameInfo info);
    }
}
