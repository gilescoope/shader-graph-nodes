#if UNITY_EDITOR
using UnityEditor.ShaderGraph;
using System.Reflection;
using UnityEngine;

[Title("Halftone", "Halftone Circle")]
public class HalftoneCircleNode : CodeFunctionNode {
	public HalftoneCircleNode() {
		name = "Halftone Circle";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("HalftoneCircle", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string HalftoneCircle([Slot(0, Binding.None)] Vector1 Base, [Slot(1, Binding.WorldSpacePosition)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)] Vector2 Offset, [Slot(3, Binding.None)] out Vector1 Out) {
		return @"
{
	float Scale = 0.78;
	float2 Direction, Position;
	Direction = Offset/dot(Offset, Offset);
	Position = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	Out = Scale*dot(Position, Position)/(0.25*(1-Base));
	Out = 1-saturate((1 - Out) / fwidth(Out));
}
";
	}
}

[Title("Halftone", "Halftone Circle Color")]
public class HalftoneCircleColorNode : CodeFunctionNode {
	public HalftoneCircleColorNode() {
		name = "Halftone Circle Color";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("HalftoneCircleColor", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string HalftoneCircleColor([Slot(0, Binding.None)] Vector3 Base, [Slot(1, Binding.WorldSpacePosition)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)] Vector2 OffsetR, [Slot(3, Binding.None, 0.00866f, 0.005f, 0, 0)] Vector2 OffsetG, [Slot(4, Binding.None, 0.005f, 0.00866f, 0, 0)] Vector2 OffsetB, [Slot(5, Binding.None)] out Vector3 Out) {
		Out = new Vector3();
		return @"
{
	float Scale = 0.78;
	float2 Direction, Position;
	Direction = OffsetR/dot(OffsetR, OffsetR);
	Position = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	Out.x = Scale*dot(Position, Position)/(0.25*(1-Base.x));
	Direction = OffsetG/dot(OffsetG, OffsetG);
	Position = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	Out.y = Scale*dot(Position, Position)/(0.25*(1-Base.y));
	Direction = OffsetB/dot(OffsetB, OffsetB);
	Position = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	Out.z = Scale*dot(Position, Position)/(0.25*(1-Base.z));
	Out = 1-saturate((1 - Out) / fwidth(Out));
}
";
	}
}

[Title("Halftone", "Halftone Smooth")]
public class HalftoneSmoothNode : CodeFunctionNode {
	public HalftoneSmoothNode() {
		name = "Halftone Smooth";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("HalftoneSmooth", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string HalftoneSmooth([Slot(0, Binding.None)] Vector1 Base, [Slot(1, Binding.WorldSpacePosition)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)] Vector2 Offset, [Slot(3, Binding.None)] out Vector1 Out) {
		return @"
{
	float2 Direction, Position1, Position2;
	float P1P1, P2P2, T; 
	Direction = Offset/dot(Offset, Offset);
	Position1 = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	Position2 = fmod(Position1 + 1, 1) - 0.5;
	P1P1 = dot(Position1, Position1);
	P2P2 = dot(Position2, Position2);
	T = P1P1/(P1P1 + P2P2);
	Out.x =  (1-T)*(P1P1 - 0.25*(1-Base))-T*(P2P2 - 0.25*Base);
	Out = 1-saturate((-Out) / fwidth(Out));

}
";
	}
}

[Title("Halftone", "Halftone Smooth Color")]
public class HalftoneSmoothColorNode : CodeFunctionNode {
	public HalftoneSmoothColorNode() {
		name = "Halftone Smooth Color";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("HalftoneSmoothColor", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string HalftoneSmoothColor([Slot(0, Binding.None)] Vector3 Base, [Slot(1, Binding.WorldSpacePosition)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)] Vector2 OffsetR, [Slot(3, Binding.None, 0.00866f, 0.005f, 0, 0)] Vector2 OffsetG, [Slot(4, Binding.None, 0.005f, 0.00866f, 0, 0)] Vector2 OffsetB, [Slot(5, Binding.None)] out Vector3 Out) {
		Out = new Vector3();
		return @"
{
	float2 Direction, Position1, Position2;
	float P1P1, P2P2, T; 
	Direction = OffsetR/dot(OffsetR, OffsetR);
	Position1 = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	Position2 = fmod(Position1 + 1, 1) - 0.5;
	P1P1 = dot(Position1, Position1);
	P2P2 = dot(Position2, Position2);
	T = P1P1/(P1P1 + P2P2);
	Out.x =  (1-T)*(P1P1 - 0.25*(1-Base.x))-T*(P2P2 - 0.25*Base.x);
	Direction = OffsetG/dot(OffsetG, OffsetG);
	Position1 = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	Position2 = fmod(Position1 + 1, 1) - 0.5;
	P1P1 = dot(Position1, Position1);
	P2P2 = dot(Position2, Position2);
	T = P1P1/(P1P1 + P2P2);
	Out.y =  (1-T)*(P1P1 - 0.25*(1-Base.y))-T*(P2P2 - 0.25*Base.y);
	Direction = OffsetB/dot(OffsetB, OffsetB);
	Position1 = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	Position2 = fmod(Position1 + 1, 1) - 0.5;
	P1P1 = dot(Position1, Position1);
	P2P2 = dot(Position2, Position2);
	T = P1P1/(P1P1 + P2P2);
	Out.z =  (1-T)*(P1P1 - 0.25*(1-Base.z))-T*(P2P2 - 0.25*Base.z);
	Out = 1-saturate((- Out) / fwidth(Out));
}
";
	}
}

[Title("Halftone", "Halftone Square")]
public class HalftoneSquareNode : CodeFunctionNode {
	public HalftoneSquareNode() {
		name = "Halftone Square";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("HalftoneSquare", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string HalftoneSquare([Slot(0, Binding.None)] Vector1 Base, [Slot(1, Binding.WorldSpacePosition)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)] Vector2 Offset, [Slot(3, Binding.None)] out Vector1 Out) {
		return @"
{
	float2 Direction, Position;
	float Radius;
	Direction = Offset/dot(Offset, Offset);
	Position = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	Radius = 0.5*sqrt(1-Base);
	Out = max(abs(Position.x), abs(Position.y))/Radius;
	Out = 1-saturate((1 - Out) / fwidth(Out));
}
";
	}
}

[Title("Halftone", "Halftone Square Color")]
public class HalftoneSquareColorNode : CodeFunctionNode {
	public HalftoneSquareColorNode() {
		name = "Halftone Square Color";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("HalftoneSquareColor", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string HalftoneSquareColor([Slot(0, Binding.None)] Vector3 Base, [Slot(1, Binding.WorldSpacePosition)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)] Vector2 OffsetR, [Slot(3, Binding.None, 0.00866f, 0.005f, 0, 0)] Vector2 OffsetG, [Slot(4, Binding.None, 0.005f, 0.00866f, 0, 0)] Vector2 OffsetB, [Slot(5, Binding.None)] out Vector3 Out) {
		Out = new Vector3();
		return @"
{
	float2 Direction, Position;
	float Radius;
	Direction = OffsetR/dot(OffsetR, OffsetR);
	Position = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	Radius = 0.5*sqrt(1-Base.x);
	Out.x = max(abs(Position.x), abs(Position.y))/Radius;
	Direction = OffsetG/dot(OffsetG, OffsetG);
	Position = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	Radius = 0.5*sqrt(1-Base.y);
	Out.y = max(abs(Position.x), abs(Position.y))/Radius;
	Direction = OffsetB/dot(OffsetB, OffsetB);
	Position = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	Radius = 0.5*sqrt(1-Base.z);
	Out.z = max(abs(Position.x), abs(Position.y))/Radius;
	Out = 1-saturate((1 - Out) / fwidth(Out));
}
";
	}
}
#endif
