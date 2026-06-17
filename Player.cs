using System.Dynamic;
using System.IO.Pipelines;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DontLikePoetry;

public class Player
{
    // posição
    // tamanho
    // velocidade
    // retângulo de colisão
    // método para mover
    // método para desenhar

    private Rectangle _player;
    private Texture2D _texture;
    private Color[] _color;

    public Player(int x, int y, int width, int height)
    {
        _player = new Rectangle(x, y, width, height);
    }
    public void LoadContent(GraphicsDevice graphicsDevice, int width, int height)
    {
        _texture = new Texture2D(graphicsDevice, width, height);
        _color = Enumerable.Repeat(Color.White, width*height).ToArray();
        _texture.SetData(_color);
    }

    public Rectangle Bound
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

    public void Draw(SpriteBatch spriteBatch, Rectangle ator)
    {
        spriteBatch.Draw(_texture, ator, Color.Purple);
    }
}