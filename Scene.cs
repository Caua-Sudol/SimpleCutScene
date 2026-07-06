using Microsoft.Xna.Framework.Graphics;

namespace DontLikePoetry;

public enum GameMode
{
   PLAYING = 0,
   CUTSCENE = 1
}

public class Scene
{
    private const int PlayerStartX = 700;
    private const int PlayerStartY = 552;
    private const int PlayerWidth = 16;
    private const int PlayerHeight = 16;

    private const int DoorX = 960;
    private const int DoorY = 540;
    private const int DoorWidth = 8;
    private const int DoorHeight = 32;

    private const int FloorX = 700;
    private const int FloorY = 564;
    private const int FloorWidth = 600;
    private const int FloorHeight = 8;

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
        _player = new Player(PlayerStartX, PlayerStartY, PlayerWidth, PlayerHeight);
        _player.LoadContent(graphicsDevice, PlayerWidth, PlayerHeight);

        _door = new Door(DoorX, DoorY, DoorWidth, DoorHeight);
        _door.LoadContent(graphicsDevice, DoorWidth, DoorHeight);

        _floor = new Floor(FloorX, FloorY, FloorWidth, FloorHeight);
        _floor.LoadContent(graphicsDevice, FloorWidth, FloorHeight);

        GameMode = GameMode.PLAYING;
        SecondsPerFrame = 1.0 / 60.0;
    }

    public void Update(Camera camera, int screenWidth, int screenHeight)
    {
        if (GameMode == GameMode.PLAYING)
        {
            _player.Update();

            if (PlayerTouchedEdge())
            {
                camera.Zoom = 2.0f;
                camera.GetTransform();
                StartCutscene();
            }
        }
        else if (GameMode == GameMode.CUTSCENE)
        {
            UpdateCutscene(camera);
        }
    }

    private bool PlayerTouchedEdge()
    {
        return _player.Bound.Intersects(_door.Bound);
    }

    private void StartCutscene()
    {
        GameMode = GameMode.CUTSCENE;
        SecondsPerFrame = 1.0 / 5.0;
        _player.Move(PlayerStartX, PlayerStartY);
    }

    private void UpdateCutscene(Camera camera)
    {
        if(_player.Bound.X <= 940)
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
        _player.Move(PlayerStartX, PlayerStartY);
    }

    public void Draw(SpriteBatch spriteBatch, Camera camera)
    {

        var cameraTransform = camera.GetTransform();

        spriteBatch.Begin(transformMatrix: cameraTransform);

        _player.Draw(spriteBatch, _player.Bound);
        _door.Draw(spriteBatch, _door.Bound);
        _floor.Draw(spriteBatch, _floor.Bound);

        spriteBatch.End();
    
    }
}
