using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DontLikePoetry;

public class Camera
{
    private int _zoom;
    private int _cameraPosition;
    public Door _door { get; private set; }
    public Player _player { get; private set; }
    public Floor _floor { get; private set; }
    private int _doorWidth, _doorHeight;
    private int _playerWidth, _playerHeight;
    private int _floorWidth, _floorHeight;

    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        _doorWidth = 8;
        _doorHeight = 32;
        _playerWidth = 16;
        _playerHeight = 16;
        _floorWidth = 120*5;
        _floorHeight = 8;

        var x = 700;
        var y = 560;
        
        _player = new Player(x, y, _playerWidth, _playerHeight);
        _player.LoadContent(graphicsDevice, _playerWidth, _playerHeight);

        x = 960;
        y = 540;

        _door = new Door(x, y, _doorWidth, _doorHeight);
        _door.LoadContent(graphicsDevice, _doorWidth, _doorHeight);
        
        x = 700;
        y = 564;

        
        _floor = new Floor(x, y, _floorWidth, _floorHeight);
        _floor.LoadContent(graphicsDevice, _floorWidth, _floorHeight);
    }

    public void Update()
    {
        _player.Update();
    }

    public void Move(int x, int y, Rectangle obj)
    {
        var pX = obj.X;
        var pY = obj.Y;

        pX += x;
        pY += y;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _player.Draw(spriteBatch);
        _door.Draw(spriteBatch);
        _floor.Draw(spriteBatch);
    }
}