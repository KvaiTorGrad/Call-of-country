//////////////////////////////////////////////////////////////
/// Shadero Sprite: Sprite Shader Editor - by VETASOFT 2020 //
/// Shader generate with Shadero 1.9.9                      //
/// http://u3d.as/V7t #AssetStore                           //
/// http://www.shadero.com #Docs                            //
//////////////////////////////////////////////////////////////

Shader "Shadero Previews/PreviewXATXQ1"
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
_OutlineEmpty_Size_1("_OutlineEmpty_Size_1", Range(1, 16)) = 16.0
_OutlineEmpty_Color_1("_OutlineEmpty_Color_1", COLOR) = (1,1,1,1)
_Mask2RGBA_Fade_1("_Mask2RGBA_Fade_1", Range(0, 1)) = 1
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
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};

struct v2f
{
float2 texcoord  : TEXCOORD0;
float4 vertex   : SV_POSITION;
float4 color    : COLOR;
};

sampler2D _MainTex;
float _SpriteFade;
float ResizeUV_X_1;
float ResizeUV_Y_1;
float ResizeUV_ZoomX_1;
float ResizeUV_ZoomY_1;
float ZoomUV_Zoom_1;
float ZoomUV_PosX_1;
float ZoomUV_PosY_1;
sampler2D _NewTex_2;
float AnimatedPingPongOffsetUV_1_OffsetX_1;
float AnimatedPingPongOffsetUV_1_OffsetY_1;
float AnimatedPingPongOffsetUV_1_ZoomX_1;
float AnimatedPingPongOffsetUV_1_ZoomY_1;
float AnimatedPingPongOffsetUV_1_Speed_1;
float DistortionUV_WaveX_1;
float DistortionUV_WaveY_1;
float DistortionUV_DistanceX_1;
float DistortionUV_DistanceY_1;
float DistortionUV_Speed_1;
sampler2D _NewTex_1;
float _OutlineEmpty_Size_1;
float4 _OutlineEmpty_Color_1;
float _Mask2RGBA_Fade_1;

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}


float2 AnimatedPingPongOffsetUV(float2 uv, float offsetx, float offsety, float zoomx, float zoomy, float speed)
{
float time = sin(_Time * 100* speed)  * 0.1;
speed *= time * 25;
uv += float2(offsetx, offsety)*speed;
uv = uv * float2(zoomx, zoomy);
return uv;
}
float2 DistortionUV(float2 p, float WaveX, float WaveY, float DistanceX, float DistanceY, float Speed)
{
Speed *=_Time*100;
p.x= p.x+sin(p.y*WaveX + Speed)*DistanceX*0.05;
p.y= p.y+cos(p.x*WaveY + Speed)*DistanceY*0.05;
return p;
}
float4 OutLineEmpty(float2 uv,sampler2D source, float value, float4 color)
{

value*=0.01;
float4 mainColor = tex2D(source, uv + float2(-value, value))
+ tex2D(source, uv + float2(value, -value))
+ tex2D(source, uv + float2(value, value))
+ tex2D(source, uv - float2(value, value));

mainColor.rgb = color;
float4 addcolor = tex2D(source, uv);
if (mainColor.a > 0.40) { mainColor = color; }
if (addcolor.a > 0.40) { mainColor.a = 0; }
return mainColor;
}
float2 ZoomUV(float2 uv, float zoom, float posx, float posy)
{
float2 center = float2(posx, posy);
uv -= center;
uv = uv * zoom;
uv += center;
return uv;
}
float2 ResizeUV(float2 uv, float offsetx, float offsety, float zoomx, float zoomy)
{
uv += float2(offsetx, offsety);
uv = fmod(uv * float2(zoomx*zoomx, zoomy*zoomy), 1);
return uv;
}

float2 ResizeUVClamp(float2 uv, float offsetx, float offsety, float zoomx, float zoomy)
{
uv += float2(offsetx, offsety);
uv = fmod(clamp(uv * float2(zoomx*zoomx, zoomy*zoomy), 0.0001, 0.9999), 1);
return uv;
}
float4 frag (v2f i) : COLOR
{
float2 ResizeUV_1 = ResizeUV(i.texcoord,ResizeUV_X_1,ResizeUV_Y_1,ResizeUV_ZoomX_1,ResizeUV_ZoomY_1);
float2 ZoomUV_1 = ZoomUV(ResizeUV_1,ZoomUV_Zoom_1,ZoomUV_PosX_1,ZoomUV_PosY_1);
float4 NewTex_2 = tex2D(_NewTex_2,ZoomUV_1);
float2 AnimatedPingPongOffsetUV_1 = AnimatedPingPongOffsetUV(i.texcoord,AnimatedPingPongOffsetUV_1_OffsetX_1,AnimatedPingPongOffsetUV_1_OffsetY_1,AnimatedPingPongOffsetUV_1_ZoomX_1,AnimatedPingPongOffsetUV_1_ZoomY_1,AnimatedPingPongOffsetUV_1_Speed_1);
float2 DistortionUV_1 = DistortionUV(AnimatedPingPongOffsetUV_1,DistortionUV_WaveX_1,DistortionUV_WaveY_1,DistortionUV_DistanceX_1,DistortionUV_DistanceY_1,DistortionUV_Speed_1);
float4 NewTex_1 = tex2D(_NewTex_1,DistortionUV_1);
NewTex_1.a = NewTex_2.a;
NewTex_1.a = NewTex_2.a;
float4 _OutlineEmpty_1 = OutLineEmpty(i.texcoord,_MainTex,_OutlineEmpty_Size_1,_OutlineEmpty_Color_1);
float4 Mask2RGBA_1 = lerp(NewTex_1,NewTex_1, lerp(_OutlineEmpty_1.r, 1 - _OutlineEmpty_1.r ,_Mask2RGBA_Fade_1));
float4 FinalResult = Mask2RGBA_1;
FinalResult.rgb *= i.color.rgb;
FinalResult.a = FinalResult.a * _SpriteFade * i.color.a;
return FinalResult;
}

ENDCG
}
}
Fallback "Sprites/Default"
}
