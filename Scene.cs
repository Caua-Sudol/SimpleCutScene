using Microsoft.Xna.Framework;
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
    private const int PlayerStartY = 560;
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

            if (PlayerTouchedEdge(screenWidth, screenHeight))
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

    private bool PlayerTouchedEdge(int screenWidth, int screenHeight)
    {
        var bound = _player.Bound;

        return bound.Left <= 0
            || bound.Right >= screenWidth
            || bound.Top <= 0
            || bound.Bottom >= screenHeight;
    }

    private void StartCutscene()
    {
        GameMode = GameMode.CUTSCENE;
        SecondsPerFrame = 1.0 / 5.0;
        _player.Move(PlayerStartX, PlayerStartY);
    }

    private void UpdateCutscene(Camera camera)
    {
        //jogador se move até bater na parede
        // depois temos que chamar o fim e resetar as variaveis
    }

    private void FinishCutscene()
    {
        GameMode = GameMode.PLAYING;
        SecondsPerFrame = 1.0 / 60.0;
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
