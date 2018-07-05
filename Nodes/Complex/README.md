# Complex

These nodes allow complex arithmetic inside the shader graph. The x component of each Vector2 represents the real part of the complex number, the y component the imaginary part.

| Node  |  Result  |
|---|---|
|  Complex Conjugate  | Out = Ā |
|  Complex Reciprocal | Out = 1 / A |
|  Complex Multiply | Out = A × B |
|  Complex Divide | Out = A / B |
|  Complex Logarithm |  Out = ln (A) |
|  Complex Exponential  | Out = e ^ A  |
|  Complex Power  |  Out = A ^ B |

Addition and subtraction can be accomplished with the standard vector nodes. The modulus and argument of a complex number can be extracted using the polar coordinates node.
