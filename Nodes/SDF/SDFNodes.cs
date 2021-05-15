#if UNITY_EDITOR
using System;
using UnityEditor.ShaderGraph;
using System.Reflection;
using UnityEditor.Graphing;
using UnityEditor.ShaderGraph.Drawing.Controls;
using UnityEngine;

[Title("SDF", "SDF Line")]
internal class SDFLineNode: CodeFunctionNode {
    public SDFLineNode() {
        name = "SDF Line";
    }

    protected override MethodInfo GetFunctionToConvert()
    {
        return GetType().GetMethod("SDFLine", BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string SDFLine([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None)] Vector1 Position, [Slot(2, Binding.None, 0.1f, 0, 0, 0)] Vector1 Width, [Slot(3, Binding.None)] out Vector1 SDF) {
        return @"
{    
    SDF = abs(UV.x - Position) - 0.5*Width;
}
";
    }
}

[Title("SDF", "SDF Circle")]
internal class SDFCircleNode: CodeFunctionNode {
    public SDFCircleNode() {
        name = "SDF Circle";
    }

    protected override MethodInfo GetFunctionToConvert()
    {
        return GetType().GetMethod("SDFCircle", BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string SDFCircle([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None)] Vector2 Position, [Slot(2, Binding.None, 0.25f, 0, 0, 0)] Vector1 Radius, [Slot(3, Binding.None)] out Vector1 SDF) {
        return @"
{
    SDF = length(UV - Position) - Radius;
}
";
    }
}

[Title("SDF", "SDF Rectangle")]
internal class SDFRectangleNode: CodeFunctionNode {
    public SDFRectangleNode() {
        name = "SDF Rectangle";
    }

    protected override MethodInfo GetFunctionToConvert()
    {
        return GetType().GetMethod("SDFRectangle", BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string SDFRectangle([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None)] Vector2 Position, [Slot(2, Binding.None, 0.5f, 0, 0, 0)] Vector1 Width, [Slot(3, Binding.None, 0.5f, 0, 0, 0)] Vector1 Height, [Slot(4, Binding.None)] Vector1 CornerRadius, [Slot(5, Binding.None)] out Vector1 SDF) {
        return @"
{    
    float2 d = abs(UV - Position) - 0.5*float2(Width, Height);
    SDF = min(max(d.x,d.y), 0) + length(max(d, 0));
    SDF = SDF - CornerRadius;
}
";
    }
}

[Title("SDF", "SDF Polygon")]
internal class SDFPolygonNode: CodeFunctionNode {
    public SDFPolygonNode() {
        name = "SDF Polygon";
    }

    protected override MethodInfo GetFunctionToConvert()
    {
        return GetType().GetMethod("SDFPolygon", BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string SDFPolygon([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None)] Vector2 Position, [Slot(2, Binding.None, 0.25f, 0, 0, 0)] Vector1 Radius, [Slot(3, Binding.None, 6, 0, 0, 0)] Vector1 Sides, [Slot(4, Binding.None)] Vector1 CornerRadius, [Slot(5, Binding.None)] out Vector1 SDF) {
        return @"
{    
    float2 f = UV - Position;
	float theta = atan2(f.y, f.x);
	float angle = 6.2831853071/Sides;

	float SinSide, CosSide;
	sincos(round(theta / angle) * angle, SinSide, CosSide); 

    float2 d = float2(SinSide, -CosSide); 
	float2 n = float2(CosSide, SinSide);
    float t = dot(d, f);
    float sideLength = Radius * tan(0.5*angle);
    SDF = abs(t) < Radius * tan(0.5*angle) ? dot(f, n) - Radius : length(f - (Radius * n + d * clamp(dot(d, f), -sideLength, sideLength)));
    SDF = SDF - CornerRadius;
}
";
    }
}

[Title("SDF", "SDF Sample")]
internal class SDFSampleNode: CodeFunctionNode {
    public SDFSampleNode() {
        name = "SDF Sample";
    }

