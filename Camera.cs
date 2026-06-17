using Microsoft.Xna.Framework;

namespace DontLikePoetry;

public class Camera
{
    public Camera(int screenWidth, int screenHeight)
    {
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
        X = 700;
        Y = 560;
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

    public Rectangle GetTransform(Rectangle obj)
    {
        // Funcionou mas preciso de uma transição suave.
        Rectangle tempObj = obj;
        
        tempObj.X = (obj.X - X) * Zoom;
        tempObj.Y = (obj.Y - Y) * Zoom;

        tempObj.Width = obj.Width * Zoom;
        tempObj.Height = obj.Height * Zoom;


        return tempObj;
    }
}
