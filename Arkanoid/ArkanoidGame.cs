using Arkanoid.Application.App;
using Arkanoid.Application.App.Graphics;
using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.GeneralExtensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arkanoid
{
    public class ArkanoidGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ArkanoidScenario Scenario;
        private GameInfo info;

        public ArkanoidGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            info = new GameInfo();
            Scenario = new ArkanoidScenario()
            {
                Width = _graphics.PreferredBackBufferWidth,
                Height = _graphics.PreferredBackBufferHeight
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TexturesFactory.Load(Content);
            Scenario.Initialize();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            info.ElapsedFrameTime = gameTime.ElapsedGameTime.TotalSeconds;
            info.KeyboardState = Keyboard.GetState();
            Scenario.Run(info);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            //_smily.draw(_spriteBatch, _smilyPosition);

            _spriteBatch.Begin();
            _spriteBatch.Draw(Scenario);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
