#version 440 core

layout(location = 0) out vec4 color;

in vec3 v_UV;
in vec4 v_Color;

uniform sampler2DMS tex;
uniform vec2 u_Viewport;

vec4 samples[4][4];

void main()
{
	ivec2 texturePosition = ivec2(v_UV.x * u_Viewport.x, v_UV.y * u_Viewport.y);

	for (int i = 0; i < 8; i++)
	{
		color += texelFetch(tex, texturePosition, i);
	}

	color /= 8.0;
}