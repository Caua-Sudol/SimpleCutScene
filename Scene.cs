using Microsoft.Xna.Framework;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DontLikePoetry;

public enum GameMode
{
   PLAYING = 0,
   CUTSCENE = 1,

   FADE_OUT = 2,
   FADE_IN = 3
}

public class Scene
{
    private const int PlayerStartX = 700;
    private const int PlayerStartY = 552;
    private const int PlayerWidth = 16;
    private const int PlayerHeight = 16;
    private Vector2 PlayerVelocity = new Vector2(10.0f, 10.0f); // Não vai ser const, velocidade vai ficar variando
    private const float PlayerGravity = -9.81f; //-9.81f

    private const int DoorX = 960;
    private const int DoorY = 540;
    private const int DoorWidth = 8;
    private const int DoorHeight = 32;

    private const int FloorX = 700;
    private const int FloorY = 564;
    private const int FloorWidth = 600;
    private const int FloorHeight = 8;

    private Color []fadeColor;
    private Rectangle fadeRec;
    private Texture2D fadeTexture;
    private float _fadeAlph;

    private Player _player;
    private Door _door;
    private Floor _floor;

    public GameMode GameMode { get; private set; }
    public double SecondsPerFrame { get; private set; }

    public Player Player
    {
        get
        {
            return _player;
        }
    }

    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        _fadeAlph = 0.0f;

        fadeColor = Enumerable.Repeat(Color.White, 1920*1080).ToArray();
        fadeRec = new Rectangle(0, 0, 1920, 1080);
        fadeTexture = new Texture2D(graphicsDevice, 1920, 1080);
        fadeTexture.SetData(fadeColor);        

        _player = new Player(PlayerStartX, PlayerStartY, PlayerVelocity, PlayerGravity, PlayerWidth, PlayerHeight);
        _player.LoadContent(graphicsDevice, PlayerWidth, PlayerHeight);

        _door = new Door(DoorX, DoorY, DoorWidth, DoorHeight);
        _door.LoadContent(graphicsDevice, DoorWidth, DoorHeight);

        _floor = new Floor(FloorX, FloorY, FloorWidth, FloorHeight);
        _floor.LoadContent(graphicsDevice, FloorWidth, FloorHeight);

        GameMode = GameMode.PLAYING;
        SecondsPerFrame = 1.0 / 60.0;
    }

    public void Update(Camera camera)
    {
        if (GameMode == GameMode.PLAYING)
        {
            _player.Update();

            if (PlayerTouchedObj(_door.Bound))
            {
                SecondsPerFrame = 1.0 / 5.0;
                GameMode = GameMode.FADE_OUT;
            }
        }
        else if (GameMode == GameMode.CUTSCENE)
        {
            StartCutscene(camera);
        }
        else if (GameMode == GameMode.FADE_OUT)
        {
            if(_fadeAlph < 1.0f)
            {
                _fadeAlph += 0.2f;
            }
            if(_fadeAlph >= 1.0f)
            {

                _player.Move(PlayerStartX, PlayerStartY);
                GameMode = GameMode.FADE_IN;
            }
            
        }
        else if (GameMode == GameMode.FADE_IN)
        {
            _fadeAlph -= 0.2f;

            if(_fadeAlph <= 0.0f)
            {
                GameMode = GameMode.CUTSCENE;
            }
        }
        if(PlayerTouchedObj(_floor.Bound))
        {
            var tempPlayer = _player.Position;
            _player.StopFalling(PlayerVelocity.X);
            tempPlayer.Y = _floor.Bound.Y - _player.HitBox.Height;
            _player.Position = tempPlayer;
        }
    }

    private bool PlayerTouchedObj(Rectangle obj)
    {
        return _player.HitBox.Intersects(obj);
    }

    private void StartCutscene(Camera camera)
    {
        camera.Zoom = 2.0f;
        UpdateCutscene(camera);
    }

    private void UpdateCutscene(Camera camera)
    {
        camera.Update(_player);
        if(_player.Position.X <= 940)
        {
            _player.Walk(10, 0);
        }
        else
        {
            FinishCutscene(camera);
        }
    }

    private void FinishCutscene(Camera camera)
    {
        GameMode = GameMode.PLAYING;
        SecondsPerFrame = 1.0 / 60.0;
        camera.Zoom = 1.0f;
        _fadeAlph = 0.0f;
        _player.Move(PlayerStartX, PlayerStartY);
    }

    public void Draw(SpriteBatch spriteBatch, Camera camera)
    {
            var cameraTransform = camera.GetTransform();
            spriteBatch.Begin(transformMatrix: cameraTransform);

            _player.Draw(spriteBatch, _player.Position);
            _door.Draw(spriteBatch, _door.Bound);
            _floor.Draw(spriteBatch, _floor.Bound);
            if (GameMode == GameMode.FADE_OUT || GameMode == GameMode.FADE_IN || GameMode == GameMode.CUTSCENE)
            {
                spriteBatch.Draw(fadeTexture, fadeRec, new Color(Color.Black, _fadeAlph));
            }
            spriteBatch.End();
    }
}
