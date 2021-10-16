using Arkanoid.Application.App.Graphics.Textures.Paddles;

namespace Arkanoid.Application.App.Graphics.Effects.Concrete
{
    public class GunPaddleEffect : TimeEffectCommand
    {
        private GunPaddle _gunPaddle;

        public GunPaddleEffect(ArkanoidScenario scenario) : base(scenario)
        {
            AddIncompatibleEffect<LongPaddleEffect>();
        }

        public override float SecondsLimit => 5;

        protected override void ChangeScenario()
        {
            _gunPaddle = TexturesFactory.GetTextureClone<GunPaddle>();
            _scenario.AddDrawer(_gunPaddle);
            _scenario.SetPaddle(_gunPaddle);
        }

        protected override void RecoverScenario()
        {
            Paddle paddle = TexturesFactory.GetTextureClone<Paddle>();
            _scenario.RemoveDrawer(_gunPaddle);
            _scenario.SetPaddle(paddle);
        }
    }
}
