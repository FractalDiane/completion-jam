// -------------------------------------
// Godot 3.2 Cel Shader v2.0.0
// Adapted to Unity CG by Diane Sparks
// -------------------------------------
// Copyright (c) 2017 David E Lipps
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"),to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,and/or sell copies of the Software, and
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions
// of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Shader "Custom/Cel"
{
    Properties
    {
		_BaseColor ("Base Color", Color) = (1, 1, 1, 1)
		_ShadeColor ("Shade Color", Color) = (1, 1, 1, 1)
		_RimTint ("Rim Tint", Color) = (0.75, 0.75, 0.75, 0.75)

		_ShadeThreshold ("Shade Threshold", Range (-1.0, 1.0)) = 0.0
		_ShadeSoftness ("Shade Softness", Range (0.0, 1.0)) = 0.01

		[Toggle]
		_UseRim ("Use Rim", Float) = 0
		_RimThreshold ("Rim Threshold", Range (0.0, 1.0)) = 0
		_RimSoftness ("Rim Softness", Range (0.0, 1.0)) = 0.05
		_RimSpread ("Rim Spread", Range (0.0, 1.0)) = 0.5

		_ShadowThreshold ("Shadow Threshold", Range (0.0, 1.0)) = 0.7
		_ShadowSoftness ("Shadow Softness", Range (0.0, 1.0)) = 0.1

        _BaseTex ("Base Texture", 2D) = "white" {}
		_ShadeTex ("Shade Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
			Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
			

            #include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			
			#pragma multi_compile_fwdbase

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;

				float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 pos : SV_POSITION;

				float3 worldNormal : NORMAL;
				float3 viewDir : TEXCOORD1;

				//SHADOW_COORDS(2)
				LIGHTING_COORDS(2, 3)
            };

            sampler2D _BaseTex;
            float4 _BaseTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                o.uv = TRANSFORM_TEX(v.uv, _BaseTex);
                UNITY_TRANSFER_FOG(o,o.pos);

				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = WorldSpaceViewDir(v.vertex);

				//TRANSFER_SHADOW(o);
				TRANSFER_VERTEX_TO_FRAGMENT(o);

                return o;
            }

			fixed4 _BaseColor;
			fixed4 _ShadeColor;
			fixed4 _RimTint;

			float _ShadeThreshold;
			float _ShadeSoftness;

			float _UseRim;
			float _RimThreshold;
			float _RimSoftness;
			float _RimSpread;

			float _ShadowThreshold;
			float _ShadowSoftness;

			sampler2D _ShadeTex;

            fixed4 frag (v2f i) : SV_Target
            {
				float3 normal = normalize(i.worldNormal);

                float NdotL = dot(normal, _WorldSpaceLightPos0);
				float is_lit = step(_ShadeThreshold, NdotL);
				fixed4 base = tex2D(_BaseTex, i.uv) * _BaseColor;
				fixed4 shade = tex2D(_ShadeTex, i.uv) * _ShadeColor;
				fixed4 diffuse = base;

				float shade_value = smoothstep(_ShadeThreshold - _ShadeSoftness, _ShadeThreshold + _ShadeSoftness, NdotL);
				diffuse = lerp(shade, base, shade_value);

				float shadow_value = smoothstep(_ShadowThreshold - _ShadowSoftness, _ShadowThreshold + _ShadowSoftness, LIGHT_ATTENUATION(i));
				shade_value = min(shade_value, shadow_value);
				diffuse = lerp(shade, base, shade_value);
				is_lit = step(_ShadowThreshold, shade_value);

				return diffuse;
            }
            ENDCG
        }
    }

	Fallback "VertexLit"
}
