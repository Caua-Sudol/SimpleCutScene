using Microsoft.Xna.Framework;

namespace DontLikePoetry;

public class Camera
{
    public Camera(int screenWidth, int screenHeight)
    {
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
        X = 0;
        Y = 0;
        Zoom = 1;
    }

    public int ScreenWidth { get; private set; }
    public int ScreenHeight { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Zoom { get; private set; }

    public void Move(int x, int y)
    {
        X += x;
        Y += y;
    }

    public void MoveTo(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void SetZoom(int zoom)
    {
        Zoom = zoom;
    }

    public Matrix? GetTransform()
    {
        return null;
    }
}
