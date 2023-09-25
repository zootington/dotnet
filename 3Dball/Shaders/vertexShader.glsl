#version 330 core

layout(location = 0) in vec3 inPosition; // Vertex position
// layout(location = 1) in vec3 inColor;    // Comment out or remove this line

// out vec3 fragColor; // Comment out or remove this line

uniform mat4 model;      // Model matrix
uniform mat4 view;       // View matrix
uniform mat4 projection; // Projection matrix

void main()
{
    // fragColor = inColor;  // Comment out or remove this line
    gl_Position = projection * view * model * vec4(inPosition, 1.0);
}
