using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Components;
using System.Collections.Generic;

namespace Arkanoid.Application.Utils.Game.DynamicDrawing
{
    public class ScenarioUtils
    {
        public CollisionManager CM { get; set; }

        public LinkedList<Component> AnimatedComponents { get; set; }

        public ScenarioTemplate Scenario { get; set; }
    }
}
