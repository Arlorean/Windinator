// The MIT License
// Copyright © 2019 Inigo Quilez
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// 2D SDF Primitives:
// https://iquilezles.org/www/articles/distfunctions2d/distfunctions2d.htm
// https://www.shadertoy.com/playlist/MXdSRf

// https://www.shadertoy.com/view/4llXD7
// b.x = width
// b.y = height
// r.x = roundness top-right  
// r.y = roundness boottom-right
// r.z = roundness top-left
// r.w = roundness bottom-left
float sdRoundedBox( float2 p, float2 b, float4 r )
{
    r.xy = (p.x > 0.0) ? r.xy : r.zw;
    r.x = (p.y > 0.0) ? r.x : r.y;
    float2 q = abs(p) - b + r.x;
    return min(max(q.x, q.y), 0.0) + length(max(q, 0.0)) - r.x;
}

// https://www.shadertoy.com/view/wl23RK
// sc is the sin/cos of the aperture
// ra is the radius
// rb is the thickness of the arc either side of the radius
float sdArc( float2 p, float2 sc, float ra, float rb )
{
    p.x = abs(p.x);
    return ((sc.y*p.x > sc.x*p.y)
        ? length(p - sc*ra)
        : abs(length(p) - ra)) - rb;
}

// https://www.shadertoy.com/view/XXycDR
// sc is the sin/cos of the aperture
// r is the radius
// th is the thickness of the arc either side of the radius
// rot is the rotation matrix (anticlockwise relative to the vertical)
float sdArcRotated( float2 p, float2 sc, float r, float th, float2x2 m )
{
    p = mul(p, m);
    return sdArc(p, sc, r, th);
}