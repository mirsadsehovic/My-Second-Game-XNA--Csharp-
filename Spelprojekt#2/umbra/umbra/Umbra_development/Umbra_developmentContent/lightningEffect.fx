texture lightMask;


sampler mainSampler : register(s0);
sampler lightSampler = sampler_state
{
	Texture = lightMask;};
	/*magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
	AddressU = mirror;
	AddressV = mirror;*/


struct PixelShaderInput
{
    float4 TextureCoords: TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
	float4 Color : COLOR;
};

float4 ambientColor = float4(1.0,1.0,1.0,0.0);
float ambientIntensity = 0.3;

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
        float2 texCoord = input.TexCoord;

        float4 mainColor = tex2D(mainSampler, texCoord);
        float4 lightColor = tex2D(lightSampler, texCoord)*5;
		
	float4 color = ambientIntensity*ambientColor;
	float dist;

	float lightRadius = 0.2f;
	float lightIntensity = 15.0f;
	float2 lightPos = float2(0.3f,0.5f);
	float4 _lightColor = float4(1,1,1,1);

	dist = distance(lightPos, input.TexCoord);
	color += saturate((lightRadius -dist)* lightIntensity)*_lightColor;

	//mainColor = saturate(color)*mainColor;

	//return mainColor;
    return mainColor * lightColor*mainColor;
}


technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}