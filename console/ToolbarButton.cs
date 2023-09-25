using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace ToolbarNS
{
    public class ToolbarButton
    {
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public string Label { get; }

        public ToolbarButton(int x, int y, int width, int height, string label)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Label = label;
        }

        public void Render()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.8, 0.8, 0.8);  // Light gray for button background
            GL.Vertex2(X, Y);
            GL.Vertex2(X + Width, Y);
            GL.Vertex2(X + Width, Y + Height);
            GL.Vertex2(X, Y + Height);
            GL.End();

            // Placeholder for button label rendering
            // This would typically involve another library or system to draw text
        }

        public bool IsInside(int x, int y)
        {
            return x >= X && x <= X + Width && y >= Y && y <= Y + Height;
        }
    }
}
