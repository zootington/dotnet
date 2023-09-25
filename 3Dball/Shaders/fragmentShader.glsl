#version 330 core

// in vec3 fragColor;  // Comment this out for now

out vec4 outColor;

void main()
{
    // outColor = vec4(fragColor, 1.0); // Comment this out for now
    outColor = vec4(1.0, 0.0, 0.0, 1.0); // Set the pixel color to red
}
