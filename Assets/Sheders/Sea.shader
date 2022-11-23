Shader "Shadero Customs/Sea"
{
Properties
{
[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
ResizeUV_X_1("ResizeUV_X_1", Range(-1, 1)) = 0.371
ResizeUV_Y_1("ResizeUV_Y_1", Range(-1, 1)) = 0
ResizeUV_ZoomX_1("ResizeUV_ZoomX_1", Range(0.1, 3)) = 0.722
ResizeUV_ZoomY_1("ResizeUV_ZoomY_1", Range(0.1, 3)) = 1
ZoomUV_Zoom_1("ZoomUV_Zoom_1", Range(0.2, 4)) = 1.154
ZoomUV_PosX_1("ZoomUV_PosX_1", Range(-3, 3)) = 0.169
ZoomUV_PosY_1("ZoomUV_PosY_1", Range(-3, 3)) =0.5
_NewTex_2("NewTex_2(RGB)", 2D) = "white" { }
AnimatedPingPongOffsetUV_1_OffsetX_1("AnimatedPingPongOffsetUV_1_OffsetX_1", Range(-1, 1)) = -0.313
AnimatedPingPongOffsetUV_1_OffsetY_1("AnimatedPingPongOffsetUV_1_OffsetY_1", Range(-1, 1)) = 0.204
AnimatedPingPongOffsetUV_1_ZoomX_1("AnimatedPingPongOffsetUV_1_ZoomX_1", Range(1, 10)) = 1
AnimatedPingPongOffsetUV_1_ZoomY_1("AnimatedPingPongOffsetUV_1_ZoomY_1", Range(1, 10)) = 2.375
AnimatedPingPongOffsetUV_1_Speed_1("AnimatedPingPongOffsetUV_1_Speed_1", Range(-1, 1)) = 0.144
DistortionUV_WaveX_1("DistortionUV_WaveX_1", Range(0, 128)) = 81.92
DistortionUV_WaveY_1("DistortionUV_WaveY_1", Range(0, 128)) = 49.571
DistortionUV_DistanceX_1("DistortionUV_DistanceX_1", Range(0, 1)) = 0.536
DistortionUV_DistanceY_1("DistortionUV_DistanceY_1", Range(0, 1)) = 0.144
DistortionUV_Speed_1("DistortionUV_Speed_1", Range(-2, 2)) = 0.625
_NewTex_1("NewTex_1(RGB)", 2D) = "white" { }
_AlphaIntensity_Fade_1("_AlphaIntensity_Fade_1", Range(0, 6)) = 1.546
_InnerGlowHQ_Intensity_1("_InnerGlowHQ_Intensity_1", Range(1, 16)) = 0
_InnerGlowHQ_Size_1("_InnerGlowHQ_Size_1", Range(1, 16)) = 15.811
_InnerGlowHQ_Color_1("_InnerGlowHQ_Color_1", COLOR) = (0,0.4284544,1,1)
_AlphaAsAura_Fade_1("_AlphaAsAura_Fade_1", Range(0, 1)) = 0.131
_SpriteFade("SpriteFade", Range(0, 1)) = 1.0

// required for UI.Mask
[HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
[HideInInspector]_Stencil("Stencil ID", Float) = 0
[HideInInspector]_StencilOp("Stencil Operation", Float) = 0
[HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
[HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255
[HideInInspector]_ColorMask("Color Mask", Float) = 15

}

SubShader
{

Tags {"Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off 

// required for UI.Mask
Stencil
{
Ref [_Stencil]
Comp [_StencilComp]
Pass [_StencilOp]
ReadMask [_StencilReadMask]
WriteMask [_StencilWriteMask]
}

Pass
{

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

struct appdata_t{
half4 vertex   : POSITION;
half4 color    : COLOR;
half2 texcoord : TEXCOORD0;
};

struct v2f
{
half2 texcoord  : TEXCOORD0;
half4 vertex   : SV_POSITION;
half4 color    : COLOR;
};

sampler2D _MainTex;
half _SpriteFade;
half ResizeUV_X_1;
half ResizeUV_Y_1;
half ResizeUV_ZoomX_1;
half ResizeUV_ZoomY_1;
half ZoomUV_Zoom_1;
half ZoomUV_PosX_1;
half ZoomUV_PosY_1;
sampler2D _NewTex_2;
half AnimatedPingPongOffsetUV_1_OffsetX_1;
half AnimatedPingPongOffsetUV_1_OffsetY_1;
half AnimatedPingPongOffsetUV_1_ZoomX_1;
half AnimatedPingPongOffsetUV_1_ZoomY_1;
half AnimatedPingPongOffsetUV_1_Speed_1;
half DistortionUV_WaveX_1;
half DistortionUV_WaveY_1;
half DistortionUV_DistanceX_1;
half DistortionUV_DistanceY_1;
half DistortionUV_Speed_1;
sampler2D _NewTex_1;
half _AlphaIntensity_Fade_1;
half _InnerGlowHQ_Intensity_1;
half _InnerGlowHQ_Size_1;
half4 _InnerGlowHQ_Color_1;
half _AlphaAsAura_Fade_1;

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}


half2 AnimatedPingPongOffsetUV(half2 uv, half offsetx, half offsety, half zoomx, half zoomy, half speed)
{
half time = sin(_Time * 100* speed)  * 0.1;
speed *= time * 25;
uv += half2(offsetx, offsety)*speed;
uv = uv * half2(zoomx, zoomy);
return uv;
}
half2 DistortionUV(half2 p, half WaveX, half WaveY, half DistanceX, half DistanceY, half Speed)
{
Speed *=_Time*100;
p.x= p.x+sin(p.y*WaveX + Speed)*DistanceX*0.05;
p.y= p.y+cos(p.x*WaveY + Speed)*DistanceY*0.05;
return p;
}
half2 ZoomUV(half2 uv, half zoom, half posx, half posy)
{
half2 center = half2(posx, posy);
uv -= center;
uv = uv * zoom;
uv += center;
return uv;
}
half2 ResizeUV(half2 uv, half offsetx, half offsety, half zoomx, half zoomy)
{
uv += half2(offsetx, offsety);
uv = fmod(uv * half2(zoomx*zoomx, zoomy*zoomy), 1);
return uv;
}

half2 ResizeUVClamp(half2 uv, half offsetx, half offsety, half zoomx, half zoomy)
{
uv += half2(offsetx, offsety);
uv = fmod(clamp(uv * half2(zoomx*zoomx, zoomy*zoomy), 0.0001, 0.9999), 1);
return uv;
}
half InnerGlowAlpha(sampler2D source, half2 uv)
{
return (1 - tex2D(source, uv).a);
}
half4 InnerGlow(half2 uv, sampler2D source, half Intensity, half size, half4 color)
{
half step1 = 0.00390625f * size*2;
half step2 = step1 * 2;
half4 result = half4 (0, 0, 0, 0);
half2 texCoord = half2(0, 0);
texCoord = uv + half2(-step2, -step2); result += InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(-step1, -step2); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(0, -step2); result += 6.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(step1, -step2); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(step2, -step2); result += InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(-step2, -step1); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(-step1, -step1); result += 16.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(0, -step1); result += 24.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(step1, -step1); result += 16.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(step2, -step1); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(-step2, 0); result += 6.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(-step1, 0); result += 24.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv; result += 36.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(step1, 0); result += 24.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(step2, 0); result += 6.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(-step2, step1); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(-step1, step1); result += 16.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(0, step1); result += 24.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(step1, step1); result += 16.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(step2, step1); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(-step2, step2); result += InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(-step1, step2); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(0, step2); result += 6.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(step1, step2); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + half2(step2, step2); result += InnerGlowAlpha(source, texCoord);
result = result*0.00390625;
result = lerp(tex2D(source,uv),color*Intensity,result*color.a);
result.a = tex2D(source, uv).a;
return saturate(result);
}
half4 AlphaAsAura(half4 origin, half4 overlay, half blend)
{
half4 o = origin;
o = o.a;
if (o.a > 0.99) o = 0;
half4 aura = lerp(origin, origin + overlay, blend);
o = lerp(origin, aura, o);
return o;
}

half4 AlphaIntensity(half4 txt,half fade)
{
if (txt.a < 1) txt.a = txt.a*fade;
return txt;
}

half4 frag (v2f i) : COLOR
{
half2 ResizeUV_1 = ResizeUV(i.texcoord,ResizeUV_X_1,ResizeUV_Y_1,ResizeUV_ZoomX_1,ResizeUV_ZoomY_1);
half2 ZoomUV_1 = ZoomUV(ResizeUV_1,ZoomUV_Zoom_1,ZoomUV_PosX_1,ZoomUV_PosY_1);
half4 NewTex_2 = tex2D(_NewTex_2,ZoomUV_1);
half2 AnimatedPingPongOffsetUV_1 = AnimatedPingPongOffsetUV(i.texcoord,AnimatedPingPongOffsetUV_1_OffsetX_1,AnimatedPingPongOffsetUV_1_OffsetY_1,AnimatedPingPongOffsetUV_1_ZoomX_1,AnimatedPingPongOffsetUV_1_ZoomY_1,AnimatedPingPongOffsetUV_1_Speed_1);
half2 DistortionUV_1 = DistortionUV(AnimatedPingPongOffsetUV_1,DistortionUV_WaveX_1,DistortionUV_WaveY_1,DistortionUV_DistanceX_1,DistortionUV_DistanceY_1,DistortionUV_Speed_1);
half4 NewTex_1 = tex2D(_NewTex_1,DistortionUV_1);
NewTex_1.a = NewTex_2.a;
half4 AlphaIntensity_1 = AlphaIntensity(NewTex_1,_AlphaIntensity_Fade_1);
NewTex_1.a = NewTex_2.a;
half2 rgba_uv_1 = half2(NewTex_1.r,NewTex_1.g);
half4 _InnerGlowHQ_1 = InnerGlow(rgba_uv_1,_MainTex,_InnerGlowHQ_Intensity_1,_InnerGlowHQ_Size_1,_InnerGlowHQ_Color_1);
half4 AlphaAsAura_1 = AlphaAsAura(AlphaIntensity_1, _InnerGlowHQ_1, _AlphaAsAura_Fade_1); 
half4 FinalResult = AlphaAsAura_1;
FinalResult.rgb *= i.color.rgb;
FinalResult.a = FinalResult.a * _SpriteFade * i.color.a;
return FinalResult;
}

ENDCG
}
}
Fallback "Sprites/Default"
}
