using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing; // Ensure this is at the top of your file


namespace DimBall
{
    public class MyGameWindow : GameWindow
    {
        private Shader _shader;
        private Camera _camera;
        private Mesh _sphere;
        private Toolbar _toolbar;
        private bool _isDragging;
        private Vector2 _lastMousePosition;

        public MyGameWindow()
            : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
        }

        private Vector2 ToVector2(Point p)
        {
            return new Vector2(p.X, p.Y);
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.4f, 0.6f, 0.9f, 1.0f);
            _shader = new Shader("./Shaders/vertexShader.glsl", "./Shaders/fragmentShader.glsl");
            _camera = new Camera();
            

            SphereGenerator.GenerateUVSphere(40, 40, 1.0f, out Vector3[] sphereVertices, out uint[] sphereIndices);
            _sphere = new Mesh(sphereVertices, sphereIndices);

            _toolbar = new Toolbar();
            _toolbar.AddButton(new Button(10, 10, 100, 30, "Button1"));

            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _shader.Use();

            // Update camera, set uniforms, bind VAO and render sphere
            _sphere.Render(_sphere.IndexCount);

            _toolbar.Render();

            SwapBuffers();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButton.Left)
            {
                if (_toolbar.IsMouseOver(ToVector2(e.Position)))
                {
                    Button clickedButton = _toolbar.GetButtonAt(ToVector2(e.Position));
                    if (clickedButton != null)
                    {
                        clickedButton.Trigger();
                    }
                }
                else
                {
                    _isDragging = true;
                    _lastMousePosition = ToVector2(e.Position);
                }
            }
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            if (_isDragging)
            {
                Vector2 currentMousePosition = ToVector2(e.Position);
                Vector2 delta = currentMousePosition - _lastMousePosition;
                _camera.Rotate(delta);
                _lastMousePosition = currentMousePosition;
            }
        }



        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButton.Left)
            {
                _isDragging = false;
            }
        }
        // Other event handlers (keyboard, mouse click)...
    }
}
