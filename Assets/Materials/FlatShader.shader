Shader "Unlit/FlatShader"
{
    Properties
    {
        _Albedo("Albedo", Color) = (1,1,1,1)
        _Shades("Shades", Range(1, 20)) = 3
        _InkColor("InkColor", Color) = (0,0,0,0)
        _InkSize("InkSize", float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        // Pass for Outline effect
        Pass
        {
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL; //Declare variable for object normal
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float4 _InkColor;
            float _InkSize;
 

            v2f vert (appdata v)
            {
                v2f o;

                //Translate vertex along the normal vector to increase the size of the mesh
                o.vertex = UnityObjectToClipPos(v.vertex + _InkSize * v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _InkColor;
            }
            ENDCG
        }

        //Toon shader pass
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag



            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL; //Declare variable for object normal
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD0; //Declare world space normal
            };

            float4 _Albedo;
            float _Shades;
 

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal); //Convert normal to world space normal
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //Calculate the cosine of the angle between the normal vector and the light direction
                // The world space light direction is stored in _WorldSpaceLightPos0
                // The world space normal is stored in i.worldNormal
                // Normalize both vectors and calculate the dot product
                float cosineAngle = dot(normalize(i.worldNormal), normalize(_WorldSpaceLightPos0.xyz));

                //Set min to 0 to avoid negative values
                cosineAngle = max(cosineAngle, 0.3);
                
                //Quantisize color with _Shades variable
                cosineAngle = floor(cosineAngle * _Shades) / _Shades;

                return _Albedo * cosineAngle;
            }
            ENDCG
        }
    }

    //Fallback shader for cast shadow
    Fallback "VertexLit"
}
