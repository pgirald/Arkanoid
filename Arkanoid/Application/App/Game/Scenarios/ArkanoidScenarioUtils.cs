using Arkanoid.Application.App.Graphics;
using Arkanoid.Application.App.Graphics.Textures.Paddles;
using Arkanoid.Application.App.Graphics.Textures.Projectiles;
using Arkanoid.Application.Utils.Game.DynamicDrawing;

namespace Arkanoid.Application.App.Game.Scenarios
{
    public class ArkanoidScenarioUtils : ScenarioUtils
    {
        private ArkanoidScenario _arkScenario;

        public ArkanoidScenarioUtils(ArkanoidScenario scenario, ScenarioUtils utils)
        {
            _arkScenario = scenario;
            CM = utils.CM;
            AnimatedComponents = utils.AnimatedComponents;
            Scenario = scenario;
        }

        public Paddle Paddle => _arkScenario.paddle;

        public BlockSet Blocks => _arkScenario.blocks;

        public Projectile Projectile => _arkScenario.projectile;
    }
}
