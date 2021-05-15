#if UNITY_EDITOR
using UnityEditor.ShaderGraph;
using System.Reflection;
using UnityEngine;

[Title("Pixel Perfect", "Pixel Point")]
internal class PixelPointNode : CodeFunctionNode {
	public PixelPointNode() {
		name = "Pixel Point";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("PixelPoint", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string PixelPoint([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None)] Vector2 Position, [Slot(2, Binding.None)] out Vector1 Out) {
		return @"
{
    float2 f = UV - Position;
    float2 ddxUV = ddx(UV);
    float2 ddyUV = ddy(UV);
	
	float InvD = 1/(ddxUV.x*ddyUV.y-ddxUV.y*ddyUV.x);

	float tx = (ddyUV.y*f.x-ddyUV.x*f.y)*InvD;
	float ty = (-ddxUV.y*f.x+ddxUV.x*f.y)*InvD;

    Out = (tx > -0.5 && tx <= 0.5) && (ty > -0.5 && ty <= 0.5) ? 1 : 0;
}
";
	}
}

[Title("Pixel Perfect", "Pixel Point Grid")]
internal class PixelPointGridNode : CodeFunctionNode {
	public PixelPointGridNode() {
		name = "Pixel Point Grid";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("PixelPointGrid", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string PixelPointGrid([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None)] Vector2 Position, [Slot(2, Binding.None, 0.1f, 0, 0, 0)] Vector1 Width, [Slot(3, Binding.None)] out Vector1 Out) {
		return @"
{
    float2 f = UV - Position;
	float2 rounded = {round(f.x/Width), round(f.y/Width)};
	f = f - Width * rounded;
    float2 ddxUV = ddx(UV);
    float2 ddyUV = ddy(UV);
	
	float InvD = 1/(ddxUV.x*ddyUV.y-ddxUV.y*ddyUV.x);

	float tx = (ddyUV.y*f.x-ddyUV.x*f.y)*InvD;
	float ty = (-ddxUV.y*f.x+ddxUV.x*f.y)*InvD;

    Out = (tx > -0.5 && tx <= 0.5) && (ty > -0.5 && ty <= 0.5) ? 1 : 0;
}
";
	}
}

[Title("Pixel Perfect", "Pixel Ray")]
internal class PixelRayNode : CodeFunctionNode {
	public PixelRayNode() {
		name = "Pixel Ray";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("PixelRay", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string PixelRay([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None)] Vector2 Position, [Slot(2, Binding.None, 1, 0, 0, 0)] Vector2 Direction, [Slot(3, Binding.None)] out Vector1 Out) {
		return @"
{
    float2 d = Direction;
    float2 f = UV - Position;
    float2 ddxUV = ddx(UV);
    float2 ddyUV = ddy(UV);

    float tq1 = (d.x * f.y - d.y * f.x) / (ddxUV.y * d.x - ddxUV.x * d.y);
    float tq2 = (d.x * f.y - d.y * f.x) / (ddyUV.y * d.x - ddyUV.x * d.y);

    Out = (tq1 > -0.5 && tq1 <= 0.5) || (tq2 > -0.5 && tq2 <= 0.5) ? 1 : 0;
}
";
	}
}

[Title("Pixel Perfect", "Pixel Rays")]
internal class PixelRaysNode : CodeFunctionNode {
	public PixelRaysNode() {
		name = "Pixel Rays";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("PixelRays", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string PixelRays([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None)] Vector2 Position, [Slot(2, Binding.None, 1, 0, 0, 0)] Vector2 Direction, [Slot(3, Binding.None, 0.1f, 0, 0, 0)] Vector1 Width, [Slot(4, Binding.None)] out Vector1 Out) {
		return @"
{
    float2 d = Direction;
	float2 n = {-Direction.y, Direction.x};
	n = normalize(n);
    float2 f = UV - Position;
	f = f - Width * round(dot(n, f)/Width) * n;
    float2 ddxUV = ddx(UV);
    float2 ddyUV = ddy(UV);

    float tq1 = (d.x * f.y - d.y * f.x) / (ddxUV.y * d.x - ddxUV.x * d.y);
    float tq2 = (d.x * f.y - d.y * f.x) / (ddyUV.y * d.x - ddyUV.x * d.y);

    Out = (tq1 > -0.5 && tq1 <= 0.5) || (tq2 > -0.5 && tq2 <= 0.5) ? 1 : 0;
}
";
	}
}

[Title("Pixel Perfect", "Pixel Line")]
internal class PixelLineNode : CodeFunctionNode {
	public PixelLineNode() {
		name = "Pixel Line";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("PixelLine", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string PixelLine([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None)] Vector2 Position1, [Slot(2, Binding.None)] Vector2 Position2, [Slot(3, Binding.None)] out Vector1 Out) {
		return @"

{
    float2 d = Position2 - Position1;
    float2 f = UV - Position1;
    float2 ddxUV = ddx(UV);
    float2 ddyUV = ddy(UV);

    float InvD1 = 1/(ddxUV.y * d.x - ddxUV.x * d.y);
    float InvD2 = 1/(ddyUV.y * d.x - ddyUV.x * d.y);

    float tp1 = -(ddxUV.x * f.y - ddxUV.y * f.x) * InvD1;
    float tq1 = (d.x * f.y - d.y * f.x) * InvD1;

    float tp2 = -(ddyUV.x * f.y - ddyUV.y * f.x) * InvD2;
    float tq2 = (d.x * f.y - d.y * f.x) * InvD2;

    Out = (tp1 >= 0 && tp1 <= 1 && tq1 > -0.5 && tq1 <= 0.5) || (tp2 >= 0 && tp2 <= 1 && tq2 > -0.5 && tq2 <= 0.5) ? 1 : 0;
}
";
	}
}

[Title("Pixel Perfect", "Pixel Lines")]
internal class PixelLinesNode : CodeFunctionNode {
	public PixelLinesNode() {
		name = "Pixel Lines";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("PixelLines", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string PixelLines([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None)] Vector2 Position1, [Slot(2, Binding.None)] Vector2 Position2, [Slot(3, Binding.None, 0.1f, 0, 0, 0)] Vector1 Width, [Slot(4, Binding.None)] out Vector1 Out) {
		return @"

{
    float2 d = Position2 - Position1;
	float2 n = {-d.y, d.x};
	n = normalize(n);
    float2 f = UV - Position1;
	f = f - Width * round(dot(n, f)/Width) * n;
    float2 ddxUV = ddx(UV);
    float2 ddyUV = ddy(UV);

    float InvD1 = 1/(ddxUV.y * d.x - ddxUV.x * d.y);
    float InvD2 = 1/(ddyUV.y * d.x - ddyUV.x * d.y);

    float tp1 = -(ddxUV.x * f.y - ddxUV.y * f.x) * InvD1;
    float tq1 = (d.x * f.y - d.y * f.x) * InvD1;

    float tp2 = -(ddyUV.x * f.y - ddyUV.y * f.x) * InvD2;
    float tq2 = (d.x * f.y - d.y * f.x) * InvD2;

    Out = (tp1 >= 0 && tp1 <= 1 && tq1 > -0.5 && tq1 <= 0.5) || (tp2 >= 0 && tp2 <= 1 && tq2 > -0.5 && tq2 <= 0.5) ? 1 : 0;
}
";
	}
}

[Title("Pixel Perfect", "Pixel Circle")]
internal class PixelCircleNode : CodeFunctionNode {
	public PixelCircleNode() {
		name = "Pixel Circle";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("PixelCircle", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string PixelCircle([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None)] Vector2 Center, [Slot(2, Binding.None, 0.5f, 0, 0, 0)] Vector1 Radius, [Slot(3, Binding.None)] out Vector1 Out) {
		return @"

{
    float2 f = UV - Center;
    float2 ddxUV = ddx(UV);
    float2 ddyUV = ddy(UV);

	float r2 = Radius * Radius;
	
	float2 fx1, fx2, fy1, fy2;
	fx1 = f - 0.5*ddxUV;
	fx2 = f + 0.5*ddxUV;
	fy1 = f - 0.5*ddyUV;
	fy2 = f + 0.5*ddyUV;
	
    Out = ((dot(fx1, fx1) - r2) * (dot(fx2, fx2) - r2) <= 0 || (dot(fy1, fy1) - r2) * (dot(fy2, fy2) - r2) <= 0) ? 1 : 0;
}
";
	}
}

[Title("Pixel Perfect", "Pixel Polygon")]
internal class PixelPolygonNode : CodeFunctionNode {
	public PixelPolygonNode() {
		name = "Pixel Polygon";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("PixelPolygon", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string PixelPolygon([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None)] Vector2 Center, [Slot(2, Binding.None, 0.5f, 0, 0, 0)] Vector1 Radius, [Slot(3, Binding.None, 6, 0, 0, 0)] Vector1 Sides, [Slot(4, Binding.None)] Vector1 Angle, [Slot(5, Binding.None)] out Vector1 Out) {
		return @"

{
    float2 f = UV - Center;
	float angle = 6.2831853071/Sides;
	Angle = 0.0174533*Angle;

	// Apply rotation
	float sinAngle, cosAngle;
	sincos(Angle, sinAngle, cosAngle);
	f = float2(f.y * sinAngle + f.x * cosAngle, f.y * cosAngle - f.x * sinAngle);

	float theta = atan2(f.y, f.x);

	float SinSide, CosSide;
	sincos(round(theta / angle) * angle, SinSide, CosSide); 

    float2 d = float2(SinSide, -CosSide);
	float2 n = float2(CosSide, SinSide);
    f = f - n*Radius;
    float2 ddxUV = ddx(UV);
    float2 ddyUV = ddy(UV);

    float tq1 = (d.x * f.y - d.y * f.x) / (ddxUV.y * d.x - ddxUV.x * d.y);
    float tq2 = (d.x * f.y - d.y * f.x) / (ddyUV.y * d.x - ddyUV.x * d.y);

    Out = (tq1 > -0.5 && tq1 <= 0.5) || (tq2 > -0.5 && tq2 <= 0.5) ? 1 : 0;
}
";
	}
}

#endif
