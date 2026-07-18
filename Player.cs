using System.Dynamic;
using System.IO.Pipelines;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DontLikePoetry;

public class Player
{
    private Vector2 _player;
    private int _widthPlayer;
    private int _heightPlayer;

    public Vector2 _velocity { get; private set;}
    public float _gravity { get; private set;}
    private Texture2D _texture;
    private Color[] _color;

    public Player(int x, int y, Vector2 velocity, float gravity, int width, int height)
    {
        _player = new Vector2(x, y);
        _velocity = velocity;
        _gravity = gravity;
        _widthPlayer = width;
        _heightPlayer = height;
    }
    public void LoadContent(GraphicsDevice graphicsDevice, int width, int height)
    {
        _texture = new Texture2D(graphicsDevice, width, height);
        _color = Enumerable.Repeat(Color.White, width*height).ToArray();
        _texture.SetData(_color);
    }

    public Vector2 Position
    {
        set 
        {
            _player = value;
        }
        get
        {
            return _player;
        }
    }

    public Rectangle HitBox
    {
        get
        {
            return new Rectangle((int)_player.X, (int)_player.Y, _widthPlayer, _heightPlayer);
        }
    }

    public void Update()
    {
        _player.Y -= _gravity;

        var state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.W))
        {
            _player.Y -= _velocity.Y;
        }
        if (state.IsKeyDown(Keys.S) )
        {
            _player.Y += _velocity.Y;
        }
        if (state.IsKeyDown(Keys.D))
        {
            _player.X += _velocity.X;
        }
        if (state.IsKeyDown(Keys.A))
        {
            _player.X -= _velocity.X;
        }
    }

    public void Move(int x, int y)
    {
        _player.Y = y;
        _player.X = x;
    }

    public void Walk(int x, int y)
    {
        _player.Y += y;
        _player.X += x;
    }

    public void StopFalling(float velocityX)
    {
        _velocity = new Vector2(velocityX, 0);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 actor)
    {
        spriteBatch.Draw(_texture, actor, Color.Purple);
    }
}