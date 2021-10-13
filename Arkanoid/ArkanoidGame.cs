using Arkanoid.Application.App;
using Arkanoid.Application.App.General;
using Arkanoid.Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arkanoid
{
    public class ArkanoidGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ClassicScenario Scenario;
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TexturesFactory.Load(Content);
            Scenario = new ClassicScenario(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            info.ElapsedFrameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Scenario.Run(info);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //_smily.draw(_spriteBatch, _smilyPosition);

            _spriteBatch.Begin();
            _spriteBatch.Draw(Scenario);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
