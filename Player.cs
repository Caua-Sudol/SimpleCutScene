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

    public Rectangle _pip;
    private Texture2D _texture;
    private Color[] _color;

    public void LoadContent(GraphicsDevice graphicsDevice, int wallWidth, int wallHeight)
    {
        _pip = new Rectangle(0, 0, wallWidth, wallHeight);
        _texture = new Texture2D(graphicsDevice, wallWidth, wallHeight);
        _color = Enumerable.Repeat(Color.White, wallWidth*wallHeight).ToArray();
        _texture.SetData(_color);
    }

    public Rectangle Pip
    {
        set 
        {
            _pip = value;
        }
        get
        {
            return _pip;
        }
    }

    public void Update()
    {
        var state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.W))
        {
            _pip.Y -= 10;
        }
        if (state.IsKeyDown(Keys.S) )
        {
            _pip.Y += 10;
        }
        if (state.IsKeyDown(Keys.D))
        {
            _pip.X += 10;
        }
        if (state.IsKeyDown(Keys.A))
        {
            _pip.X -= 10;
        }
    }

    public void Move(int x, int y)
    {
        _pip.Y = y;
        _pip.X = x;
    }

    public void Draw(SpriteBatch spriteBatch)
    {

        spriteBatch.Draw(_texture, _pip, Color.Purple);
    }
}