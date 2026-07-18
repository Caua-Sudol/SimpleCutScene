using System.Dynamic;
using System.IO.Pipelines;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DontLikePoetry;

public enum Direction{
    Top = 1,
    Down = 2,
    Rigth = 3,
    Left = 4
}

public class Player
{
    private Vector2 _player;
    private int _widthPlayer;
    private int _heightPlayer;

    public Vector2 _velocity { get; private set;}
    public float _gravity { get; private set;}
    public bool _isGrounded {get; private set;} = false;
    public Direction _direction {get; private set;} = Direction.Rigth;
    public bool isBreath {get; private set;} = true;
    private float _dashTimer = 0;
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
        var velX = 0f; 
        var velY = _velocity.Y;
        var state = Keyboard.GetState();

        if(_dashTimer > 0)
        {
            if(_direction == Direction.Rigth)
            {
                velX = _dashTimer;
                _dashTimer -= 1f;
            }
            if(_direction == Direction.Left)
            {
                velX = -_dashTimer;
                _dashTimer -= 1f;
            }
            isBreath = false;
        }
        else
        { 
            // if (state.IsKeyDown(Keys.W))
            // {
            //     _direction = Direction.Top;
            // }
            // if (state.IsKeyDown(Keys.S))
            // {
            //     _direction = Direction.Down;
            // }
            if (state.IsKeyDown(Keys.Space) && _isGrounded)
            {
                velY = -15f;
                _isGrounded = false;
            }
            if (state.IsKeyDown(Keys.LeftShift) && isBreath)
            {
                _dashTimer = 15f;
                isBreath = false;
            }
            if (state.IsKeyDown(Keys.D))
            {
                velX = 10f;
                _direction = Direction.Rigth;
            }
            if (state.IsKeyDown(Keys.A))
            {
                velX = -10f;
                _direction = Direction.Left;
            }
            if(_isGrounded == false)
            {
                velY -= _gravity;   
            }
        }
        _velocity = new Vector2(velX, velY);
        _player.X += _velocity.X;
        _player.Y += _velocity.Y;  
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

    public void Grounded()
    {
        _isGrounded = true;
    }
    public void NotGrounded()
    {
        _isGrounded = false;
    }
    public void MoreBreath()
    {
        isBreath = true;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 actor)
    {
        spriteBatch.Draw(_texture, actor, Color.Purple);
    }
}