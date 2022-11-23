Shader "Shadero Customs/Flag"
{
Properties
{
[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
_Color ("Tint", Color) = (1,1,1,1)
[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
_Outline_Size_1("_Outline_Size_1", Range(1, 16)) = 0.116
_Outline_Color_1("_Outline_Color_1", COLOR) = (0,0,0,1)
_Outline_HDR_1("_Outline_HDR_1", Range(0, 2)) = 2
_ColorHSV_Hue_1("_ColorHSV_Hue_1", Range(0, 360)) = 180
_ColorHSV_Saturation_1("_ColorHSV_Saturation_1", Range(0, 2)) = 1
_ColorHSV_Brightness_1("_ColorHSV_Brightness_1", Range(0, 2)) = 1
_SpriteFade("SpriteFade", Range(0, 1)) = 1.0

}

SubShader
{
Tags
{
"Queue" = "Transparent"
"IgnoreProjector" = "True"
"RenderType" = "Transparent"
"PreviewType" = "Plane"
"CanUseSpriteAtlas" = "True"

}

Cull Off
Lighting Off
ZWrite Off
Blend SrcAlpha OneMinusSrcAlpha


CGPROGRAM

#pragma surface surf Lambert vertex:vert  nolightmap nodynlightmap keepalpha noinstancing
#pragma multi_compile _ PIXELSNAP_ON
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
#include "UnitySprites.cginc"
struct Input
{
half2 uv_MainTex;
half4 color;
};

half _SpriteFade;
half _Outline_Size_1;
half4 _Outline_Color_1;
half _Outline_HDR_1;
half _ColorHSV_Hue_1;
half _ColorHSV_Saturation_1;
half _ColorHSV_Brightness_1;

void vert(inout appdata_full v, out Input o)
{
v.vertex.xy *= _Flip.xy;
#if defined(PIXELSNAP_ON)
v.vertex = UnityPixelSnap (v.vertex);
#endif
UNITY_INITIALIZE_OUTPUT(Input, o);
o.color = v.color * _Color * _RendererColor;
}


half4 OutLine(half2 uv,sampler2D source, half value, half4 color, half HDR)
{

value*=0.01;
half4 mainColor = tex2D(source, uv + half2(-value, value))
+ tex2D(source, uv + half2(value, -value))
+ tex2D(source, uv + half2(value, value))
+ tex2D(source, uv - half2(value, value));

color *= HDR;
mainColor.rgb = color;
half4 addcolor = tex2D(source, uv);
if (mainColor.a > 0.40) { mainColor = color; }
if (addcolor.a > 0.40) { mainColor = addcolor; mainColor.a = addcolor.a; }
return mainColor;
}
half4 ColorHSV(half4 RGBA, half HueShift, half Sat, half Val)
{

half4 RESULT = half4(RGBA);
half a1 = Val*Sat;
half a2 = HueShift*3.14159265 / 180;
half VSU = a1*cos(a2);
half VSW = a1*sin(a2);

RESULT.x = (.299*Val + .701*VSU + .168*VSW)*RGBA.x
+ (.587*Val - .587*VSU + .330*VSW)*RGBA.y
+ (.114*Val - .114*VSU - .497*VSW)*RGBA.z;

RESULT.y = (.299*Val - .299*VSU - .328*VSW)*RGBA.x
+ (.587*Val + .413*VSU + .035*VSW)*RGBA.y
+ (.114*Val - .114*VSU + .292*VSW)*RGBA.z;

RESULT.z = (.299*Val - .3*VSU + 1.25*VSW)*RGBA.x
+ (.587*Val - .588*VSU - 1.05*VSW)*RGBA.y
+ (.114*Val + .886*VSU - .203*VSW)*RGBA.z;

return RESULT;
}
void surf(Input i, inout SurfaceOutput o)
{
half4 _Outline_1 = OutLine(i.uv_MainTex,_MainTex,_Outline_Size_1,_Outline_Color_1,_Outline_HDR_1);
half4 _ColorHSV_1 = ColorHSV(_Outline_1,_ColorHSV_Hue_1,_ColorHSV_Saturation_1,_ColorHSV_Brightness_1);
half4 FinalResult = _ColorHSV_1;
o.Albedo = FinalResult.rgb* i.color.rgb;
o.Alpha = FinalResult.a * _SpriteFade * i.color.a;
clip(o.Alpha - 0.05);
}

ENDCG
}
Fallback "Sprites /Default"
}
