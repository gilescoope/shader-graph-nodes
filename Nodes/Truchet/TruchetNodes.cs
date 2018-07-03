#if UNITY_EDITOR
using UnityEditor.ShaderGraph;
using System.Reflection;
using UnityEngine;


[Title("Truchet", "Truchet Triangle")]
public class TruchetTriangleNode : CodeFunctionNode {
    public TruchetTriangleNode() {
        name = "Truchet Triangle";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("TruchetTriangle", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string TruchetTriangle([Slot(0, Binding.WorldSpacePosition)] Vector2 Position, [Slot(1, Binding.None, 0.25f, 0, 0, 0)] Vector1 Size, [Slot(2, Binding.None, 1, 0, 0, 0)] Vector1 Number, [Slot(3, Binding.None)] Vector1 Seed, [Slot(4, Binding.None)] CodeFunctionNode.Boolean Rotate, [Slot(5, Binding.None)] CodeFunctionNode.Boolean Reflect, [Slot(6, Binding.None)] out Vector1 Index, [Slot(7, Binding.None)] out Vector2 UV) {
        UV = Vector2.zero;
        return @"
{
    const float3 Cos = float3(1, 0.5, -0.5);
    const float3 Sin = float3(0, 0.86602540378, 0.86602540378);

    const float2 a = float2(1, 0);
    const float2 b = float2(0.5, 0.86602540378);
    
    UV = Position / Size;
    float d = (a.x*b.y-a.y*b.x);
    float ta = (b.y*UV.x-b.x*UV.y)/d;
    float tb = (a.x*UV.y-a.y*UV.x)/d;

    float3 cell = floor(float3(ta, tb, frac(ta) +frac(tb)));
    float rand = frac(sin(dot(cell + Seed, float3(12.9898,78.233, 45.652))) * 43758.5453);
    float nRotation = Rotate ? 3 : 1;
    float nReflection = Reflect ? 2 : 1;
    float randInt = floor(rand * Number * nRotation * nReflection);
    Index = randInt % Number;
    randInt = floor(randInt / Number);
    float Rot = cell.z + 2*(randInt % nRotation);
    randInt = floor(randInt / nRotation);
    float Refl = randInt % nReflection;
    UV = UV - a*(cell.x + 0.3333333333*(1 + cell.z)) - b*(cell.y + 0.3333333333*(1 + cell.z));
    UV.x = (1-2*Refl) * UV.x;
    UV = 1.73205080757*UV;
    UV = 0.5+ 0.5*(Rot >= 3 ? -1 : 1) * float2(UV.x*Cos[Rot%3] + UV.y *-Sin[Rot%3], UV.x*Sin[Rot%3] + UV.y *Cos[Rot%3]);
}
";
    }
}

[Title("Truchet", "Truchet Square")]
public class TruchetSquareNode : CodeFunctionNode {
    public TruchetSquareNode() {
        name = "Truchet Square";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("TruchetSquare", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string TruchetSquare([Slot(0, Binding.WorldSpacePosition)] Vector2 Position, [Slot(1, Binding.None, 0.25f, 0, 0, 0)] Vector1 Size, [Slot(2, Binding.None, 1, 0, 0, 0)] Vector1 Number, [Slot(3, Binding.None)] Vector1 Seed, [Slot(4, Binding.None)] CodeFunctionNode.Boolean Rotate, [Slot(5, Binding.None)] CodeFunctionNode.Boolean Reflect, [Slot(6, Binding.None)] out Vector1 Index, [Slot(7, Binding.None)] out Vector2 UV) {
        UV = Vector2.zero;
        return @"
{
    const float4 Cos = float4(1, 0, -1, 0);
    const float4 Sin = float4(0, 1, 0, -1);
    UV = Position / Size;
    float2 cell = floor(UV);
    float rand = frac(sin(dot(cell.xy + Seed, float2(12.9898,78.233))) * 43758.5453);
    float nRotation = Rotate ? 4 : 1;
    float nReflection = Reflect ? 2 : 1;
    float randInt = floor(rand * Number * nRotation * nReflection);
    Index = randInt % Number;
    randInt = floor(randInt / Number);
    float Rot = randInt % nRotation;
    randInt = floor(randInt / nRotation);
    float Refl = randInt % nReflection;
    UV = UV - cell - 0.5;
    UV.x = (1-2*Refl) * UV.x;
    UV = 0.5 + float2(UV.x*Cos[Rot] + UV.y *-Sin[Rot], UV.x*Sin[Rot] + UV.y *Cos[Rot]);
}
";
    }
}

[Title("Truchet", "Truchet Hexagon")]
public class TruchetHexagonNode : CodeFunctionNode {
    public TruchetHexagonNode() {
        name = "Truchet Hexagon";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("TruchetHexagon", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string TruchetHexagon([Slot(0, Binding.WorldSpacePosition)] Vector2 Position, [Slot(1, Binding.None, 0.25f, 0, 0, 0)] Vector1 Size, [Slot(2, Binding.None, 1, 0, 0, 0)] Vector1 Number, [Slot(3, Binding.None)] Vector1 Seed, [Slot(4, Binding.None)] CodeFunctionNode.Boolean Rotate, [Slot(5, Binding.None)] CodeFunctionNode.Boolean Reflect, [Slot(6, Binding.None)] out Vector1 Index, [Slot(7, Binding.None)] out Vector2 UV) {
        UV = Vector2.zero;
        return @"
{
    const float3 Cos = float3(1, 0.5, -0.5);
    const float3 Sin = float3(0, 0.86602540378, 0.86602540378);

    const float2 a = float2(1, 0);
    const float2 b = float2(0.5, 0.86602540378);
    
    UV = 2 * Position / Size;
    float d = (a.x*b.y-a.y*b.x);
    float ta = (b.y*UV.x-b.x*UV.y)/d;
    float tb = (a.x*UV.y-a.y*UV.x)/d;

    float3 cell3 = floor(float3(ta, tb, frac(ta) +frac(tb)));
    float mod = cell3.x - cell3.y;
    mod = mod - 3 * floor(mod*0.33333333333);
    float2 cell = float2(cell3.x + (mod == 0 ? cell3.z : 0) + (mod == 2 ? 1 : 0), cell3.y + (mod == 0 ? cell3.z : 0) + (mod == 1 ? 1 : 0));
    float rand = frac(sin(dot(cell.xy + Seed, float2(12.9898,78.233))) * 43758.5453);
    float nRotation = Rotate ? 6 : 1;
    float nReflection = Reflect ? 2 : 1;
    float randInt = floor(rand * Number * nRotation * nReflection);
    Index = randInt % Number;
    randInt = floor(randInt / Number);
    float Rot = randInt % nRotation;
    randInt = floor(randInt / nRotation);
    float Refl = randInt % nReflection;
    UV = UV - a * cell.x - b*cell.y;
    UV.x = (1-2*Refl) * UV.x;
    UV = 0.5+ 0.5*(Rot >= 3 ? -1 : 1) * float2(UV.x*Cos[Rot%3] + UV.y *-Sin[Rot%3], UV.x*Sin[Rot%3] + UV.y *Cos[Rot%3]);
}
";
    }
}
#endif