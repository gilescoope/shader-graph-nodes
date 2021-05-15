using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.ShaderGraph;
using UnityEngine;


[Title("Math", "Complex", "Complex Conjugate")]
internal class ComplexConjugateNode : CodeFunctionNode {
	public ComplexConjugateNode() {
		name = "Complex Conjugate";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("ComplexConjugate", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string ComplexConjugate([Slot(0, Binding.None)] Vector2 A, [Slot(1, Binding.None)] out Vector2 Out) {
		Out = Vector2.zero;
		return @"
{
    Out = float2(A.x, -A.y);
}
";
	}
}

[Title("Math", "Complex", "Complex Reciprocal")]
internal class ComplexReciprocalNode : CodeFunctionNode {
	public ComplexReciprocalNode() {
		name = "Complex Reciprocal";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("ComplexReciprocal", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string ComplexReciprocal([Slot(0, Binding.None)] Vector2 A, [Slot(1, Binding.None)] out Vector2 Out) {
		Out = Vector2.zero;
		return @"
{
    Out = float2(A.x, -A.y)/(A.x*A.x + A.y*A.y);
}
";
	}
}

[Title("Math", "Complex", "Complex Multiply")]
internal class ComplexMultiplyNode : CodeFunctionNode {
	public ComplexMultiplyNode() {
		name = "Complex Multiply";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("ComplexMultiply", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string ComplexMultiply([Slot(0, Binding.None)] Vector2 A, [Slot(1, Binding.None)] Vector2 B, [Slot(2, Binding.None)] out Vector2 Out) {
		Out = Vector2.zero;
		return @"
{
    Out = float2(A.x*B.x - A.y*B.y, A.x*B.y + A.y*B.x);
}
";
	}
}

[Title("Math", "Complex", "Complex Divide")]
internal class ComplexDivideNode : CodeFunctionNode {
	public ComplexDivideNode() {
		name = "Complex Divide";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("ComplexDivide", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string ComplexDivide([Slot(0, Binding.None)] Vector2 A, [Slot(1, Binding.None)] Vector2 B, [Slot(2, Binding.None)] out Vector2 Out) {
		Out = Vector2.zero;
		return @"
{
    Out = float2(A.x*B.x + A.y*B.y, A.y*B.x - A.x*B.y)/(B.x*B.x + B.y*B.y);
}
";
	}
}

[Title("Math", "Complex", "Complex Exponential")]
internal class ComplexExponentialNode : CodeFunctionNode {
	public ComplexExponentialNode() {
		name = "Complex Exponential";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("ComplexExponential", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string ComplexExponential([Slot(0, Binding.None)] Vector2 A, [Slot(1, Binding.None)] out Vector2 Out) {
		Out = Vector2.zero;
		return @"
{
	float Sin, Cos;
	sincos(A.y, Sin, Cos);
	Out = exp(A.x)*float2(Cos, Sin);
}
";
	}
}

[Title("Math", "Complex", "Complex Logarithm")]
internal class ComplexLogarithmNode : CodeFunctionNode {
	public ComplexLogarithmNode() {
		name = "Complex Logarithm";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("ComplexLogarithm", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string ComplexLogarithm([Slot(0, Binding.None)] Vector2 A, [Slot(1, Binding.None)] out Vector2 Out) {
		Out = Vector2.zero;
		return @"
{
	Out = float2(log(sqrt(A.x*A.x + A.y*A.y)), atan2(A.y, A.x));
}
";
	}
}

[Title("Math", "Complex", "Complex Power")]
internal class ComplexPowerNode : CodeFunctionNode {
	public ComplexPowerNode() {
		name = "Complex Power";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("ComplexPower", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string ComplexPower([Slot(0, Binding.None)] Vector2 A, [Slot(1, Binding.None)] Vector2 B, [Slot(2, Binding.None)] out Vector2 Out) {
		Out = Vector2.zero;
		return @"
{
	float arg = atan2(A.y, A.x);
	float len2 = A.x*A.x + A.y*A.y;
	float Sin, Cos;
	sincos(B.x*arg + 0.5*B.y*log(len2), Sin, Cos);
	Out = (pow(len2, 0.5*B.x)*exp(-B.y*arg))*float2(Cos, Sin);
}
";
	}
}