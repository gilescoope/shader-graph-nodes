#if UNITY_EDITOR
using System;
using UnityEditor.ShaderGraph;
using System.Reflection;
using UnityEditor.Graphing;
using UnityEditor.ShaderGraph.Drawing.Controls;
using UnityEngine;

[Serializable]
public enum CompositeMode
{
    Over,
    In,
    Out,
    Atop,
    Xor
}

[Title("Compositing", "Composite")]
internal class CompositeNode : CodeFunctionNode {
    [SerializeField]
    private CompositeMode m_CompositeMode = CompositeMode.Over;
    
    public CompositeNode() {
        name = "Composite";
    }

    private string GetCurrentBlendName()
    {
        return Enum.GetName(typeof (CompositeMode), (object) this.m_CompositeMode);
    }

    [EnumControl("Mode")]
    public CompositeMode compositeMode
    {
        get
        {
            return this.m_CompositeMode;
        }
        set
        {
            if (this.m_CompositeMode == value)
                return;
            this.m_CompositeMode = value;
            this.Dirty(ModificationScope.Graph);
        }
    }

    protected override MethodInfo GetFunctionToConvert()
    {
        return this.GetType().GetMethod(string.Format("Composite_{0}", (object) this.GetCurrentBlendName()), BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string Composite_Over([Slot(0, Binding.None)] Vector4 A, [Slot(1, Binding.None)] Vector4 B, [Slot(2, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{
    Out.w = A.w + B.w*(1-A.w);
    Out.xyz = (A.xyz*A.w + B.xyz*B.w*(1-A.w))/Out.w;
}
";
    }
    
    private static string Composite_In([Slot(0, Binding.None)] Vector4 A, [Slot(1, Binding.None)] Vector4 B, [Slot(2, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{
    Out.w = A.w * B.w;
    Out.xyz = (A.xyz*A.w*B.w)/Out.w;
}
";
    }
    
    private static string Composite_Out([Slot(0, Binding.None)] Vector4 A, [Slot(1, Binding.None)] Vector4 B, [Slot(2, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{
    Out.w = A.w * (1-B.w);
    Out.xyz = (A.xyz*A.w*(1-B.w))/Out.w;
}
";
    }
    
    private static string Composite_Atop([Slot(0, Binding.None)] Vector4 A, [Slot(1, Binding.None)] Vector4 B, [Slot(2, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{
    Out.w = B.w;
    Out.xyz = (A.xyz*A.w*B.w + B.xyz*B.w*(1-A.w))/Out.w;
}
";
    }
    
    private static string Composite_Xor([Slot(0, Binding.None)] Vector4 A, [Slot(1, Binding.None)] Vector4 B, [Slot(2, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{
    Out.w = A.w + B.w - 2*A.w*B.w;
    Out.xyz = (A.xyz*A.w*(1-B.w) + B.xyz*B.w*(1-A.w))/Out.w;
}
";
    }
}
#endif