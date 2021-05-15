using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Graphing;
using UnityEditor.ShaderGraph;
using UnityEditor.ShaderGraph.Drawing.Controls;
using UnityEngine;


[Title("Math", "Random", "Random Integer Range")]
internal class RandomIntegerNode : CodeFunctionNode {
	public RandomIntegerNode() {
		name = "Random Integer Range";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("RandomIntegerRange", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string RandomIntegerRange([Slot(0, Binding.None)] Vector2 Seed, [Slot(1, Binding.None)] Vector1 Min, [Slot(2, Binding.None, 10, 0, 0, 0)] Vector1 Max, [Slot(3, Binding.None)] out Vector1 Out) {
		return @"
{
	float r = frac(sin(dot(Seed, float2(127.1,311.7))) * 43758.5453);
	Min = ceiling(Min);
	Max = ceiling(Max);
    return Min + (Max-Min)*floor(r * (Max-Min));
}
";
	}
}

[Serializable]
public enum RandomMode {
	In,
	On
}

[Title("Math", "Random", "Random Circle")]
internal class RandomCircleNode : CodeFunctionNode {
	[SerializeField] private RandomMode m_RandomMode = RandomMode.In;

	public RandomCircleNode() {
		name = "Random Circle";
	}

	private string GetCurrentModeName() {
		return Enum.GetName(typeof(RandomMode), m_RandomMode);
	}

	[EnumControl("Mode")]
	public RandomMode randomMode {
		get { return m_RandomMode; }
		set {
			if (m_RandomMode == value)
				return;
			m_RandomMode = value;
			Dirty(ModificationScope.Graph);
		}
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod(string.Format("RandomCircle_{0}", GetCurrentModeName()), BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string RandomCircle_In([Slot(0, Binding.None)] Vector2 Seed, [Slot(1, Binding.None)] out Vector2 Out) {
		Out = Vector2.zero;
		return @"
{
	float2 r = frac(sin(float2(dot(Seed, float2(127.1,311.7)), dot(Seed, float2(269.5,183.3)))) * 43758.5453);
	float Sin, Cos;
	sincos(r.x*6.28318530718, Sin, Cos);
	Out = sqrt(r.y) * float2(Cos, Sin);
}
";
	}

	static string RandomCircle_On([Slot(0, Binding.None)] Vector2 Seed, [Slot(1, Binding.None)] out Vector2 Out) {
		Out = Vector2.zero;
		return @"
{
	float r = frac(sin(dot(Seed, float2(127.1,311.7))) * 43758.5453);
	float Sin, Cos;
	sincos(r*6.28318530718, Sin, Cos);
	Out = float2(Cos, Sin);
}
";
	}
}

[Title("Math", "Random", "Random Sphere")]
internal class RandomSphereNode : CodeFunctionNode {
	[SerializeField] private RandomMode m_RandomMode = RandomMode.In;

	public RandomSphereNode() {
		name = "Random Sphere";
	}

	private string GetCurrentModeName() {
		return Enum.GetName(typeof(RandomMode), m_RandomMode);
	}

	[EnumControl("Mode")]
	public RandomMode randomMode {
		get { return m_RandomMode; }
		set {
			if (m_RandomMode == value)
				return;
			m_RandomMode = value;
			Dirty(ModificationScope.Graph);
		}
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod(string.Format("RandomSphere_{0}", GetCurrentModeName()), BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string RandomSphere_In([Slot(0, Binding.None)] Vector2 Seed, [Slot(1, Binding.None)] out Vector3 Out) {
		Out = Vector3.zero;
		return @"
{
	float3 r = frac(sin(float3(dot(Seed, float2(127.1,311.7)), dot(Seed, float2(269.5,183.3)), dot(Seed, float2(419.2,371.9)))) * 43758.5453);
	float SinTheta, CosTheta;
	sincos(r.x*6.28318530718, SinTheta, CosTheta);
	float CosPhi = 2*r.y - 1;
	float SinPhi = sqrt(1-CosPhi*CosPhi);
	Out = pow(r.z, 1/3) * float3(CosTheta*SinPhi, SinTheta*SinPhi, CosPhi);
}
";
	}

	static string RandomSphere_On([Slot(0, Binding.None)] Vector2 Seed, [Slot(1, Binding.None)] out Vector3 Out) {
		Out = Vector3.zero;
		return @"
{
	float2 r = frac(sin(float2(dot(Seed, float2(127.1,311.7)), dot(Seed, float2(269.5,183.3)))) * 43758.5453);
	float SinTheta, CosTheta;
	sincos(r.x*6.28318530718, SinTheta, CosTheta);
	float CosPhi = 2*r.y - 1;
	float SinPhi = sqrt(1-CosPhi*CosPhi);
	Out = float3(CosTheta*SinPhi, SinTheta*SinPhi, CosPhi);
}
";
	}
}

[Title("Math", "Random", "Random Rotation")]
internal class RandomRotationNode : CodeFunctionNode {
	public RandomRotationNode() {
		name = "Random Rotation";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("RandomRotation", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string RandomRotation([Slot(0, Binding.None)] Vector2 Seed, [Slot(1, Binding.None)] out Vector4 Out) {
		Out = Vector4.zero;
		return @"
{
	float3 r = frac(sin(float3(dot(Seed, float2(127.1,311.7)), dot(Seed, float2(269.5,183.3)), dot(Seed, float2(419.2,371.9)))) * 43758.5453);
	float SinY, CosY, SinZ, CosZ;
	sincos(r.y*6.28318530718, SinY, CosY);
	sincos(r.z*6.28318530718, SinZ, CosZ);
	Out = sqrt(r.x) * float4(0, 0, SinZ, CosZ) + sqrt(1-r.x) * float4(SinY, CosY, 0, 0);
}
";
	}
}

[Title("Math", "Random", "Random Color")]
internal class RandomColorNode : CodeFunctionNode {
	public RandomColorNode() {
		name = "Random Color";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("RandomColor", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string RandomColor([Slot(0, Binding.None)] Vector2 Seed, [Slot(1, Binding.None)] Vector3 MinHSV, [Slot(2, Binding.None, 1, 1, 1, 0)] Vector3 MaxHSV, [Slot(3, Binding.None)] out Vector3 RGB, [Slot(4, Binding.None)] out Vector3 HSV) {
		HSV = Vector3.zero;
		RGB = Vector3.zero;
		return @"
{
	float3 r = frac(sin(float3(dot(Seed, float2(127.1,311.7)), dot(Seed, float2(269.5,183.3)), dot(Seed, float2(419.2,371.9)))) * 43758.5453);
	HSV = MaxHSV - MinHSV;
	HSV = MinHSV + (HSV < 0 ? frac(HSV) : HSV) * r;
	HSV = float3(frac(HSV.x), clamp(HSV.y, 0, 1), clamp(HSV.z, 0, 1));
    RGB = saturate(float3(abs(HSV.x * 6 - 3) - 1,2 - abs(HSV.x * 6 - 2),2 - abs(HSV.x * 6 - 4)));
	RGB = ((RGB - 1) * HSV.y + 1) * HSV.z;
}
";
	}
}