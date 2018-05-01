// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/GradientShader" {
	Properties{
		_FColor("Color 1", Color) = (1,1,1,1)
		_SColor("Color 2", Color) = (1,1,1,1)
	}
		SubShader{

			Pass{
			CGPROGRAM

			#pragma vertex vert             
			#pragma fragment frag
			#include "UnityCG.cginc"
			fixed4 _FColor;
			fixed4 _SColor;

			struct vertOutput {
				float4 pos : SV_POSITION;
				float4  uv : TEXCOORD0;
			};

			vertOutput vert(appdata_base input) {
				vertOutput o;
				o.pos = UnityObjectToClipPos(input.vertex);
				o.uv = input.texcoord;
				return o;
			}

			float4 frag(vertOutput output) : COLOR{
				return lerp(_FColor,_SColor, output.uv.y);
			}
			ENDCG
		}
	}
}
