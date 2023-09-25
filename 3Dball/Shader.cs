using OpenTK.Graphics.OpenGL;

namespace DimBall
{
    public class Shader
    {
        public int Handle { get; private set; }

        private int LoadShader(ShaderType type, string path)
        {
            var source = System.IO.File.ReadAllText(path);
            var shader = GL.CreateShader(type);
            GL.ShaderSource(shader, source);
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out int isCompiled);
            if (isCompiled == 0)
            {
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Failed to compile {type} shader with error: {infoLog}");
            }

            return shader;
        }

        public Shader(string vertexPath, string fragmentPath)
        {
            var vertexShader = LoadShader(ShaderType.VertexShader, vertexPath);
            var fragmentShader = LoadShader(ShaderType.FragmentShader, fragmentPath);

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int isLinked);
            if (isLinked == 0)
            {
                var infoLog = GL.GetProgramInfoLog(Handle);
                throw new Exception($"Failed to link program with error: {infoLog}");
            }

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }


        public void Use() => GL.UseProgram(Handle);
    }
}
