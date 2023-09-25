using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

public struct RectangleF
{
    public float X { get; }
    public float Y { get; }
    public float Width { get; }
    public float Height { get; }

    public float Left => X;
    public float Right => X + Width;
    public float Top => Y;
    public float Bottom => Y + Height;

    public RectangleF(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public bool Contains(float x, float y)
    {
        return x >= X && x <= X + Width && y >= Y && y <= Y + Height;
    }
}

public class Button
{
    private RectangleF _bounds;
    private Vector4 _color;
    private readonly Vector4 _defaultColor = new Vector4(0.7f, 0.7f, 0.7f, 1.0f); // Gray
    private readonly Vector4 _hoverColor = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);  // Darker gray
    private string _text;

    public Button(float x, float y, float width, float height, string text)
    {
        _bounds = new RectangleF(x, y, width, height);
        _text = text;
        _color = _defaultColor;
    }

    public bool IsMouseOver(Vector2 position)
    {
        return _bounds.Contains(position.X, position.Y);
    }

    public void Render()
    {
        GL.Color4(_color);
        GL.Begin(PrimitiveType.Quads);
        GL.Vertex2(_bounds.Left, _bounds.Top);
        GL.Vertex2(_bounds.Right, _bounds.Top);
        GL.Vertex2(_bounds.Right, _bounds.Bottom);
        GL.Vertex2(_bounds.Left, _bounds.Bottom);
        GL.End();

        // Rendering text is not trivial and would typically require a separate library 
        // or an OpenGL font rendering technique, so it's not included in this example.
    }

    public void Trigger()
    {
        // Whatever action you want to perform on button click goes here.
    }

    public void OnMouseHover()
    {
        _color = _hoverColor;
    }

    public void OnMouseLeave()
    {
        _color = _defaultColor;
    }
}

