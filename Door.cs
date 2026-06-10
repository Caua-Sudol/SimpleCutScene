using System.IO.Pipelines;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DontLikePoetry;

public class Door
{
    // posição
    // tamanho
    // retângulo de colisão
    // método para desenhar

    public Rectangle _door;
    private Texture2D _texture;
    Color[] _color;

    public Rectangle Bound
    {
        set
        {
            _door = value;
        }
        get
        {
            return _door;
        }
    }

    public void LoadContent(GraphicsDevice graphicsDevice, int wallWidth, int wallHeight)
    {
        _door = new Rectangle(960, 540, wallWidth, wallHeight);
        _texture = new Texture2D(graphicsDevice, wallWidth, wallHeight);
        _color = Enumerable.Repeat(Color.White, wallWidth*wallHeight).ToArray();
        _texture.SetData(_color);
    }

    public void Draw(SpriteBatch spriteBatch)
    {

        spriteBatch.Draw(_texture, _door, Color.Blue);
    }
}