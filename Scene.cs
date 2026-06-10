using Microsoft.Xna.Framework.Graphics;

namespace DontLikePoetry;

public class Scene
{
    // Scene vai ter o enum, controlar o inicio, fim e update da cena e a camera desenha tudo.
    private Door _door;
    private Player _player;
    private Floor _floor;
    private int x, y;
    private int xFloor, yFloor;
    private int _doorWidth, _doorHeight;
    private int _playerWidth, _playerHeight;
    private int _floorWidth, _floorHeight;
    public bool _isScene { get; private set; }

    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        _doorWidth = 8;
        _doorHeight = 32;
        _playerWidth = 16;
        _playerHeight = 16;
        _floorWidth = 120*5;
        _floorHeight = 8;

        _door = new Door();
        _player = new Player();
        _floor = new Floor();
        _player.LoadContent(graphicsDevice, _playerWidth, _playerHeight);
        _door.LoadContent(graphicsDevice, _doorWidth, _doorHeight);
        _floor.LoadContent(graphicsDevice, _floorWidth, _floorHeight);

        x = 700;
        y = 560;
        xFloor = 700;
        yFloor = 564;

        _isScene = true;
    }

    public Player PlayerScene
    {
        get
        {
            return _player;
        }
        private set
        {
            _player = value;
        }
    }

    public void Update()
    {
        _isScene = true;
        
        if (_door.Bound.Width < 20)
        {
            var tempPip = _player.Pip;
            yFloor += 16;
            y += 4;
            _floor.Move(xFloor, yFloor);

            var tempDoor = _door.Bound;

            tempDoor.Width += 4;
            tempDoor.Height += 16;
            _door.Bound = tempDoor;

            _floor.floor.Width += 16;
            _floor.floor.Height += 4;

            tempPip.Width += 8;
            tempPip.Height += 8;
            _player.Pip = tempPip;
        }
        else if(_door.Bound.Width >= 20)
        {
            x += 10;
        }
        _player.Move(x, y);   

        if(x >= 950)
        {
            var tempPip = _player.Pip;
            var tempDoor = _door.Bound;
            x = 700;
            y = 560;
            xFloor = 700;
            yFloor = 564;
            _floor.Move(700, 564);
            tempDoor.Width = 8;
            tempDoor.Height = 32;
            _floor.floor.Width = 120*5;
            _floor.floor.Height = 8;
            tempPip.Width = 16;
            tempPip.Height = 16;
            _door.Bound = tempDoor;
            _player.Pip = tempPip;
            _player.Move(700, 548);
            _isScene = false;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _door.Draw(spriteBatch);
        _floor.Draw(spriteBatch);
    }
}