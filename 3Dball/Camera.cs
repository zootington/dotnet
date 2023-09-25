using OpenTK;

namespace DimBall
{
    public class Camera
    {
        public Vector3 Position { get; private set; }
        public Vector3 Front { get; private set; }
        public Vector3 Up { get; private set; }
        public Vector3 Right { get; private set; }

        private float _yaw;   // rotation around Y-axis, initialized at -90.0f to face forward
        private float _pitch; // rotation around X-axis

        private const float sensitivity = 0.1f; // adjust this to make rotation more or less sensitive

        public Camera(Vector3 position = new Vector3())
        {
            Position = position;
            Front = -Vector3.UnitZ; 
            Up = Vector3.UnitY;
            Right = Vector3.UnitX;

            _yaw = -90.0f;
            _pitch = 0.0f;

            UpdateCameraVectors();
        }

        public void Rotate(Vector2 delta)
        {
            _yaw += delta.X * sensitivity;
            _pitch += delta.Y * sensitivity;

            // Limit pitch to -89 to 89 degrees
            _pitch = MathHelper.Clamp(_pitch, -89.0f, 89.0f);

            UpdateCameraVectors();
        }

        private void UpdateCameraVectors()
        {
            // Calculate the new Front vector
            Vector3 front;
            front.X = (float)Math.Cos(MathHelper.DegreesToRadians(_yaw)) * (float)Math.Cos(MathHelper.DegreesToRadians(_pitch));
            front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(_pitch));
            front.Z = (float)Math.Sin(MathHelper.DegreesToRadians(_yaw)) * (float)Math.Cos(MathHelper.DegreesToRadians(_pitch));
            Front = Vector3.Normalize(front);

            // Also re-calculate the Right and Up vector
            Right = Vector3.Normalize(Vector3.Cross(Front, Vector3.UnitY));  
            Up = Vector3.Normalize(Vector3.Cross(Right, Front)); 
        }
    }
}
