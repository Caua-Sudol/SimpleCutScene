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
    private Texture2D _texture;
    private Color[] _color;

    public Player(int x, int y, int width, int height)
    {
        _player = new Vector2(x, y);
        _widthPlayer = width;
        _heightPlayer = height;
    }
    public void LoadContent(GraphicsDevice graphicsDevice, int width, int height)
    {
        _texture = new Texture2D(graphicsDevice, width, height);
        _color = Enumerable.Repeat(Color.White, width*height).ToArray();
        _texture.SetData(_color);
    }

    public Vector2 Bound
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

    public void Update()
    {
        var state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.W))
        {
            _player.Y -= 10;
        }
        if (state.IsKeyDown(Keys.S) )
        {
            _player.Y += 10;
        }
        if (state.IsKeyDown(Keys.D))
        {
            _player.X += 10;
        }
        if (state.IsKeyDown(Keys.A))
        {
            _player.X -= 10;
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

    public void Draw(SpriteBatch spriteBatch, Vector2 actor)
    {
        spriteBatch.Draw(_texture, actor, Color.Purple);
    }
}