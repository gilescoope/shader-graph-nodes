#if UNITY_EDITOR
using System;
using UnityEditor.ShaderGraph;
using System.Reflection;
using UnityEditor.Graphing;
using UnityEditor.ShaderGraph.Drawing.Controls;
using UnityEngine;

[Title("Symmetry", "Reflection Symmetry")]
internal class ReflectionNode: CodeFunctionNode {
    public ReflectionNode() {
        name = "Reflection Symmetry";
    }

    protected override MethodInfo GetFunctionToConvert()
    {
        return GetType().GetMethod("Reflection", BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string Reflection([Slot(0, Binding.None)] Vector2 UV, [Slot(1, Binding.None)] Vector2 Position, [Slot(2, Binding.None, 0, 1, 0, 0)] Vector2 Direction, [Slot(3, Binding.None)] Vector1 Glide, [Slot(4, Binding.None)] out Vector2 Out) {
        Out = Vector2.zero;
        return @"
{    
    UV = UV - Position;
    float2 Normal = float2(Direction.y, -Direction.x);
    float distance = dot(Normal, UV);
    Out = (dot(Direction, UV) + (distance < 0 ? -Glide : 0)) * Direction + abs(distance) * Normal;
    Out = Out + Position;
}
";
    }
}

[Title("Symmetry", "Rotation Symmetry")]
internal class RotationNode: CodeFunctionNode {
    public RotationNode() {
        name = "Rotation Symmetry";
    }

    protected override MethodInfo GetFunctionToConvert()
    {
        return GetType().GetMethod("Rotation", BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string Rotation([Slot(0, Binding.None)] Vector2 UV, [Slot(1, Binding.None)] Vector2 Position, [Slot(2, Binding.None)] Vector1 Angle, [Slot(3, Binding.None, 2, 0, 0, 0)] Vector1 Order, [Slot(4, Binding.None)] out Vector2 Out) {
        Out = Vector2.zero;
        return @"
{    
    UV = UV - Position;
    float angle = atan2(UV.y, UV.x) - Angle;
    float rotation = -floor(angle * Order * 0.15915494309) / (Order * 0.15915494309);
    float Sin, Cos;
    sincos(rotation, Sin, Cos);
    Out.x = Cos * UV.x - Sin * UV.y;
    Out.y = Sin * UV.x + Cos * UV.y;
    Out = Out + Position;
}
";
    }
}

[Serializable]
public enum TilingMode {
    Square,
    Hexagon,
    Triangle,
    Herringbone
}


[Title("Symmetry", "Tiling Symmetry")]
internal class TilingNode : CodeFunctionNode {
    [SerializeField] private TilingMode m_TilingMode = TilingMode.Square;

    public TilingNode() {
        name = "Tiling Symmetry";
    }

    private string GetCurrentModeName() {
        return Enum.GetName(typeof(TilingMode), m_TilingMode);
    }

    [EnumControl("Mode")]
    public TilingMode tilingMode {
        get { return m_TilingMode; }
        set {
            if (m_TilingMode == value)
                return;
            m_TilingMode = value;
            Dirty(ModificationScope.Graph);
        }
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod(string.Format("Tiling_{0}", GetCurrentModeName()), BindingFlags.Static | BindingFlags.NonPublic);
    }

    private static string Tiling_Square([Slot(0, Binding.None)] Vector2 UV, [Slot(1, Binding.None, 1, 0, 0, 0)] Vector1 CellSize, [Slot(2, Binding.None)] Vector1 Offset, [Slot(3, Binding.None)] out Vector2 Out, [Slot(4, Binding.None)] out Vector2 CellIndex, [Slot(5, Binding.None)] out Vector2 CellPosition) {
        Out = Vector2.zero;
        CellIndex = Vector2.zero;
        CellPosition = Vector2.zero;
        return @"
{   
    float2 UV2 = UV/CellSize;
    CellIndex.y = round(UV2.y);
    UV2.x = UV2.x - Offset/CellSize * CellIndex.y;
    CellIndex.x = round(UV2.x);
    CellPosition = CellIndex * CellSize + Offset * float2(CellIndex.y, 0);
    Out = UV - CellPosition;
}
";
    }

    private static string Tiling_Hexagon([Slot(0, Binding.None)] Vector2 UV, [Slot(1, Binding.None, 1, 0, 0, 0)] Vector1 CellSize, [Slot(2, Binding.None)] Vector1 Offset, [Slot(3, Binding.None)] out Vector2 Out, [Slot(4, Binding.None)] out Vector2 CellIndex, [Slot(5, Binding.None)] out Vector2 CellPosition) {
        Out = Vector2.zero;
        CellIndex = Vector2.zero;
        CellPosition = Vector2.zero;
        return @"
{   
    const float root3 = 1.73205080757;
    
    const float2x2 XHex = float2x2(1, -root3, 2, 0);
    const float2x2 YHex = float2x2(1, root3, -1, root3);
    const float2x2 PositionIndex = float2x2(1, 0.5, 0, 0.5*root3);
        
    float2 UV2 = UV/CellSize;

    float2 X = floor(mul(XHex, UV2));
    float2 Y = floor(mul(YHex, UV2));
    CellIndex = floor((float2(X.x + X.y, Y.x + Y.y) + 2)/3);
    CellPosition = CellSize*mul(PositionIndex, CellIndex);
    Out = UV - CellPosition;
}
";
    }

    private static string Tiling_Triangle([Slot(0, Binding.None)] Vector2 UV, [Slot(1, Binding.None, 1, 0, 0, 0)] Vector1 CellSize, [Slot(2, Binding.None)] Vector1 Offset, [Slot(3, Binding.None)] out Vector2 Out, [Slot(4, Binding.None)] out Vector2 CellIndex, [Slot(5, Binding.None)] out Vector2 CellPosition) {
        Out = Vector2.zero;
        CellIndex = Vector2.zero;
        CellPosition = Vector2.zero;
        return @"
{       //-0.57735026919, 1.15470053838
    const float2x2 Tri = float2x2(1, -0.57735026919, 0, 1.15470053838);
        
    float2 UV2 = UV/CellSize;
    float2 P = mul(Tri, UV2);
    float Z = floor(frac(P.x) + frac(P.y));
    float2 floorP = floor(P);
    CellIndex = float2(2 * floorP.x + Z, floorP.y);
    CellPosition = CellSize * (float2(floorP.x + 0.5*floorP.y, 0.86602540378*floorP.y) + (2 + 2*Z) * float2(0.25, 0.14433756729));
    Out = (2*Z-1)*(UV - CellPosition);
}
";
    }

    private static string Tiling_Herringbone([Slot(0, Binding.None)] Vector2 UV, [Slot(1, Binding.None, 1, 0, 0, 0)] Vector1 CellSize, [Slot(2, Binding.None)] Vector1 Offset, [Slot(3, Binding.None)] out Vector2 Out, [Slot(4, Binding.None)] out Vector2 CellIndex, [Slot(5, Binding.None)] out Vector2 CellPosition) {
        Out = Vector2.zero;
        CellIndex = Vector2.zero;
        CellPosition = Vector2.zero;
        return @"
{   
    const float2x2 RotA = float2x2(0.7071068, 0.7071068, -0.7071068, 0.7071068);
    const float2x2 RotB = float2x2(0.7071068, -0.7071068, 0.7071068, 0.7071068);

    const float root2 = 1.73205080757;

    float Width = 0.5*CellSize;
    
    float2 P = 2*round(0.5*(UV.xx/Width - float2(0, 1))) + float2(0, 1);

    float2 X = UV.xx - Width * P;
    float2 Y = UV.yy - 0.5*P;
    float2 Q = round(Y - X * float2(1, -1));
    Y = Y - Q;
    float2 A = mul(RotA, float2(X.x, Y.x));
    float2 B = mul(RotB, float2(X.y, Y.y));
    
    if(abs(X.x + Y.x) < Width){
        Out = A;
        CellIndex = float2(P.x, Q.x);
    } else {
        Out = B;
        CellIndex = float2(P.y, Q.y);
    }
        
    CellPosition = float2(CellIndex.x * Width, 0.5*CellIndex.x + CellIndex.y);
}
";
    }
}

#endif
