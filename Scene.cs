using Microsoft.Xna.Framework.Graphics;

namespace DontLikePoetry;

public enum GameMode
{
   PLAYING = 0,
   CUTSCENE = 1  
}
public class Scene
{
    public Player _playerScene;
    private Camera _camera;
    public GameMode gameMode { get; private set; }
    public double FPS { get; private set; }
    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        _camera = new Camera();
        _camera.LoadContent(graphicsDevice);

        var x = 700;
        var y = 560;
        var width = 16;
        var height = 16;

        _playerScene = new Player(x, y, width, height);
        _playerScene.LoadContent(graphicsDevice, width, height);


        gameMode = GameMode.PLAYING;
        FPS = 1.0/60.0;
    }

    public Player PlayerScene
    {
        get
        {
            return _playerScene;
        }
        private set
        {
            _playerScene = value;
        }
    }
    public void Start()
    {
        FPS = 1.0/5.0;
        var x = 700;
        var y = 560;
        
        _camera._player.Move(x, y);
        gameMode = GameMode.CUTSCENE;
    }

    public void Update()
    {
        
        
        if (_camera._door.Bound.Width < 20)
        {
            var tempPlayerScene = _playerScene.Bound;
            var tempDoor = _camera._door.Bound;
            var tempFloor = _camera._floor.Bound;

            tempFloor.X += 16;
            tempPlayerScene.Y += 4;
            
            _camera._floor.Move(_camera._floor.Bound.X, _camera._floor.Bound.Y);

            tempDoor.Width += 4;
            tempDoor.Height += 16;
            
            tempFloor.Width += 16;
            tempFloor.Height += 4;

            tempPlayerScene.Width += 8;
            tempPlayerScene.Height += 8;

            _camera._door.Bound = tempDoor;
            _camera._floor.Bound = tempFloor;
            _playerScene.Bound = tempPlayerScene;
        }
        else if(_camera._door.Bound.Width >= 20)
        {
            var tempPlayerScene = _playerScene.Bound;
            tempPlayerScene.X += 10;

            _playerScene.Bound = tempPlayerScene;
        }
        _playerScene.Move(_playerScene.Bound.X, _playerScene.Bound.Y);   

        if(_playerScene.Bound.X >= 950)
        {
             gameMode = GameMode.PLAYING;
        }
    }

    public void Stop()
    {
        var tempPlayerScene = _playerScene.Bound;
        var tempDoor = _camera._door.Bound;
        var tempFloor = _camera._floor.Bound;
        
        tempPlayerScene.X = 700;
        tempPlayerScene.Y = 560;
        tempFloor.X = 700;
        tempFloor.Y = 564;
        tempDoor.Width = 8;
        tempDoor.Height = 32;
        tempPlayerScene.Width = 16;
        tempPlayerScene.Height = 16;

        _camera._floor.Move(tempFloor.X, tempFloor.Y);
        
        tempFloor.Width = 120*5;
        tempFloor.Height = 8;
        
        _camera._door.Bound = tempDoor;
        _camera._floor.Bound = tempFloor;
        _playerScene.Bound = tempPlayerScene;
        _playerScene.Move(tempPlayerScene.X, 548);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _camera.Draw(spriteBatch);
        _playerScene.Draw(spriteBatch);
    }
}