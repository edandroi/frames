// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "EdanurCollection/Edanur2"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Speed("Speed", Float) = 1
		_Tiling("Tiling", Vector) = (4,4,0,0)
		_GradientAmount("Gradient Amount", Range( -20 , 20)) = 0
		[HDR]_Glow("Glow", Color) = (0.5471188,3.067774,3.732132,0)
		_Strenght("Strenght", Range( -3 , 3)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Background+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float _GradientAmount;
		uniform float _Strenght;
		uniform float4 _Glow;
		uniform float2 _Tiling;
		uniform float _Speed;
		uniform float _Cutoff = 0.5;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float4 transform56 = mul(unity_ObjectToWorld,float4( ase_vertex3Pos , 0.0 ));
			float4 temp_cast_1 = (ase_vertex3Pos.z).xxxx;
			float4 transform22 = mul(unity_ObjectToWorld,temp_cast_1);
			float yGradient17 = saturate( ( ( transform22.w + _GradientAmount ) / 0.0 ) );
			float4 VertOffset52 = ( ( transform56 * yGradient17 ) * _Strenght );
			v.vertex.xyz += VertOffset52.xyz;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float4 temp_cast_0 = (ase_vertex3Pos.z).xxxx;
			float4 transform22 = mul(unity_ObjectToWorld,temp_cast_0);
			float yGradient17 = saturate( ( ( transform22.w + _GradientAmount ) / 0.0 ) );
			float mulTime6 = _Time.y * _Speed;
			float2 panner5 = ( mulTime6 * float2( 0,-1 ) + float2( 0,0 ));
			float2 uv_TexCoord1 = i.uv_texcoord * _Tiling + panner5;
			float simplePerlin2D2 = snoise( uv_TexCoord1 );
			float Noise11 = ( simplePerlin2D2 + 1.0 );
			float4 Emission39 = ( _Glow * ( yGradient17 * Noise11 ) );
			o.Emission = Emission39.rgb;
			o.Alpha = 1;
			float OpacityMask33 = ( ( yGradient17 * 5.0 ) - ( ( 1.0 - yGradient17 ) * Noise11 ) );
			clip( OpacityMask33 - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
-1;83;797;273;2453.17;-477.3051;2.569644;True;False
Node;AmplifyShaderEditor.CommentaryNode;13;-1947.315,-73.05798;Float;False;1493.706;464.0746;Comment;9;7;6;5;4;1;2;9;10;11;Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;32;-1907.383,558.2091;Float;False;1306.657;633.6158;Y Gradient;8;16;14;15;20;22;19;21;17;Y Gradient;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1897.315,249.117;Float;False;Property;_Speed;Speed;1;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;14;-1818.756,608.2091;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;6;-1736.542,247.9411;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-1857.383,833.6572;Float;False;Property;_GradientAmount;Gradient Amount;3;0;Create;True;0;0;False;0;0;0;-20;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;22;-1623.931,621.4664;Float;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;4;-1617.241,58.04233;Float;False;Property;_Tiling;Tiling;2;0;Create;True;0;0;False;0;4,4;4,4;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;20;-1557.286,898.6724;Float;False;Constant;_DivideAmount;Divide Amount;4;0;Create;True;0;0;False;0;0;0;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-1415.921,736.2552;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;5;-1532.707,203.3011;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-1427.527,-23.05797;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;19;-1228.978,748.6292;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1083.018,233.9892;Float;False;Constant;_Float0;Float 0;3;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;21;-1003.592,690.0677;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;2;-1137.663,-14.82803;Float;True;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;34;-1944.034,-1148.185;Float;False;1491.194;826.7256;Opacity Mask;8;23;25;26;28;24;27;18;33;Opacity Mask;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;10;-910.2204,135.9365;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;17;-808.5309,715.4551;Float;True;yGradient;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;18;-1894.034,-1013.32;Float;True;17;yGradient;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;11;-687.1094,133.5167;Float;True;Noise;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;49;-1826.301,1310.062;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;37;-329.0683,792.3685;Float;False;11;Noise;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;56;-1607.757,1304.73;Float;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;26;-1689.422,-551.459;Float;True;17;yGradient;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;25;-1578.372,-1098.185;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;50;-1618.966,1528.73;Float;False;17;yGradient;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;23;-1657.125,-855.4863;Float;True;11;Noise;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;36;-329.3525,668.7388;Float;False;17;yGradient;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-1300.471,-1037.03;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-114.7768,669.7766;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-1389.712,-631.6752;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;40;-97.99889,459.1132;Float;False;Property;_Glow;Glow;4;1;[HDR];Create;True;0;0;False;0;0.5471188,3.067774,3.732132,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-1398.988,1426.451;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-1374.161,1560.309;Float;False;Property;_Strenght;Strenght;5;0;Create;True;0;0;False;0;0;0;-3;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;183.0094,593.2356;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-1042.435,1425.502;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;28;-995.3839,-956.086;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;52;-832.9002,1460.771;Float;True;VertOffset;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;39;475.37,583.3864;Float;True;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;33;-753.6104,-958.316;Float;True;OpacityMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;35;525.0923,138.7347;Float;False;33;OpacityMask;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;53;566.5499,242.3528;Float;False;52;VertOffset;1;0;OBJECT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;31;527.7443,-115.0262;Float;True;39;Emission;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;48;788.2319,-146.2646;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;EdanurCollection/Edanur2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;False;Transparent;;Background;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;7;0
WireConnection;22;0;14;3
WireConnection;15;0;22;4
WireConnection;15;1;16;0
WireConnection;5;1;6;0
WireConnection;1;0;4;0
WireConnection;1;1;5;0
WireConnection;19;0;15;0
WireConnection;19;1;20;0
WireConnection;21;0;19;0
WireConnection;2;0;1;0
WireConnection;10;0;2;0
WireConnection;10;1;9;0
WireConnection;17;0;21;0
WireConnection;11;0;10;0
WireConnection;56;0;49;0
WireConnection;25;0;18;0
WireConnection;24;0;25;0
WireConnection;24;1;23;0
WireConnection;38;0;36;0
WireConnection;38;1;37;0
WireConnection;27;0;26;0
WireConnection;51;0;56;0
WireConnection;51;1;50;0
WireConnection;41;0;40;0
WireConnection;41;1;38;0
WireConnection;55;0;51;0
WireConnection;55;1;54;0
WireConnection;28;0;27;0
WireConnection;28;1;24;0
WireConnection;52;0;55;0
WireConnection;39;0;41;0
WireConnection;33;0;28;0
WireConnection;48;2;31;0
WireConnection;48;10;35;0
WireConnection;48;11;53;0
ASEEND*/
//CHKSM=7F656ECCABDC7D043C23DE7C1C3EE312EA07831E