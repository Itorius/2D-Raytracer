#version 440 core

layout(location = 0) out vec4 color;

in vec3 v_UV;
in vec4 v_Color;

uniform sampler2D t;

void main()
{
	color = v_Color;
}