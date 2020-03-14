#version 440 core

layout(location = 0) out vec4 color;

in vec3 v_UV;
in vec4 v_Color;

uniform float u_Radius = 1f;
uniform float u_Thickness = 1f;

void main()
{
	vec2 uv = v_UV.xy - 0.5;

	float rad1 = u_Radius * 0.5;
	float rad2 = u_Thickness * 0.5;
	float dist = length(uv);

	if (dist > rad1 || dist < rad2) discard;

	float alpha = smoothstep(rad2 - 0.1, rad2, dist);
	color = vec4(v_Color.xyz, v_Color.a * alpha);
}