//////////////////////////////////////////////////////////////
/// Shadero Sprite: Sprite Shader Editor - by VETASOFT 2020 //
/// Shader generate with Shadero 1.9.9                      //
/// http://u3d.as/V7t #AssetStore                           //
/// http://www.shadero.com #Docs                            //
//////////////////////////////////////////////////////////////

Shader "Shadero Previews/PreviewXATXQ4"
{
Properties
{
[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
_FadeToAlpha_Fade_1("_FadeToAlpha_Fade_1", Range(0, 1)) = 1
_TintRGBA_Color_1("_TintRGBA_Color_1", COLOR) = (0,0,0,1)
_OutlineEmpty_Size_1("_OutlineEmpty_Size_1", Range(1, 16)) = 16.0
_OutlineEmpty_Color_1("_OutlineEmpty_Color_1", COLOR) = (1,1,1,1)
_ClippingUp_Value_1("_ClippingUp_Value_1", Range(0, 1)) = 1
_RGBA_Sub_Fade_1("_RGBA_Sub_Fade_1", Range(0, 2)) = 0
_BlurHQ_Intensity_1("_BlurHQ_Intensity_1", Range(1, 16)) = 0
_MaskAlpha_Fade_1("_MaskAlpha_Fade_1", Range(0, 1)) = 1
_Add_Fade_1("_Add_Fade_1", Range(0, 4)) = 4
AnimatedMouvementUV_X_1("AnimatedMouvementUV_X_1", Range(-1, 1)) = 0
AnimatedMouvementUV_Y_1("AnimatedMouvementUV_Y_1", Range(-1, 1)) = 1
AnimatedMouvementUV_Speed_1("AnimatedMouvementUV_Speed_1", Range(-1, 1)) = 0.1
ZoomUV_Zoom_1("ZoomUV_Zoom_1", Range(0.2, 4)) = 2.767
ZoomUV_PosX_1("ZoomUV_PosX_1", Range(-3, 3)) = 0.5
ZoomUV_PosY_1("ZoomUV_PosY_1", Range(-3, 3)) =1.384
SpriteSheetFrameUV_Size_1("SpriteSheetFrameUV_Size_1", Range(2, 16)) = 16
SpriteSheetFrameUV_Frame_1("SpriteSheetFrameUV_Frame_1", Range(0, 255)) = 108
_NewTex_1("NewTex_1(RGB)", 2D) = "white" { }
_MaskRGBA_Fade_1("_MaskRGBA_Fade_1", Range(0, 1)) = 0
_Add_Fade_2("_Add_Fade_2", Range(0, 4)) = 4
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
float _FadeToAlpha_Fade_1;
float4 _TintRGBA_Color_1;
float _OutlineEmpty_Size_1;
float4 _OutlineEmpty_Color_1;
float _ClippingUp_Value_1;
float _RGBA_Sub_Fade_1;
float _BlurHQ_Intensity_1;
float _MaskAlpha_Fade_1;
float _Add_Fade_1;
float AnimatedMouvementUV_X_1;
float AnimatedMouvementUV_Y_1;
float AnimatedMouvementUV_Speed_1;
float ZoomUV_Zoom_1;
float ZoomUV_PosX_1;
float ZoomUV_PosY_1;
float SpriteSheetFrameUV_Size_1;
float SpriteSheetFrameUV_Frame_1;
sampler2D _NewTex_1;
float _MaskRGBA_Fade_1;
float _Add_Fade_2;

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}


float4 BlurHQ(float2 uv, sampler2D source, float Intensity)
{
float step1 = 0.00390625f * Intensity * 0.5;
float step2 = step1 * 2;
float4 result = float4 (0, 0, 0, 0);
float2 texCoord = float2(0, 0);
texCoord = uv + float2(-step2, -step2); result += tex2D(source, texCoord);
texCoord = uv + float2(-step1, -step2); result += 4.0 * tex2D(source, texCoord);
texCoord = uv + float2(0, -step2); result += 6.0 * tex2D(source, texCoord);
texCoord = uv + float2(step1, -step2); result += 4.0 * tex2D(source, texCoord);
texCoord = uv + float2(step2, -step2); result += tex2D(source, texCoord);
texCoord = uv + float2(-step2, -step1); result += 4.0 * tex2D(source, texCoord);
texCoord = uv + float2(-step1, -step1); result += 16.0 * tex2D(source, texCoord);
texCoord = uv + float2(0, -step1); result += 24.0 * tex2D(source, texCoord);
texCoord = uv + float2(step1, -step1); result += 16.0 * tex2D(source, texCoord);
texCoord = uv + float2(step2, -step1); result += 4.0 * tex2D(source, texCoord);
texCoord = uv + float2(-step2, 0); result += 6.0 * tex2D(source, texCoord);
texCoord = uv + float2(-step1, 0); result += 24.0 * tex2D(source, texCoord);
texCoord = uv; result += 36.0 * tex2D(source, texCoord);
texCoord = uv + float2(step1, 0); result += 24.0 * tex2D(source, texCoord);
texCoord = uv + float2(step2, 0); result += 6.0 * tex2D(source, texCoord);
texCoord = uv + float2(-step2, step1); result += 4.0 * tex2D(source, texCoord);
texCoord = uv + float2(-step1, step1); result += 16.0 * tex2D(source, texCoord);
texCoord = uv + float2(0, step1); result += 24.0 * tex2D(source, texCoord);
texCoord = uv + float2(step1, step1); result += 16.0 * tex2D(source, texCoord);
texCoord = uv + float2(step2, step1); result += 4.0 * tex2D(source, texCoord);
texCoord = uv + float2(-step2, step2); result += tex2D(source, texCoord);
texCoord = uv + float2(-step1, step2); result += 4.0 * tex2D(source, texCoord);
texCoord = uv + float2(0, step2); result += 6.0 * tex2D(source, texCoord);
texCoord = uv + float2(step1, step2); result += 4.0 * tex2D(source, texCoord);
texCoord = uv + float2(step2, step2); result += tex2D(source, texCoord);
result = result*0.00390625;
return result;
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
float4 ClippingUp(float4 txt, float2 uv, float value)
{
float4 tex = txt;
if (uv.y > value) tex = float4(0, 0, 0, 0);
return tex;
}
float4 TintRGBA(float4 txt, float4 color)
{
float3 tint = dot(txt.rgb, float3(.222, .707, .071));
tint.rgb *= color.rgb;
txt.rgb = lerp(txt.rgb,tint.rgb,color.a);
return txt;
}
float2 ZoomUV(float2 uv, float zoom, float posx, float posy)
{
float2 center = float2(posx, posy);
uv -= center;
uv = uv * zoom;
uv += center;
return uv;
}
float2 AnimatedMouvementUV(float2 uv, float offsetx, float offsety, float speed)
{
speed *=_Time*50;
uv += float2(offsetx, offsety)*speed;
uv = fmod(uv,1);
return uv;
}
float2 SpriteSheetFrame(float2 uv, float size, float frame)
{
frame = int(frame);
uv /= size;
uv.x += fmod(frame,size) / size;
uv.y -=1/size;
uv.y += 1-floor(frame / size) / size;
return uv;
}
float4 FadeToAlpha(float4 txt,float fade)
{
return float4(txt.rgb, txt.a*fade);
}

float4 frag (v2f i) : COLOR
{
float4 SourceRGBA_1 = tex2D(_MainTex, i.texcoord);
float4 FadeToAlpha_1 = FadeToAlpha(SourceRGBA_1,_FadeToAlpha_Fade_1);
float4 TintRGBA_1 = TintRGBA(FadeToAlpha_1,_TintRGBA_Color_1);
float4 _OutlineEmpty_1 = OutLineEmpty(i.texcoord,_MainTex,_OutlineEmpty_Size_1,_OutlineEmpty_Color_1);
float4 ClippingUp_1 = ClippingUp(_OutlineEmpty_1,i.texcoord,_ClippingUp_Value_1);
ClippingUp_1.rgb -= _RGBA_Sub_Fade_1;
float4 _BlurHQ_1 = BlurHQ(i.texcoord,_MainTex,_BlurHQ_Intensity_1);
float4 MaskAlpha_1=ClippingUp_1;
MaskAlpha_1.a = lerp(_BlurHQ_1.a * ClippingUp_1.a, (1 - _BlurHQ_1.a) * ClippingUp_1.a,_MaskAlpha_Fade_1);
TintRGBA_1 = lerp(TintRGBA_1,TintRGBA_1*TintRGBA_1.a + MaskAlpha_1*MaskAlpha_1.a,_Add_Fade_1 * MaskAlpha_1.a);
float2 AnimatedMouvementUV_1 = AnimatedMouvementUV(i.texcoord,AnimatedMouvementUV_X_1,AnimatedMouvementUV_Y_1,AnimatedMouvementUV_Speed_1);
float2 ZoomUV_1 = ZoomUV(AnimatedMouvementUV_1,ZoomUV_Zoom_1,ZoomUV_PosX_1,ZoomUV_PosY_1);
float2 SpriteSheetFrameUV_1 = SpriteSheetFrame(ZoomUV_1,SpriteSheetFrameUV_Size_1,SpriteSheetFrameUV_Frame_1);
float4 NewTex_1 = tex2D(_NewTex_1,SpriteSheetFrameUV_1);
float4 MaskRGBA_1=SourceRGBA_1;
MaskRGBA_1.a = lerp(NewTex_1.r * SourceRGBA_1.a, (1 - NewTex_1.r) * SourceRGBA_1.a,_MaskRGBA_Fade_1);
TintRGBA_1 = lerp(TintRGBA_1,TintRGBA_1*TintRGBA_1.a + MaskRGBA_1*MaskRGBA_1.a,_Add_Fade_2 * TintRGBA_1.a);
float4 FinalResult = TintRGBA_1;
FinalResult.rgb *= i.color.rgb;
FinalResult.a = FinalResult.a * _SpriteFade * i.color.a;
return FinalResult;
}

ENDCG
}
}
Fallback "Sprites/Default"
}
