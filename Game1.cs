using System;
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
    private Camera _camera;
    private Scene _scene;

    private Vector2 positionCamera;
    private Vector2 dimentionsCamera;

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
        _scene = new Scene();

        positionCamera = new Vector2((float)windowWidth/2, (float)windowHeight/2);
        dimentionsCamera = new Vector2((float)windowWidth, (float)windowHeight);

        _camera = new Camera(positionCamera, dimentionsCamera, 1.0f);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _scene.LoadContent(GraphicsDevice);

        TargetElapsedTime = TimeSpan.FromSeconds(_scene.SecondsPerFrame);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _scene.Update(_camera, windowWidth, windowHeight);
        TargetElapsedTime = TimeSpan.FromSeconds(_scene.SecondsPerFrame);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _scene.Draw(_spriteBatch, _camera);

        base.Draw(gameTime);
    }
}
