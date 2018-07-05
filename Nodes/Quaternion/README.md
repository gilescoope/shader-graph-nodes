# Quaternion

This is a set of custom nodes for use with Shader Graph that allow you to do quaternion calculations inside the graph.

Quaternions are inputted and outputted as Vector4 values. Quaternions here describe rotations so at times may be assumed to have a magnitude of 1.

| Node | In | Out |
|---|---|---|
| Quaternion Inverse | Q | Out: Q ^ -1 , the inverse rotation |
| Quaternion From Euler | V | Out: rotation that rotates V.z degrees around the z axis, V.x degrees around the x axis, and V.y degrees around the y axis |
| Quaternion From Angle Axis | Angle<br>Axis | Out: rotation which rotates Angle degrees around Axis |
| Quaternion To Angle Axis | Q | Angle: angle of rotation in degrees<br>Axis = axis of rotation |
| Quaternion From To Rotation | From<br>To |  Out: rotation that rotates from From to To |
| Quaternion Multiply | P<br>Q | Out: P ⋅ Q |
| Quaternion Rotate Vector | V<br>Q | Out: The vector V rotated by the quaternion Q |
| Quaternion Slerp | P<br>Q<br>T | Out: rotation give by spherical linear interpolation of P to Q by factor T |

To use simply add the .cs file to your project then inside Shader Graph click Create Node > Math > Quaternion and use one of the nodes.