    protected override MethodInfo GetFunctionToConvert()
    {
        return GetType().GetMethod("SDFSample", BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string SDFSample([Slot(0, Binding.None)] Vector1 SDF, [Slot(1, Binding.None)] Vector1 Offset, [Slot(2, Binding.None)] out Vector1 Out) {
        return @"
{
    Out = SDF - Offset;
    Out = saturate(-Out / fwidth(Out));
}
";
    }
}

[Title("SDF", "SDF Sample Strip")]
internal class SDFSampleStripNode: CodeFunctionNode {
    public SDFSampleStripNode() {
        name = "SDF Sample Strip";
    }

    protected override MethodInfo GetFunctionToConvert()
    {
        return GetType().GetMethod("SDFSampleStrip", BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string SDFSampleStrip([Slot(0, Binding.None)] Vector1 SDF, [Slot(1, Binding.None, -0.05f, 0.05f, 0, 0)] Vector2 Offset, [Slot(2, Binding.None)] out Vector1 Out) {
        return @"
{
    Out = max(-(SDF - Offset.x), SDF - Offset.y);
    Out = saturate(-Out / fwidth(Out));
}
";
    }
}

public enum BooleanMode
{
    Union,
    Intersection,
    Difference
}

[Title("SDF", "SDF Boolean")]
internal class SDFBooleanNode: CodeFunctionNode {
    [SerializeField]
    private BooleanMode m_BooleanMode = BooleanMode.Union;
    
    public SDFBooleanNode() {
        name = "SDF Boolean";
    }

    private string GetCurrentBlendName()
    {
        return Enum.GetName(typeof (BooleanMode), m_BooleanMode);
    }

    [EnumControl("Mode")]
    public BooleanMode booleanMode
    {
        get
        {
            return m_BooleanMode;
        }
        set
        {
            if (m_BooleanMode == value)
                return;
            m_BooleanMode = value;
            Dirty(ModificationScope.Graph);
        }
    }

    protected override MethodInfo GetFunctionToConvert()
    {
        return GetType().GetMethod(string.Format("SDFBoolean_{0}", GetCurrentBlendName()), BindingFlags.Static | BindingFlags.NonPublic);
    }
    
    private static string SDFBoolean_Union([Slot(0, Binding.None)] Vector1 A, [Slot(1, Binding.None)] Vector1 B, [Slot(2, Binding.None)] out Vector1 Out) {
        return @"
{
    Out = min(A, B);
}
";
    }
    
    private static string SDFBoolean_Intersection([Slot(0, Binding.None)] Vector1 A, [Slot(1, Binding.None)] Vector1 B, [Slot(2, Binding.None)] out Vector1 Out) {
        return @"
{
    Out = max(A, B);
}
";
    }
    
    private static string SDFBoolean_Difference([Slot(0, Binding.None)] Vector1 A, [Slot(1, Binding.None)] Vector1 B, [Slot(2, Binding.None)] out Vector1 Out) {
        return @"
{
    Out = max(A, -B);
}
";
    }
    
}

[Title("SDF", "SDF Boolean Soft")]
internal class SDFBooleanSoftNode: CodeFunctionNode {
    [SerializeField]
    private BooleanMode m_BooleanMode = BooleanMode.Union;
    
    public SDFBooleanSoftNode() {
        name = "SDF Boolean Soft";
    }

    private string GetCurrentBlendName()
    {
        return Enum.GetName(typeof (BooleanMode), m_BooleanMode);
    }

    [EnumControl("Mode")]
    public BooleanMode booleanMode
    {
        get
        {
            return m_BooleanMode;
        }
        set
        {
            if (m_BooleanMode == value)
                return;
            m_BooleanMode = value;
            Dirty(ModificationScope.Graph);
        }
    }

    protected override MethodInfo GetFunctionToConvert()
    {
        return GetType().GetMethod(string.Format("SDFBooleanSoft_{0}", GetCurrentBlendName()), BindingFlags.Static | BindingFlags.NonPublic);
    }
    
    private static string SDFBooleanSoft_Union([Slot(0, Binding.None)] Vector1 A, [Slot(1, Binding.None)] Vector1 B, [Slot(2, Binding.None, 0.1f, 0, 0, 0)] Vector1 Smoothing,  [Slot(3, Binding.None)] out Vector1 Out) {
        return @"
{
    float t = clamp(0.5 * (1 + (B - A) / Smoothing), 0, 1);
    Out = lerp(B, A, t) - Smoothing * t * (1 - t);
}
";
    }
    
    private static string SDFBooleanSoft_Intersection([Slot(0, Binding.None)] Vector1 A, [Slot(1, Binding.None)] Vector1 B, [Slot(2, Binding.None, 0.1f, 0, 0, 0)] Vector1 Smoothing,  [Slot(3, Binding.None)] out Vector1 Out) {
        return @"
{
    float t = clamp(0.5 * (1 + (A - B) / Smoothing), 0, 1);
    Out = -(lerp(-B, -A, t) - Smoothing * t * (1 - t));
}
";
    }
    
    private static string SDFBooleanSoft_Difference([Slot(0, Binding.None)] Vector1 A, [Slot(1, Binding.None)] Vector1 B, [Slot(2, Binding.None, 0.1f, 0, 0, 0)] Vector1 Smoothing,  [Slot(3, Binding.None)] out Vector1 Out) {
        return @"
{
    float t = clamp(0.5 * (1 + (A + B) / Smoothing), 0, 1);
    Out = -(lerp(B, -A, t) - Smoothing * t * (1 - t));
}
";
    }
    
}

#endif
