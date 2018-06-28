#if UNITY_EDITOR
using UnityEditor.ShaderGraph;
using System.Reflection;
using UnityEngine;

[Title("Math", "Quaternion", "Quaternion Inverse")]
public class QuaternionInverseNode : CodeFunctionNode {
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
    float4 Out4 = {-Q.xyz, Q.w};
	Out = Out4;
}
";
    }
}

[Title("Math", "Quaternion", "Quaternion From Euler")]
public class QuaternionFromEulerNode : CodeFunctionNode {
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
	float num6, num5, num4, num3, num2, num;
	sincos(V.z * 0.5 * 0.0174533, num6, num5);
	sincos(V.x * 0.5 * 0.0174533, num4, num3);
	sincos(V.y * 0.5 * 0.0174533, num2, num);
    float4 Out4 = {num * num4 * num5 + num2 * num3 * num6, num2 * num3 * num5 - num * num4 * num6, num * num3 * num6 - num2 * num4 * num5, num * num3 * num5 + num2 * num4 * num6};
	Out = Out4;
} 
";
    }
}

[Title("Math", "Quaternion", "Quaternion From Angle Axis")]
public class QuaternionFromAngleAxisNode : CodeFunctionNode {
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
    float rad = 0.0174533 * Angle;
    float s, c;
    sincos(0.5*rad, s, c);
    float4 Out4 = {Axis.x * s, Axis.y * s, Axis.z * s, c};
    Out = Out4;
} 
";
    }
}

[Title("Math", "Quaternion", "Quaternion To Angle Axis")]
public class QuaternionToAngleAxisNode : CodeFunctionNode {
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
    float length = sqrt(dot(Q.xyz, Q.xyz));
    Axis = 1/length * Q.xyz;
    Angle = 57.2958 * 2 * atan2(length, Q.w);
} 
";
    }
}

[Title("Math", "Quaternion", "Quaternion From To Rotation")]
public class QuaternionFromToRotationNode : CodeFunctionNode {
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
    float4 Out4 = {cross(From, To), sqrt(dot(From,From)*dot(To,To)) + dot(From, To)};
    Out = normalize(Out4);
} 
";
    }
}

[Title("Math", "Quaternion", "Quaternion Multiply")]
public class QuaternionMultiplyNode : CodeFunctionNode {
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
	float4 Out4 = float4(
        Q.xyz * P.w + P.xyz * Q.w + cross(P.xyz, Q.xyz),
        P.w * Q.w - dot(P.xyz, Q.xyz)
    );
    Out = Out4;
} 
";
    }
}

[Title("Math", "Quaternion", "Quaternion Rotate Vector")]
public class QuaternionRotateVectorNode : CodeFunctionNode {
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
    float4 QInverse = {-Q.xyz, Q.w};
	float4 Q1 = float4(
        V * QInverse.w + QInverse.xyz + cross(QInverse.xyz, V),
        QInverse.w - dot(QInverse.xyz, V)
    );
	Out = Q.xyz * Q1.w + Q1.xyz * Q.w + cross(Q1.xyz, Q.xyz);
} 
";
    }
}

[Title("Math", "Quaternion", "Quaternion Slerp")]
public class QuaternionSlerpNode : CodeFunctionNode {
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
    float cosHalfAngle = P.w * Q.w + dot(P.xyz, Q.xyz);
    float halfAngle = acos(cosHalfAngle);
    float sinHalfAngle = sin(halfAngle);
    float oneOverSinHalfAngle = 1 / sinHalfAngle;
    float blendP = sin(halfAngle * (1 - T)) * oneOverSinHalfAngle;
    float blendQ = sin(halfAngle * T) * oneOverSinHalfAngle;
    float4 Out4 = blendP * (cosHalfAngle > 0 ? P : -P) + blendQ * Q;
    Out = normalize(Out4);
} 
";
    }
}
#endif