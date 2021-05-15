#if UNITY_EDITOR
using UnityEditor.ShaderGraph;
using System.Reflection;
using UnityEngine;

[Title("Math", "Quaternion", "Quaternion Inverse")]
internal class QuaternionInverseNode : CodeFunctionNode {
    public QuaternionInverseNode() {
        name = "Quaternion Inverse";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("QuaternionInverse", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string QuaternionInverse([Slot(0, Binding.None, 0, 0, 0, 1)] Vector4 Q, [Slot(1, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{
    Out.xyz = -Q.xyz;
    Out.w = Q.w;
}
";
    }
}

[Title("Math", "Quaternion", "Quaternion From Euler")]
internal class QuaternionFromEulerNode : CodeFunctionNode {
    public QuaternionFromEulerNode() {
        name = "Quaternion From Euler";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("QuaternionFromEuler", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string QuaternionFromEuler([Slot(0, Binding.None)] Vector3 V, [Slot(1, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{
	float3 Sin, Cos;
    sincos(0.5 * 0.0174533 * V, Sin, Cos);
    Out.x = Cos.y * Sin.x * Cos.z + Sin.y * Cos.x * Sin.z;
    Out.y = Sin.y * Cos.x * Cos.z - Cos.y * Sin.x * Sin.z;
    Out.z = Cos.y * Cos.x * Sin.z - Sin.y * Sin.x * Cos.z;
    Out.w = Cos.y * Cos.x * Cos.z + Sin.y * Sin.x * Sin.z;
} 
";
    }
}

[Title("Math", "Quaternion", "Quaternion From Angle Axis")]
internal class QuaternionFromAngleAxisNode : CodeFunctionNode {
    public QuaternionFromAngleAxisNode() {
        name = "Quaternion From Angle Axis";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("QuaternionFromAngleAxis", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string QuaternionFromAngleAxis([Slot(0, Binding.None)] Vector1 Angle, [Slot(1, Binding.None, 1, 0, 0, 0)] Vector3 Axis, [Slot(2, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{
    float Sin, Cos;
    sincos(0.5 * 0.0174533 * Angle, Sin, Cos);
    Out.xyz = Sin * Axis;
    Out.w = Cos;
} 
";
    }
}

[Title("Math", "Quaternion", "Quaternion To Angle Axis")]
internal class QuaternionToAngleAxisNode : CodeFunctionNode {
    public QuaternionToAngleAxisNode() {
        name = "Quaternion To Angle Axis";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("QuaternionToAngleAxis", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string QuaternionToAngleAxis([Slot(0, Binding.None, 0, 0, 0, 1)] Vector4 Q, [Slot(1, Binding.None)] out Vector1 Angle, [Slot(2, Binding.None)] out Vector3 Axis) {
        Axis = Vector3.zero;
        return @"
{
    float Length = sqrt(dot(Q.xyz, Q.xyz));
    Axis = 1 / Length * Q.xyz;
    Angle = 57.2958 * 2 * atan2(Length, Q.w);
} 
";
    }
}

[Title("Math", "Quaternion", "Quaternion From To Rotation")]
internal class QuaternionFromToRotationNode : CodeFunctionNode {
    public QuaternionFromToRotationNode() {
        name = "Quaternion From To Rotation Node";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("QuaternionFromToRotation", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string QuaternionFromToRotation([Slot(0, Binding.None)] Vector3 From, [Slot(1, Binding.None)] Vector3 To, [Slot(2, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{
    Out.xyz = cross(From, To);
    Out.w = sqrt(dot(From,From)*dot(To,To)) + dot(From, To);
} 
";
    }
}

[Title("Math", "Quaternion", "Quaternion Multiply")]
internal class QuaternionMultiplyNode : CodeFunctionNode {
    public QuaternionMultiplyNode() {
        name = "Quaternion Multiply";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("QuaternionMultiply", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string QuaternionMultiply([Slot(0, Binding.None, 0, 0, 0, 1)] Vector4 P, [Slot(1, Binding.None, 0, 0, 0, 1)] Vector4 Q, [Slot(2, Binding.None)] out Vector4 Out) {
        Out = Vector4.zero;
        return @"
{
    Out.xyz = P.w * Q.xyz + Q.w * P.xyz + cross(P.xyz, Q.xyz);
    Out.w = P.w * Q.w - dot(P.xyz, Q.xyz);
} 
";
    }
}

[Title("Math", "Quaternion", "Quaternion Rotate Vector")]
internal class QuaternionRotateVectorNode : CodeFunctionNode {
    public QuaternionRotateVectorNode() {
        name = "Quaternion Rotate Vector";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("QuaternionRotateVector", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string QuaternionRotateVector([Slot(0, Binding.None)] Vector3 V, [Slot(1, Binding.None, 0, 0, 0, 1)] Vector4 Q, [Slot(2, Binding.None)] out Vector3 Out) {
        Out = Vector3.zero;
        return @"
{
    float3 Q1 = Q.w * V - Q.xyz - cross(Q.xyz, V);
	Out = (Q.w + dot(Q.xyz, V)) * Q.xyz + Q.w * Q1 + cross(Q1, Q.xyz);
}
";
    }
}

[Title("Math", "Quaternion", "Quaternion Slerp")]
internal class QuaternionSlerpNode : CodeFunctionNode {
    public QuaternionSlerpNode() {
        name = "Quaternion Slerp";
    }

    protected override MethodInfo GetFunctionToConvert() {
        return GetType().GetMethod("QuaternionSlerp", BindingFlags.Static | BindingFlags.NonPublic);
    }

    static string QuaternionSlerp([Slot(0, Binding.None, 0, 0, 0, 1)] Vector4 P, [Slot(1, Binding.None, 0, 0, 0, 1)] Vector4 Q, [Slot(2, Binding.None)] Vector1 T, [Slot(3, Binding.None)] out Vector4 Out) {
        Out = new Vector4(0, 0, 0, 1);
        return @"
{
    float CosHalfAngle = P.w * Q.w + dot(P.xyz, Q.xyz);
    float HalfAngle = acos(CosHalfAngle);
    float CosecHalfAngle = 1 / sin(HalfAngle);
    Out = normalize((sin(HalfAngle * (1 - T)) * CosecHalfAngle) * (CosHalfAngle > 0 ? P : -P) + (sin(HalfAngle * T) * CosecHalfAngle) * Q);
} 
";
    }
}
#endif