using OpenTK;

namespace DimBall
{

    public static class SphereGenerator
    {
        public static void GenerateUVSphere(int latitudes, int longitudes, float radius, out Vector3[] vertices, out uint[] indices)
        {
            int vertexCount = (latitudes + 1) * (longitudes + 1);
            int indexCount = latitudes * longitudes * 6; // Each rectangle in the grid consists of 2 triangles, and each triangle has 3 vertices.

            vertices = new Vector3[vertexCount];
            indices = new uint[indexCount];

            int vertexIndex = 0;
            int indexIndex = 0;

            for (int i = 0; i <= latitudes; i++)
            {
                float theta = i * MathF.PI / latitudes; // Vertical angle [0, Pi].
                float sinTheta = MathF.Sin(theta);
                float cosTheta = MathF.Cos(theta);

                for (int j = 0; j <= longitudes; j++)
                {
                    float phi = j * 2 * MathF.PI / longitudes; // Horizontal angle [0, 2*Pi].
                    float sinPhi = MathF.Sin(phi);
                    float cosPhi = MathF.Cos(phi);

                    float x = cosPhi * sinTheta;
                    float y = cosTheta;
                    float z = sinPhi * sinTheta;

                    vertices[vertexIndex++] = new Vector3(x * radius, y * radius, z * radius);

                    // Don't generate indices for the top and bottom edge.
                    if (i < latitudes && j < longitudes)
                    {
                        // First triangle of square
                        indices[indexIndex++] = (uint)(i * (longitudes + 1) + j);
                        indices[indexIndex++] = (uint)((i + 1) * (longitudes + 1) + j);
                        indices[indexIndex++] = (uint)(i * (longitudes + 1) + j + 1);

                        // Second triangle of square
                        indices[indexIndex++] = (uint)(i * (longitudes + 1) + j + 1);
                        indices[indexIndex++] = (uint)((i + 1) * (longitudes + 1) + j);
                        indices[indexIndex++] = (uint)((i + 1) * (longitudes + 1) + j + 1);
                    }
                }
            }
        }
    }

}
public static class SphereGenerator
{
    public static void GenerateUVSphere(int latitudes, int longitudes, float radius, out Vector3[] vertices, out uint[] indices)
    {
        int vertexCount = (latitudes + 1) * (longitudes + 1);
        int indexCount = latitudes * longitudes * 6; // Each rectangle in the grid consists of 2 triangles, and each triangle has 3 vertices.

        vertices = new Vector3[vertexCount];
        indices = new uint[indexCount];

        int vertexIndex = 0;
        int indexIndex = 0;

        for (int i = 0; i <= latitudes; i++)
        {
            float theta = i * MathF.PI / latitudes; // Vertical angle [0, Pi].
            float sinTheta = MathF.Sin(theta);
            float cosTheta = MathF.Cos(theta);

            for (int j = 0; j <= longitudes; j++)
            {
                float phi = j * 2 * MathF.PI / longitudes; // Horizontal angle [0, 2*Pi].
                float sinPhi = MathF.Sin(phi);
                float cosPhi = MathF.Cos(phi);

                float x = cosPhi * sinTheta;
                float y = cosTheta;
                float z = sinPhi * sinTheta;

                vertices[vertexIndex++] = new Vector3(x * radius, y * radius, z * radius);

                // Don't generate indices for the top and bottom edge.
                if (i < latitudes && j < longitudes)
                {
                    // First triangle of square
                    indices[indexIndex++] = (uint)(i * (longitudes + 1) + j);
                    indices[indexIndex++] = (uint)((i + 1) * (longitudes + 1) + j);
                    indices[indexIndex++] = (uint)(i * (longitudes + 1) + j + 1);

                    // Second triangle of square
                    indices[indexIndex++] = (uint)(i * (longitudes + 1) + j + 1);
                    indices[indexIndex++] = (uint)((i + 1) * (longitudes + 1) + j);
                    indices[indexIndex++] = (uint)((i + 1) * (longitudes + 1) + j + 1);
                }
            }
        }
    }
}
