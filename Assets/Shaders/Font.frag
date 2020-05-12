#version 440 core

layout(location = 0) out vec4 color;

in vec3 v_UV;
in vec4 v_Color;

uniform sampler2DArray msdf;

float median(float r, float g, float b)
{
	return max(min(r, g), min(max(r, g), b));
}

void main()
{
	vec3 s = texture(msdf, v_UV).rgb;
	ivec2 sz = textureSize( msdf, 0 ).xy;
	float dx = dFdx( v_UV.x ) * sz.x;
	float dy = dFdy( v_UV.y ) * sz.y;
	float toPixels = 12.0 * inversesqrt( dx * dx + dy * dy );
	float sigDist = median( s.r, s.g, s.b ) - 0.5;
	float opacity = clamp( sigDist * toPixels + 0.5, 0.0, 1.0 );

	color = mix(vec4(0), v_Color, opacity);
	
//	vec2 msdfUnit = 12.0 / vec2(textureSize(msdf, 0));
//	float sigDist = median(s.r, s.g, s.b) - 0.5;
//	sigDist *= dot(vec3(msdfUnit, 0.0), vec3(0.5 / fwidth(v_UV.xy), 0.0));
//	float opacity = clamp(sigDist + 0.5, 0.0, 1.0);
//	color = mix(vec4(0), v_Color, opacity);
}
