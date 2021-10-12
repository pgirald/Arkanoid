using Arkanoid.Application.App;
using Arkanoid.Application.App.General;
using Arkanoid.Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arkanoid
{
    public class Arkanoid : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Projectile _projectile;
        private Paddle _paddle;
        private BlocksSet _blocks;

        public Arkanoid()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            ParentContainer parent = new ParentContainer()
            {
                Width = _graphics.PreferredBackBufferWidth,
                Height = _graphics.PreferredBackBufferHeight
            };

            _projectile = new Projectile
            {
                Container = parent,
                AbsolutePositionAlignment = Alignment.MiddleCenter,
            };

            _paddle = new Paddle
            {
                Color = Color.Green,
                Container = parent,
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _2dTexturesFactory.Load(Content);
            _blocks = new BlocksSet(_projectile.Container as ParentContainer);
            _blocks.AddRows();
            _projectile.Texture = Content.Load<Texture2D>(_projectile.ParentPath + _projectile.TexturePath);
            _paddle.Texture = Content.Load<Texture2D>(_paddle.ParentPath + _paddle.TexturePath);
            _projectile.OriginAlignment = Alignment.MiddleCenter;
            _paddle.OriginAlignment = Alignment.BottomCenter;
            _paddle.PositionAlignment = Alignment.BottomCenter;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Right))
            {
                _paddle.X += 9f;
            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                _paddle.X -= 9f;
            }
            if (kstate.IsKeyDown(Keys.W))
            {
                _blocks.Y -= 9f;
            }
            if (kstate.IsKeyDown(Keys.A))
            {
                _blocks.X -= 9f;
            }
            if (kstate.IsKeyDown(Keys.S))
            {
                _blocks.Y += 9f;
            }
            if (kstate.IsKeyDown(Keys.D))
            {
                _blocks.X += 9f;
            }

            if (_paddle.Right > _paddle.Container.Right)
            {
                _paddle.Right = _paddle.Container.Right;
            }

            if (_paddle.Left < _paddle.Container.Left)
            {
                _paddle.Left = _paddle.Container.Left;
            }

            if (_blocks.Right > _blocks.Container.Right)
            {
                _blocks.Right = _blocks.Container.Right;
            }

            if (_blocks.Left < _blocks.Container.Left)
            {
                _blocks.Left = _blocks.Container.Left;
            }

            if (_blocks.Top < _blocks.Container.Top)
            {
                _blocks.Top = _blocks.Container.Top;
            }

            if (_blocks.Bottom > _blocks.Container.Bottom)
            {
                _blocks.Bottom = _blocks.Container.Bottom;
            }

            _projectile.move();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //_smily.draw(_spriteBatch, _smilyPosition);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_paddle);
            _spriteBatch.Draw(_projectile);
            _spriteBatch.Draw(_blocks);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
