Shader "Shadero Customs/UpEvolution"
{
Properties
{
[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
_FadeToAlpha_Fade_1("_FadeToAlpha_Fade_1", Range(0, 1)) = 1
_OutlineEmpty_Size_1("_OutlineEmpty_Size_1", Range(1, 16)) = 16.0
_OutlineEmpty_Color_1("_OutlineEmpty_Color_1", COLOR) = (1,1,1,1)
_RGBA_Sub_Fade_1("_RGBA_Sub_Fade_1", Range(0, 1)) = 1
_Add_Fade_1("_Add_Fade_1", Range(0, 4)) = 4
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
half _FadeToAlpha_Fade_1;
half _OutlineEmpty_Size_1;
half4 _OutlineEmpty_Color_1;
half _RGBA_Sub_Fade_1;
half _Add_Fade_1;

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}


half4 OutLineEmpty(half2 uv,sampler2D source, half value, half4 color)
{

value*=0.01;
half4 mainColor = tex2D(source, uv + half2(-value, value))
+ tex2D(source, uv + half2(value, -value))
+ tex2D(source, uv + half2(value, value))
+ tex2D(source, uv - half2(value, value));

mainColor.rgb = color;
half4 addcolor = tex2D(source, uv);
if (mainColor.a > 0.40) { mainColor = color; }
if (addcolor.a > 0.40) { mainColor.a = 0; }
return mainColor;
}
half2 ZoomUV(half2 uv, half zoom, half posx, half posy)
{
half2 center = half2(posx, posy);
uv -= center;
uv = uv * zoom;
uv += center;
return uv;
}
half2 AnimatedMouvementUV(half2 uv, half offsetx, half offsety, half speed)
{
speed *=_Time*50;
uv += half2(offsetx, offsety)*speed;
uv = fmod(uv,1);
return uv;
}
half4 FadeToAlpha(half4 txt,half fade)
{
return half4(txt.rgb, txt.a*fade);
}

half4 frag (v2f i) : COLOR
{
half4 SourceRGBA_1 = tex2D(_MainTex, i.texcoord);
half4 FadeToAlpha_1 = FadeToAlpha(SourceRGBA_1,_FadeToAlpha_Fade_1);
half4 _OutlineEmpty_1 = OutLineEmpty(i.texcoord,_MainTex,_OutlineEmpty_Size_1,_OutlineEmpty_Color_1);
_OutlineEmpty_1.rgb -= _RGBA_Sub_Fade_1;
FadeToAlpha_1 = lerp(FadeToAlpha_1,FadeToAlpha_1*FadeToAlpha_1.a + _OutlineEmpty_1*_OutlineEmpty_1.a,_Add_Fade_1 * _OutlineEmpty_1.a);
half4 FinalResult = FadeToAlpha_1;
FinalResult.rgb *= i.color.rgb;
FinalResult.a = FinalResult.a * _SpriteFade * i.color.a;
return FinalResult;
}

ENDCG
}
}
Fallback "Sprites/Default"
}
