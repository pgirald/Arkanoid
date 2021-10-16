using Arkanoid.Application.App.Graphics.Textures.Paddles;

namespace Arkanoid.Application.App.Graphics.Effects.Concrete
{
    public class LongPaddleEffect : TimeEffectCommand
    {
        public LongPaddleEffect(ArkanoidScenario scenario) : base(scenario)
        {
            AddIncompatibleEffect<GunPaddleEffect>();
        }

        public override float SecondsLimit => 10;

        protected override void ChangeScenario()
        {
            LongPaddle longPaddle = TexturesFactory.GetTextureClone<LongPaddle>();
            _scenario.SetPaddle(longPaddle);
        }

        protected override void RecoverScenario()
        {
            Paddle paddle = TexturesFactory.GetTextureClone<Paddle>();
            _scenario.SetPaddle(paddle);
        }
    }
}
