#include_with_pragmas "shared.cginc"

uniform float4x4 _ArcRotation; // The rotation matrix for the arc about the origin (Only float2x2 part used)
uniform float2 _ApertureSinCos = { 1, 0 }; // half circle (+/-90 degrees) { sin(90)=1, cos(90)=0 }
uniform float _LineThickness = 1; // 1 pixel either side of the arc line

static float _ArcRadius; // Calculate from rect transform, keeping aspect ratio of circle

float getSDF(float2 p)
{
    return sdArcRotated(p, _ApertureSinCos, _ArcRadius, _LineThickness, (float2x2) _ArcRotation);
}

fixed4 frag(v2f IN) : SV_Target
{
    float2 position;
    float2 halfSize;
    float2 worldPos;

    LoadData(IN, worldPos);
    GetRawRect(IN.texcoord, position, halfSize, 0);

    // Calculate radius to keep the aspect ratio
    _ArcRadius = min(halfSize.x, halfSize.y);
        
    // Signed distance field calculation
    float dist = getSDF(position);
    float norm = SDF_NORMAL(getSDF, position);

    return fragFunction(IN.texcoord, worldPos, IN.color, dist, position, halfSize, norm);
}