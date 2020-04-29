// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Trail"
{
	Properties
	{
		_MainColor("MainColor", Color) = (0.99641,1,0.75,1)
		_EmissiveIntensity("EmissiveIntensity", Float) = 2
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_MinNewNoise("MinNewNoise", Float) = 0.5
		_PannerSpeed("PannerSpeed", Vector) = (0,0,0,0)
		_MaxNewNoise("MaxNewNoise", Float) = 0.8
		_opacity("opacity", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _MainColor;
		uniform float _EmissiveIntensity;
		uniform sampler2D _TextureSample0;
		uniform float2 _PannerSpeed;
		uniform float _MinNewNoise;
		uniform float _MaxNewNoise;
		uniform float _opacity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 panner10 = ( _Time.y * _PannerSpeed + i.uv_texcoord);
			o.Emission = ( _MainColor * ( _EmissiveIntensity * (_MinNewNoise + (tex2D( _TextureSample0, panner10 ).r - 0.0) * (_MaxNewNoise - _MinNewNoise) / (1.0 - 0.0)) ) ).rgb;
			o.Alpha = _opacity;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
94;144;1569;875;2416.225;1052.052;2.397515;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-1341.403,238.1341;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;11;-1296.403,366.1342;Float;False;Property;_PannerSpeed;PannerSpeed;4;0;Create;True;0;0;False;0;0,0;0.1,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;12;-1067.402,365.1342;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;10;-1069.402,246.1341;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;4;-825.4012,224.1341;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;db492e182ddba8e428bf1e64e9be7f4b;db492e182ddba8e428bf1e64e9be7f4b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-718.4015,426.1341;Float;False;Property;_MinNewNoise;MinNewNoise;3;0;Create;True;0;0;False;0;0.5;0.76;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-697.1068,528.3719;Float;False;Property;_MaxNewNoise;MaxNewNoise;5;0;Create;True;0;0;False;0;0.8;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-531.5,151;Float;False;Property;_EmissiveIntensity;EmissiveIntensity;1;0;Create;True;0;0;False;0;2;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;7;-486.4015,255.1342;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.5;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;-495.5,-73;Float;False;Property;_MainColor;MainColor;0;0;Create;True;0;0;False;0;0.99641,1,0.75,1;0.993536,1,0.4764151,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-246.5244,170.5237;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-245.5,72;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;15;91.54285,405.4429;Float;False;Property;_opacity;opacity;6;0;Create;True;0;0;False;0;0;0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;87,-17;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_Trail;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;9;0
WireConnection;10;2;11;0
WireConnection;10;1;12;0
WireConnection;4;1;10;0
WireConnection;7;0;4;1
WireConnection;7;3;8;0
WireConnection;7;4;14;0
WireConnection;16;0;3;0
WireConnection;16;1;7;0
WireConnection;2;0;1;0
WireConnection;2;1;16;0
WireConnection;0;2;2;0
WireConnection;0;9;15;0
ASEEND*/
//CHKSM=214E0BD21C9036CCA82B7FD3DEA6AA321A379A98