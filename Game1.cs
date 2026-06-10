using System;
using System.IO.Pipelines;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DontLikePoetry;

public enum GameMode
{
   PLAYING = 0,
   CUTSCENE = 1  
}

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private int windowWidth = 1920;
    private int windowHeight = 1080;
    private GameMode _gameMode;
    private double FPS = 1.0 / 60.0;
    private Scene _scene;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = windowWidth;
        _graphics.PreferredBackBufferHeight = windowHeight;
        _graphics.ApplyChanges();

        TargetElapsedTime = TimeSpan.FromSeconds(FPS); // FPS

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    { 
        _gameMode = GameMode.PLAYING;
        _scene = new Scene();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _scene.LoadContent(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (_scene.PlayerScene.Pip.Y > windowHeight || _scene.PlayerScene.Pip.Y < 0 || _scene.PlayerScene.Pip.X > windowWidth || _scene.PlayerScene.Pip.X < 0)
        {
            FPS = 1.0 / 5.0;
            TargetElapsedTime = TimeSpan.FromSeconds(FPS);
            _gameMode = GameMode.CUTSCENE;
        }

        if (_gameMode == GameMode.PLAYING)
        {
            FPS = 1.0 / 60.0;
            TargetElapsedTime = TimeSpan.FromSeconds(FPS);
            _scene.PlayerScene.Update();
        }
        else if (_gameMode == GameMode.CUTSCENE)
        {
            _scene.Update();
            if(_scene._isScene == false)
            {
                _gameMode = GameMode.PLAYING;
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _scene.PlayerScene.Draw(_spriteBatch);
        _scene.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
