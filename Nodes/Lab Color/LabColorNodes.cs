#if UNITY_EDITOR
using System;
using UnityEditor.ShaderGraph;
using System.Reflection;
using UnityEditor.Graphing;
using UnityEditor.ShaderGraph.Drawing.Controls;
using UnityEngine;

[Title("Artistic", "Utility", "Color Scheme Complimentary")]
internal class ComplimentaryNode : CodeFunctionNode {
    public ComplimentaryNode() {
        name = "Color Scheme Complimentary";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("Complimentary", BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string Complimentary([Slot(0, Binding.None)] Vector4 In, [Slot(1, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{
    const float3x3 RGBXYZ = float3x3(0.4124564,  0.3575761,  0.1804375, 
                                    0.2126729,  0.7151522,  0.0721750,
                                    0.0193339,  0.1191920,  0.9503041);
    const float3x3 XYZRGB = float3x3(3.2404542, -1.5371385, -0.4985314,
                                    -0.9692660,  1.8760108,  0.0415560,
                                     0.0556434, -0.2040259,  1.0572252);
    const float3 n = float3(95.047, 100.000, 108.883);
    Out.w = In.w;

    float3 RGB = In.xyz;
    float3 XYZ = mul(RGBXYZ, RGB)/n;
    float3 f = float3(XYZ > 0.00885645167) ? pow(XYZ, 0.3333333333) : 7.7870370374 * XYZ + 0.13793103448;
    float3 Lab = float3(116 * f.y - 16, 500 * (f.x-f.y), 200 * (f.y-f.z));

    Lab = float3(Lab.x, -Lab.y, -Lab.z);
    XYZ = float3(Lab.y/500, 0, -Lab.z/200) + (Lab.x + 16)/116;
    XYZ = n * (float3(XYZ > 0.20689655172) ? XYZ*XYZ*XYZ : 0.12841854934 * (XYZ - 0.13793103448));
    RGB = mul(XYZRGB, XYZ);
    Out.xyz = RGB;
}
";
    }
}

[Title("Artistic", "Utility", "Color Scheme Split")]
internal class SplitNode : CodeFunctionNode {
    public SplitNode() {
        name = "Color Scheme Split";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("Split", BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string Split([Slot(0, Binding.None)] Vector4 In, [Slot(1, Binding.None, 120, 0, 0, 0)] Vector1 Angle, [Slot(2, Binding.None)] out Vector4 Out1, [Slot(3, Binding.None)] out Vector4 Out2) {
        Out1 = Vector4.zero;
        Out2 = Vector4.zero;
        return @"
{
    Angle = radians(Angle);
    const float3x3 RGBXYZ = float3x3(0.4124564,  0.3575761,  0.1804375, 
                                    0.2126729,  0.7151522,  0.0721750,
                                    0.0193339,  0.1191920,  0.9503041);
    const float3x3 XYZRGB = float3x3(3.2404542, -1.5371385, -0.4985314,
                                    -0.9692660,  1.8760108,  0.0415560,
                                     0.0556434, -0.2040259,  1.0572252);
    const float3 n = float3(95.047, 100.000, 108.883);
    Out1.w = In.w;
    Out2.w = In.w;

    float3 RGB = In.xyz;
    float3 XYZ = mul(RGBXYZ, RGB)/n;
    float3 f = float3(XYZ > 0.00885645167) ? pow(XYZ, 0.3333333333) : 7.7870370374 * XYZ + 0.13793103448;
    float3 Lab = float3(116 * f.y - 16, 500 * (f.x-f.y), 200 * (f.y-f.z));
    float3 LCH = float3(Lab.x, sqrt(Lab.y*Lab.y + Lab.z*Lab.z), (Lab.z == 0 && Lab.y == 0) ? 0 : atan2(Lab.z, Lab.y));

    Lab = float3(LCH.x, LCH.y*cos(LCH.z+ Angle), LCH.y*sin(LCH.z + Angle));
    XYZ = float3(Lab.y/500, 0, -Lab.z/200) + (Lab.x + 16)/116;
    XYZ = n * (float3(XYZ > 0.20689655172) ? XYZ*XYZ*XYZ : 0.12841854934 * (XYZ - 0.13793103448));
    RGB = mul(XYZRGB, XYZ);
    Out1.xyz = RGB;

    Lab = float3(LCH.x, LCH.y*cos(LCH.z - Angle), LCH.y*sin(LCH.z - Angle));
    XYZ = float3(Lab.y/500, 0, -Lab.z/200) + (Lab.x + 16)/116;
    XYZ = n * (float3(XYZ > 0.20689655172) ? XYZ*XYZ*XYZ : 0.12841854934 * (XYZ - 0.13793103448));
    RGB = mul(XYZRGB, XYZ);
    Out2.xyz = RGB;
}
";
    }
}

[Title("Artistic", "Utility", "Color Scheme Dual")]
internal class DualNode : CodeFunctionNode {
    public DualNode() {
        name = "Color Scheme Dual";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("Dual", BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string Dual([Slot(0, Binding.None)] Vector4 In, [Slot(1, Binding.None, 90, 0, 0, 0)] Vector1 Angle, [Slot(2, Binding.None)] out Vector4 Out1, [Slot(3, Binding.None)] out Vector4 Out2, [Slot(4, Binding.None)] out Vector4 Out3) {
        Out1 = Vector4.zero;
        Out2 = Vector4.zero;
        Out3 = Vector4.zero;
        return @"
{
    Angle = radians(Angle);
    const float3x3 RGBXYZ = float3x3(0.4124564,  0.3575761,  0.1804375, 
                                    0.2126729,  0.7151522,  0.0721750,
                                    0.0193339,  0.1191920,  0.9503041);
    const float3x3 XYZRGB = float3x3(3.2404542, -1.5371385, -0.4985314,
                                    -0.9692660,  1.8760108,  0.0415560,
                                     0.0556434, -0.2040259,  1.0572252);
    const float3 n = float3(95.047, 100.000, 108.883);
    Out1.w = In.w;
    Out2.w = In.w;
    Out3.w = In.w;

    float3 RGB = In.xyz;
    float3 XYZ = mul(RGBXYZ, RGB)/n;
    float3 f = float3(XYZ > 0.00885645167) ? pow(XYZ, 0.3333333333) : 7.7870370374 * XYZ + 0.13793103448;
    float3 Lab = float3(116 * f.y - 16, 500 * (f.x-f.y), 200 * (f.y-f.z));
    float3 LCH = float3(Lab.x, sqrt(Lab.y*Lab.y + Lab.z*Lab.z), (Lab.z == 0 && Lab.y == 0) ? 0 : atan2(Lab.z, Lab.y));

    Lab = float3(Lab.x, -Lab.y, -Lab.z);
    XYZ = float3(Lab.y/500, 0, -Lab.z/200) + (Lab.x + 16)/116;
    XYZ = n * (float3(XYZ > 0.20689655172) ? XYZ*XYZ*XYZ : 0.12841854934 * (XYZ - 0.13793103448));
    RGB = mul(XYZRGB, XYZ);
    Out2.xyz = RGB;

    Lab = float3(LCH.x, LCH.y*cos(LCH.z+ Angle), LCH.y*sin(LCH.z + Angle));
    XYZ = float3(Lab.y/500, 0, -Lab.z/200) + (Lab.x + 16)/116;
    XYZ = n * (float3(XYZ > 0.20689655172) ? XYZ*XYZ*XYZ : 0.12841854934 * (XYZ - 0.13793103448));
    RGB = mul(XYZRGB, XYZ);
    Out1.xyz = RGB;

    Lab = float3(Lab.x, -Lab.y, -Lab.z);
    XYZ = float3(Lab.y/500, 0, -Lab.z/200) + (Lab.x + 16)/116;
    XYZ = n * (float3(XYZ > 0.20689655172) ? XYZ*XYZ*XYZ : 0.12841854934 * (XYZ - 0.13793103448));
    RGB = mul(XYZRGB, XYZ);
    Out3.xyz = RGB;
}
";
    }
}

[Serializable]
public enum LabColorspace {
    RGB,
    Lab,
    LCH
}

[Serializable]
public struct LabColorspaceConversion : IEnumConversion {
    public LabColorspace from;
    public LabColorspace to;

    public LabColorspaceConversion(LabColorspace from, LabColorspace to) {
        this.from = from;
        this.to = to;
    }

    Enum IEnumConversion.from {
        get { return from; }
        set { from = (LabColorspace) value; }
    }

    Enum IEnumConversion.to {
        get { return to; }
        set { to = (LabColorspace) value; }
    }
}

[Title("Artistic", "Utility", "Lab Colorspace Conversion")]
internal class LabColorspaceConversionNode : CodeFunctionNode {
    [SerializeField] private LabColorspaceConversion m_Conversion = new LabColorspaceConversion(LabColorspace.RGB, LabColorspace.RGB);

    public LabColorspaceConversionNode() {
        name = "Lab Colorspace Conversion";
    }

    [EnumConversionControl]
    private LabColorspaceConversion conversion {
        get { return m_Conversion; }
        set {
            if (m_Conversion.Equals(value))
                return;
            m_Conversion = value;
            Dirty(ModificationScope.Graph);
        }
    }

    private string GetSpaceFrom() {
        return Enum.GetName(typeof(LabColorspace), conversion.from);
    }

    private string GetSpaceTo() {
        return Enum.GetName(typeof(LabColorspace), conversion.to);
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod(string.Format("LabColorspaceConversion_{0}_{1}", GetSpaceFrom(), GetSpaceTo()), BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string LabColorspaceConversion_RGB_RGB([Slot(0, Binding.None)] Vector4 In, [Slot(1, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{ 
    Out = In; 
}
";
    }
    
    private static string LabColorspaceConversion_RGB_Lab([Slot(0, Binding.None)] Vector4 In, [Slot(1, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{ 
    const float3x3 RGBXYZ = float3x3(0.4124564,  0.3575761,  0.1804375, 
                                    0.2126729,  0.7151522,  0.0721750,
                                    0.0193339,  0.1191920,  0.9503041);
    const float3 n = float3(95.047, 100.000, 108.883);

    float3 RGB = In.xyz;
    float3 XYZ = mul(RGBXYZ, RGB)/n;
    float3 f = float3(XYZ > 0.00885645167) ? pow(XYZ, 0.3333333333) : 7.7870370374 * XYZ + 0.13793103448;
    float3 Lab = float3(116 * f.y - 16, 500 * (f.x-f.y), 200 * (f.y-f.z));
    Out.xyz = Lab;
    Out.w = In.w;
}
";
    } 
    
    private static string LabColorspaceConversion_RGB_LCH([Slot(0, Binding.None)] Vector4 In, [Slot(1, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{ 
    const float3x3 RGBXYZ = float3x3(0.4124564,  0.3575761,  0.1804375, 
                                    0.2126729,  0.7151522,  0.0721750,
                                    0.0193339,  0.1191920,  0.9503041);
    const float3 n = float3(95.047, 100.000, 108.883);

    float3 RGB = In.xyz;
    float3 XYZ = mul(RGBXYZ, RGB)/n;
    float3 f = float3(XYZ > 0.00885645167) ? pow(XYZ, 0.3333333333) : 7.7870370374 * XYZ + 0.13793103448;
    float3 Lab = float3(116 * f.y - 16, 500 * (f.x-f.y), 200 * (f.y-f.z));
    float3 LCH = float3(Lab.x, sqrt(Lab.y*Lab.y + Lab.z*Lab.z), (Lab.z == 0 && Lab.y == 0) ? 0 : atan2(Lab.z, Lab.y));
    Out.xyz = LCH;
    Out.w = In.w;
}
";
    } 

    private static string LabColorspaceConversion_Lab_RGB([Slot(0, Binding.None)] Vector4 In, [Slot(1, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{ 
    const float3x3 XYZRGB = float3x3(3.2404542, -1.5371385, -0.4985314,
                                    -0.9692660,  1.8760108,  0.0415560,
                                     0.0556434, -0.2040259,  1.0572252);
    const float3 n = float3(95.047, 100.000, 108.883);

    float3 Lab = In.xyz;
    float3 XYZ = float3(Lab.y/500, 0, -Lab.z/200) + (Lab.x + 16)/116;
    XYZ = n * (float3(XYZ > 0.20689655172) ? XYZ*XYZ*XYZ : 0.12841854934 * (XYZ - 0.13793103448));
    float3 RGB = mul(XYZRGB, XYZ);
    Out.xyz = RGB;
    Out.w = In.w;
}
";
    } 

    private static string LabColorspaceConversion_Lab_Lab([Slot(0, Binding.None)] Vector4 In, [Slot(1, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{ 
    Out = In; 
}
";
    } 

    private static string LabColorspaceConversion_Lab_LCH([Slot(0, Binding.None)] Vector4 In, [Slot(1, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{ 
    float3 Lab = In.xyz;
    float3 LCH = float3(Lab.x, sqrt(Lab.y*Lab.y + Lab.z*Lab.z), (Lab.z == 0 && Lab.y == 0) ? 0 : atan2(Lab.z, Lab.y));
    Out.xyz = LCH;
    Out.w = In.w;
}
";
    } 

    private static string LabColorspaceConversion_LCH_RGB([Slot(0, Binding.None)] Vector4 In, [Slot(1, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{ 
    const float3x3 XYZRGB = float3x3(3.2404542, -1.5371385, -0.4985314,
                                    -0.9692660,  1.8760108,  0.0415560,
                                     0.0556434, -0.2040259,  1.0572252);
    const float3 n = float3(95.047, 100.000, 108.883);

    float3 LCH = In.xyz;
    float3 Lab = float3(LCH.x, LCH.y*cos(LCH.z), LCH.y*sin(LCH.z));
    float3 XYZ = float3(Lab.y/500, 0, -Lab.z/200) + (Lab.x + 16)/116;
    XYZ = n * (float3(XYZ > 0.20689655172) ? XYZ*XYZ*XYZ : 0.12841854934 * (XYZ - 0.13793103448));
    float3 RGB = mul(XYZRGB, XYZ);
    Out.xyz = RGB;
    Out.w = In.w;
}
";
    } 
    
    private static string LabColorspaceConversion_LCH_Lab([Slot(0, Binding.None)] Vector4 In, [Slot(1, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{ 
    float3 LCH = In.xyz;
    float3 Lab = float3(LCH.x, LCH.y*cos(LCH.z), LCH.y*sin(LCH.z));
    Out.xyz = Lab;
    Out.w = In.w;
}
";
    } 

    private static string LabColorspaceConversion_LCH_LCH([Slot(0, Binding.None)] Vector4 In, [Slot(1, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{ 
    Out = In; 
}
";
    } 
}

#endif