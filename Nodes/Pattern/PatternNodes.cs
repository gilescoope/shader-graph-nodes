#if UNITY_EDITOR
using UnityEditor.ShaderGraph;
using System.Reflection;
using UnityEngine;

[Title("Pattern", "Zig Zag")]
internal class ZigZagNode : CodeFunctionNode {
    public ZigZagNode() {
        name = "Zig Zag";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("ZigZag", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string ZigZag([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None, 0.1f, 0.1f, 0, 0)]
        Vector2 Widths, [Slot(2, Binding.None, 0.5f, 0, 0, 0)] Vector1 Wavelength, [Slot(3, Binding.None, 0.1f, 0, 0, 0)] Vector1 Amplitude, [Slot(4, Binding.None)] out Vector1 Out) {
        return @"
{
    float Width = Widths.x + Widths.y;
    Out = ((UV.y - Amplitude * (1-2* abs(frac(UV.x/Wavelength) - 0.5)))%Width + Width) % Width;
    Out = abs(Out - 0.5*Width) - 0.5* Widths.y;
    Out = saturate(Out/fwidth(Out));
}
";
    }
}

[Title("Pattern", "Sine Waves")]
internal class SineWavesNode : CodeFunctionNode {
    public SineWavesNode() {
        name = "Sine Waves";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("SineWaves", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string SineWaves([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None, 0.1f, 0.1f, 0, 0)]
        Vector2 Widths, [Slot(2, Binding.None, 0.5f, 0, 0, 0)] Vector1 Wavelength, [Slot(3, Binding.None, 0.1f, 0, 0, 0)] Vector1 Amplitude, [Slot(4, Binding.None)] out Vector1 Out) {
        return @"
{
    float Width = Widths.x + Widths.y;
    float y = 0.5*Amplitude * sin(6.28318530718 * UV.x/Wavelength);
    Out = ((UV.y - y)%Width + Width) % Width;
    Out = abs(Out - 0.5*Width) - 0.5* Widths.y;
    Out = saturate(Out/fwidth(Out));
}
";
    }
}

[Title("Pattern", "Round Waves")]
internal class RoundWavesNode : CodeFunctionNode {
    public RoundWavesNode() {
        name = "Round Waves";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("RoundWaves", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string RoundWaves([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None, 0.1f, 0.1f, 0, 0)]
        Vector2 Widths, [Slot(2, Binding.None, 0.5f, 0, 0, 0)] Vector1 Wavelength, [Slot(3, Binding.None, 0.1f, 0, 0, 0)] Vector1 Amplitude, [Slot(4, Binding.None)] out Vector1 Out) {
        return @"
{
    float Width = Widths.x + Widths.y;
    float x = (((UV.x % Wavelength) + Wavelength) % Wavelength);
    float X = 0.25*Wavelength;
    float Y = 0.5*Amplitude;
    float Offset = (X*X - Y*Y)/(2*Y);
    float R = Offset + Y;
    float x2 = x < 0.5 * Wavelength ? 0.25 * Wavelength - x : 0.75*Wavelength - x;
    float y = (x < 0.5 * Wavelength ? 1 : -1) * (sqrt(R*R - x2*x2) - Offset);
    Out = ((UV.y - y)%Width + Width) % Width;
    Out = abs(Out - 0.5*Width) - 0.5* Widths.y;
    Out = saturate(Out/fwidth(Out));
}
";
    }
}

[Title("Pattern", "Dots")]
internal class DotsNode : CodeFunctionNode {
    public DotsNode() {
        name = "Dots";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("Dots", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string Dots([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None, 0.2f, 0.2f, 0, 0)]
        Vector2 Spacing, [Slot(2, Binding.None, 0.1f, 0, 0, 0)] Vector1 Offset, [Slot(3, Binding.None, 0.05f, 0, 0, 0)]
        Vector1 Radius, [Slot(4, Binding.None)] out Vector1 Out) {
        return @"
{
    float d = Spacing.x*Spacing.y;
    float tx = (Spacing.y*UV.x-Offset*UV.y)/d;
    float ty = Spacing.x*UV.y/d;
    tx = ((((tx + 0.5) % 1) + 1) %1)-0.5;
    ty = ((((ty + 0.5) % 1) + 1) %1)-0.5;
    float px = tx * Spacing.x + ty*Offset;
    float py = ty * Spacing.y;
    Out = px*px + py*py - Radius*Radius;
    Out = saturate(-Out/fwidth(Out));
}
";
    }
}

[Title("Pattern", "Spiral")]
internal class SpiralNode : CodeFunctionNode {
    public SpiralNode() {
        name = "Spiral";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("Spiral", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string Spiral([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None, 0.1f, 0.1f, 0, 0)] Vector2 Widths, [Slot(5, Binding.None)] out Vector1 Out) {
        return @"
{
    float Width = Widths.x + Widths.y;
    float r = length(UV);
    float theta = atan2(UV.y, UV.x);
    float k = (r/(Width/6.28318530718) - theta)/6.28318530718;
	Out = abs(r - ((Width/6.28318530718)*(theta + 6.28318530718*round(k)))) - 0.5*Widths.x;
    Out = saturate(-Out/fwidth(Out));
}
";
    }
}


[Title("Pattern", "Whirl")]
internal class WhirlNode : CodeFunctionNode {
    public WhirlNode() {
        name = "Whirl";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("Whirl", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string Whirl([Slot(0, Binding.WorldSpacePosition)] Vector2 UV, [Slot(1, Binding.None, 30, 30, 0, 0)] Vector2 Widths, [Slot(2, Binding.None, 1, 0, 0, 0)] Vector1 Whirl,  [Slot(3, Binding.None)] out Vector1 Out) {
        return @"
{
    float Width = Widths.x + Widths.y;
    float r = length(UV);
    float theta = 57.2958 * atan2(UV.y, UV.x) - Whirl*r;
    Out = ((((theta + 0.5*Width) % Width) + Width )%Width)- 0.5*Width;
    Out = abs(Out) - 0.5*Widths.x;
    Out = saturate(-Out/fwidth(Out));
}
";
    }
}
#endif