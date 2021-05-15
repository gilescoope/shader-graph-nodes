#if UNITY_EDITOR
using System;
using UnityEditor.ShaderGraph;
using System.Reflection;
using UnityEditor.Graphing;
using UnityEditor.ShaderGraph.Drawing.Controls;
using UnityEngine;

[Serializable]
public enum HalftoneMode {
    Circle,
    Smooth,
    Square
}

[Title("Halftone", "Halftone Monochrome")]
internal class HalftoneMonochromeNode : CodeFunctionNode {
    [SerializeField] private HalftoneMode m_HalftoneMode = HalftoneMode.Circle;

    public HalftoneMonochromeNode() {
        name = "Halftone Monochrome";
    }

    private string GetCurrentModeName() {
        return Enum.GetName(typeof(HalftoneMode), m_HalftoneMode);
    }

    [EnumControl("Mode")]
    public HalftoneMode halftoneMode {
        get { return m_HalftoneMode; }
        set {
            if (m_HalftoneMode == value)
                return;
            m_HalftoneMode = value;
            Dirty(ModificationScope.Graph);
        }
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod(string.Format("HalftoneMonochrome_{0}", GetCurrentModeName()), BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string HalftoneMonochrome_Circle([Slot(0, Binding.None)] Vector1 Base, [Slot(1, Binding.WorldSpacePosition)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)]
        Vector2 Offset, [Slot(3, Binding.None)] out Vector1 Out) {
        return @"
{
	float Scale = 0.78;
	float2 Direction = Offset/dot(Offset, Offset);
	float2 Position = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	Out = Scale*dot(Position, Position)/(0.25*(1-Base));
	Out = 1-saturate((1 - Out) / fwidth(Out));
}
";
    }

    private static string HalftoneMonochrome_Smooth([Slot(0, Binding.None)] Vector1 Base, [Slot(1, Binding.WorldSpacePosition)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)]
        Vector2 Offset, [Slot(3, Binding.None)] out Vector1 Out) {
        return @"
{
	float2 Direction = Offset/dot(Offset, Offset);
	float2 Position1 = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	float2 Position2 = fmod(Position1 + 1, 1) - 0.5;
	float P1P1 = dot(Position1, Position1);
	float P2P2 = dot(Position2, Position2);
	float T = P1P1/(P1P1 + P2P2);
	Out.x =  (1-T)*(P1P1 - 0.25*(1-Base))-T*(P2P2 - 0.25*Base);
	Out = 1-saturate((-Out) / fwidth(Out));

}
";
    }

    private static string HalftoneMonochrome_Square([Slot(0, Binding.None)] Vector1 Base, [Slot(1, Binding.WorldSpacePosition)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)]
        Vector2 Offset, [Slot(3, Binding.None)] out Vector1 Out) {
        return @"
{
	float2 Direction = Offset/dot(Offset, Offset);
	float2 Position = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	float Radius = 0.5*sqrt(1-Base);
	Out = max(abs(Position.x), abs(Position.y))/Radius;
	Out = 1-saturate((1 - Out) / fwidth(Out));
}
";
    }
}

[Title("Halftone", "Halftone Color")]
internal class HalftoneColorNode : CodeFunctionNode {
    [SerializeField] private HalftoneMode m_HalftoneMode = HalftoneMode.Circle;

    public HalftoneColorNode() {
        name = "Halftone Color";
    }

    private string GetCurrentModeName() {
        return Enum.GetName(typeof(HalftoneMode), m_HalftoneMode);
    }

    [EnumControl("Mode")]
    public HalftoneMode halftoneMode {
        get { return m_HalftoneMode; }
        set {
            if (m_HalftoneMode == value)
                return;
            m_HalftoneMode = value;
            Dirty(ModificationScope.Graph);
        }
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod(string.Format("HalftoneColor_{0}", GetCurrentModeName()), BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string HalftoneColor_Circle([Slot(0, Binding.None)] Vector3 Base, [Slot(1, Binding.WorldSpacePosition)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)]
        Vector2 OffsetR, [Slot(3, Binding.None, 0.00866f, 0.005f, 0, 0)]
        Vector2 OffsetG, [Slot(4, Binding.None, 0.005f, 0.00866f, 0, 0)]
        Vector2 OffsetB, [Slot(5, Binding.None)] out Vector3 Out) {
        Out = new Vector3();
        return @"
{
	float Scale = 0.78;
	float2 Direction = OffsetR/dot(OffsetR, OffsetR);
	float2 Position = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
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

    static string HalftoneColor_Smooth([Slot(0, Binding.None)] Vector3 Base, [Slot(1, Binding.WorldSpacePosition)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)]
        Vector2 OffsetR, [Slot(3, Binding.None, 0.00866f, 0.005f, 0, 0)]
        Vector2 OffsetG, [Slot(4, Binding.None, 0.005f, 0.00866f, 0, 0)]
        Vector2 OffsetB, [Slot(5, Binding.None)] out Vector3 Out) {
        Out = new Vector3();
        return @"
{
	float2 Direction = OffsetR/dot(OffsetR, OffsetR);
	float2 Position1 = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	float2 Position2 = fmod(Position1 + 1, 1) - 0.5;
	float P1P1 = dot(Position1, Position1);
	float P2P2 = dot(Position2, Position2);
	float T = P1P1/(P1P1 + P2P2);
	Out.x = (1-T)*(P1P1 - 0.25*(1-Base.x))-T*(P2P2 - 0.25*Base.x);
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

    static string HalftoneColor_Square([Slot(0, Binding.None)] Vector3 Base, [Slot(1, Binding.WorldSpacePosition)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)]
        Vector2 OffsetR, [Slot(3, Binding.None, 0.00866f, 0.005f, 0, 0)]
        Vector2 OffsetG, [Slot(4, Binding.None, 0.005f, 0.00866f, 0, 0)]
        Vector2 OffsetB, [Slot(5, Binding.None)] out Vector3 Out) {
        Out = new Vector3();
        return @"
{
	float2 Direction = OffsetR/dot(OffsetR, OffsetR);
	float2 Position = fmod(abs(float2(dot(UV, Direction), -UV.x*Direction.y + UV.y*Direction.x)) + 0.5, 1) - 0.5;
	float Radius = 0.5*sqrt(1-Base.x);
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