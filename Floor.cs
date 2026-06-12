using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DontLikePoetry;

public class Floor
{
    // posição
    // tamanho
    // retângulo de colisão
    // método para desenhar

    public Rectangle floor;
    private Texture2D texture;
    private Color[] color;

    public void LoadContent(GraphicsDevice graphicsDevice, int wallWidth, int wallHeight)
    {
        floor = new Rectangle(700, 564, wallWidth, wallHeight);
        texture = new Texture2D(graphicsDevice, wallWidth, wallHeight);
        color = Enumerable.Repeat(Color.White, wallWidth*wallHeight).ToArray();
        texture.SetData(color);
    }
    public void Move(int x, int y)
    {
        floor.Y = y;
        floor.X = x;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, floor, Color.Green);
    }
}