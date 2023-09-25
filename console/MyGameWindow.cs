using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Graphics;
using ToolbarNS;


public class MyGameWindow : GameWindow
{
    private Toolbar toolbar = new Toolbar();
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private Vector2 lastMousePos;
    private bool isMouseDown = false;

    private Vector2 angularVelocity = Vector2.Zero;
    private const float decayFactor = 0.98f;  // Adjust this value to control how fast the spin slows down

    public MyGameWindow() : base(800, 600)
    {

        Title = "My 3D Room";
        Load += Window_Load;
        RenderFrame += Window_RenderFrame;
        Resize += Window_Resize;
        KeyDown += Keyboard_KeyDown;
        MouseDown += Mouse_ButtonDown;
        MouseUp += Mouse_ButtonUp;
        MouseMove += Mouse_Moved;
        UpdateFrame += Window_UpdateFrame;
        RenderFrame += Window_RenderFrame;

    }

    private void Window_Load(object? sender, EventArgs e)
    {
        GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
    }

    private void Window_RenderFrame(object? sender, FrameEventArgs e)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        // Set up perspective projection
        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadIdentity();
        Matrix4 perspectiveMat = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), Width / (float)Height, 0.1f, 100f);
        GL.LoadMatrix(ref perspectiveMat);

        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadIdentity();
        GL.Translate(0.0, 0.0, -3.0); // Move the sphere a bit into the view

        // Apply rotations
        GL.Rotate(rotationX, 1.0f, 0.0f, 0.0f);
        GL.Rotate(rotationY, 0.0f, 1.0f, 0.0f);

        DrawSphere(1.0, 20, 20);

        SwapBuffers();
    }



    private void Window_Resize(object? sender, EventArgs e)
    {
        GL.Viewport(0, 0, Width, Height);
    }

    private void Keyboard_KeyDown(object? sender, KeyboardKeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            Exit();
        }
        const float rotationSpeed = 5.0f; // Adjust as needed

        if (e.Key == Key.Left)
            rotationY -= rotationSpeed;
        else if (e.Key == Key.Right)
            rotationY += rotationSpeed;
        else if (e.Key == Key.Up)
            rotationX -= rotationSpeed;
        else if (e.Key == Key.Down)
            rotationX += rotationSpeed;
    }

    private void DrawSphere(double r, int lats, int longs)
    {
        for (int i = 0; i <= lats; i++)
        {
            double lat0 = Math.PI * (-0.5 + (double)(i - 1) / lats);
            double z0 = r * Math.Sin(lat0);
            double zr0 = r * Math.Cos(lat0);

            double lat1 = Math.PI * (-0.5 + (double)i / lats);
            double z1 = r * Math.Sin(lat1);
            double zr1 = r * Math.Cos(lat1);

            GL.Begin(PrimitiveType.QuadStrip);
            for (int j = 0; j <= longs; j++)
            {
                double lng = 2 * Math.PI * (double)(j - 1) / longs;
                double x = Math.Cos(lng);
                double y = Math.Sin(lng);

                // Adjust color based on latitude and longitude for gradient effect
                Color4 color0 = GetGradientColor((double)(i - 1) / lats, (double)(j - 1) / longs);
                Color4 color1 = GetGradientColor((double)i / lats, (double)j / longs);

                GL.Color4(color0);
                GL.Vertex3(x * zr0, y * zr0, z0);
                GL.Color4(color1);
                GL.Vertex3(x * zr1, y * zr1, z1);
            }
            GL.End();
        }
    }

    private Color4 GetGradientColor(double latPercent, double longPercent)
    {
        float r = (float)(0.5f + 0.5f * Math.Cos(latPercent * Math.PI * 2));
        float g = (float)(0.5f + 0.5f * Math.Sin(latPercent * Math.PI * 2));
        float b = (float)(0.5f + 0.5f * Math.Sin(longPercent * Math.PI * 2));

        return new Color4(r, g, b, 1.0f);
    }

    private void Mouse_ButtonDown(object? sender, MouseButtonEventArgs e)
    {
        if (e.Button == MouseButton.Left)
        {
            isMouseDown = true;
            lastMousePos = new Vector2(e.X, e.Y);
        }
    }

    private void Mouse_ButtonUp(object? sender, MouseButtonEventArgs e)
    {
        if (e.Button == MouseButton.Left)
        {
            isMouseDown = false;
        }
    }

    private void Mouse_Moved(object? sender, MouseMoveEventArgs e)
    {
        if (isMouseDown)
        {
            // Calculate the distance the mouse has moved since the last event
            float deltaX = e.X - lastMousePos.X;
            float deltaY = e.Y - lastMousePos.Y;

            rotationX += deltaY * 0.5f;
            rotationY += deltaX * 0.5f;

            // Store this as the angular velocity
            angularVelocity = new Vector2(deltaX, deltaY);

            lastMousePos = new Vector2(e.X, e.Y);
        }
    }

    private void Window_UpdateFrame(object? sender, FrameEventArgs e)
    {
        if (!isMouseDown)  // Only spin when not dragging
        {
            rotationX += angularVelocity.Y * 0.5f;
            rotationY += angularVelocity.X * 0.5f;

            // Apply the decay factor
            angularVelocity *= decayFactor;

            // You can also add a threshold to stop minor spins, e.g., if the speed is too low:
            if (angularVelocity.Length < 0.1f)
            {
                angularVelocity = Vector2.Zero;
            }
        }
    }


}
