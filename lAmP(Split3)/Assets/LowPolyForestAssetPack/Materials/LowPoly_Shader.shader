// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader_LowPoly"
{
	Properties
	{
		_Roughness("Roughness", Float) = 0.8
		_ColorVariationTexture("Color Variation Texture", 2D) = "white" {}
		_ColorTexture("Color Texture", 2D) = "white" {}
		[Toggle]_VertexColorVariation("Vertex Color Variation", Int) = 0
		_SnowContrast("Snow Contrast", Float) = 15
		_SnowContrast2("Snow Contrast 2", Float) = 10
		[Toggle]_Snow("Snow", Int) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma shader_feature _VERTEXCOLORVARIATION_ON
		#pragma shader_feature _SNOW_ON
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
			float3 worldNormal;
		};

		uniform sampler2D _ColorTexture;
		uniform float4 _ColorTexture_ST;
		uniform sampler2D _ColorVariationTexture;
		uniform float4 _ColorVariationTexture_ST;
		uniform float _SnowContrast;
		uniform float _SnowContrast2;
		uniform float _Roughness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_ColorTexture = i.uv_texcoord * _ColorTexture_ST.xy + _ColorTexture_ST.zw;
			float4 tex2DNode7 = tex2D( _ColorTexture, uv_ColorTexture );
			float2 uv_ColorVariationTexture = i.uv_texcoord * _ColorVariationTexture_ST.xy + _ColorVariationTexture_ST.zw;
			float4 lerpResult5 = lerp( tex2DNode7 , tex2D( _ColorVariationTexture, uv_ColorVariationTexture ) , i.vertexColor);
			#ifdef _VERTEXCOLORVARIATION_ON
				float4 staticSwitch10 = lerpResult5;
			#else
				float4 staticSwitch10 = tex2DNode7;
			#endif
			float4 temp_cast_0 = (1.0).xxxx;
			float dotResult13 = dot( i.worldNormal , float3(0,1,0) );
			float clampResult23 = clamp( round( ( pow( dotResult13 , _SnowContrast ) * _SnowContrast2 ) ) , 0.0 , 1.0 );
			float4 lerpResult19 = lerp( staticSwitch10 , temp_cast_0 , clampResult23);
			#ifdef _SNOW_ON
				float4 staticSwitch21 = lerpResult19;
			#else
				float4 staticSwitch21 = staticSwitch10;
			#endif
			o.Albedo = staticSwitch21.rgb;
			o.Smoothness = _Roughness;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			# include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD6;
				float3 worldNormal : TEXCOORD1;
				float4 texcoords01 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.texcoords01 = float4( v.texcoord.xy, v.texcoord1.xy );
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord.xy = IN.texcoords01.xy;
				float3 worldPos = IN.worldPos;
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13701
7;29;1522;788;1231.852;279.1434;1;True;False
Node;AmplifyShaderEditor.Vector3Node;11;-1023.173,-343.4355;Float;False;Constant;_Vector0;Vector 0;4;0;0,1,0;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WorldNormalVector;12;-1063.474,-509.8354;Float;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.DotProductOpNode;13;-665.6738,-400.6352;Float;False;2;0;FLOAT3;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;15;-526.5731,-253.7349;Float;False;Property;_SnowContrast;Snow Contrast;4;0;15;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.PowerNode;14;-299.0738,-404.5351;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;18;-267.8731,-234.2348;Float;False;Property;_SnowContrast2;Snow Contrast 2;5;0;10;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;7;-956.8742,-104.2356;Float;True;Property;_ColorTexture;Color Texture;2;0;Assets/Textures/T_Colorlookup_Summer.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.VertexColorNode;3;-798.6744,269.0646;Float;False;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-108.9674,-389.2661;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;8;-962.0753,84.26434;Float;True;Property;_ColorVariationTexture;Color Variation Texture;1;0;Assets/Textures/T_Colorlookup_Vertcolors_Summer.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;5;-527.874,2.364284;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;2;COLOR;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.RoundOpNode;17;90.55235,-376.7461;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.StaticSwitch;10;-235.3736,-62.63542;Float;False;Property;_VertexColorVariation;Vertex Color Variation;3;0;0;False;True;;2;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.ClampOpNode;23;202.896,-299.2548;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;20;46.33292,-157.0957;Float;False;Constant;_Float0;Float 0;5;0;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;19;478.837,-266.7967;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StaticSwitch;21;647.7482,-113.4692;Float;False;Property;_Snow;Snow;6;0;0;False;True;;2;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;9;483.5256,140.1645;Float;False;Property;_Roughness;Roughness;0;0;0.8;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;837.1999,52;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader_LowPoly;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;13;0;12;0
WireConnection;13;1;11;0
WireConnection;14;0;13;0
WireConnection;14;1;15;0
WireConnection;16;0;14;0
WireConnection;16;1;18;0
WireConnection;5;0;7;0
WireConnection;5;1;8;0
WireConnection;5;2;3;0
WireConnection;17;0;16;0
WireConnection;10;0;5;0
WireConnection;10;1;7;0
WireConnection;23;0;17;0
WireConnection;19;0;10;0
WireConnection;19;1;20;0
WireConnection;19;2;23;0
WireConnection;21;0;19;0
WireConnection;21;1;10;0
WireConnection;0;0;21;0
WireConnection;0;4;9;0
ASEEND*/
//CHKSM=1DA33991810B6409AA2D79F01675006B9789424D