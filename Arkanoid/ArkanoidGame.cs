using Arkanoid.Application.App.Game;
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
        private World world;
        private GameInfo info;

        public ArkanoidGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            _graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            info = new GameInfo();
            world = new World(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TexturesFactory.Load(Content);
            world.Initialize();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            info.ElapsedFrameTime = gameTime.ElapsedGameTime.TotalSeconds;
            info.KeyboardState = Keyboard.GetState();
            world.RunCurrent(info);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            //_smily.draw(_spriteBatch, _smilyPosition);

            _spriteBatch.Begin();
            _spriteBatch.Draw(world);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
