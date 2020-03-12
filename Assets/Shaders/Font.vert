#version 440 core

layout(location = 0) in vec3 a_Position;
layout(location = 1) in vec3 a_Normal;
layout(location = 2) in vec3 a_UV;
layout(location = 3) in vec4 a_Color;

uniform mat4 u_ViewProjection;
uniform vec2 u_ViewportSize;

out vec3 v_UV;
out vec4 v_Color;

void main()
{
	vec4 cpos = u_ViewProjection * vec4(a_Position, 1.0);
	vec2 p = floor(cpos.xy * u_ViewportSize * (0.5 / cpos.w));

	if (mod(u_ViewportSize.x, 2.0) != 0.0) p.x += 0.5;
	if (mod(u_ViewportSize.y, 2.0) != 0.0) p.y += 0.5;

	cpos.xy = (p * cpos.w) * (2.0 / u_ViewportSize);
	
	gl_Position = cpos;

	v_UV = a_UV;
	v_Color = a_Color;
}