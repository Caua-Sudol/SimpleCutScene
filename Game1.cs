using System;
using System.IO.Pipelines;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DontLikePoetry;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private int windowWidth = 1920;
    private int windowHeight = 1080;
    private GameMode _gameMode;
    private Camera _camera;
    private Scene _scene;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = windowWidth;
        _graphics.PreferredBackBufferHeight = windowHeight;
        _graphics.ApplyChanges();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    { 
        _gameMode = GameMode.PLAYING;
        _scene = new Scene();
        _camera = new Camera();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _scene.LoadContent(GraphicsDevice);
        _camera.LoadContent(GraphicsDevice);

        TargetElapsedTime = TimeSpan.FromSeconds(_scene.FPS);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (_camera._player.Bound.Y > windowHeight ||_camera._player.Bound.Y < 0 ||_camera._player.Bound.X > windowWidth ||_camera._player.Bound.X < 0)
        {
            _scene.Start();
            TargetElapsedTime = TimeSpan.FromSeconds(_scene.FPS);
        }

        if (_scene.gameMode == GameMode.PLAYING)
        {
            _scene.Stop();
            TargetElapsedTime = TimeSpan.FromSeconds(_scene.FPS);
            _camera._player.Update();
        }
        else if (_gameMode == GameMode.CUTSCENE)
        {
            _scene.Update();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        if (_scene.gameMode == GameMode.PLAYING)
        {
            _camera.Draw(_spriteBatch);
        }
        else if (_gameMode == GameMode.CUTSCENE)
        {
            _scene.Draw(_spriteBatch);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
