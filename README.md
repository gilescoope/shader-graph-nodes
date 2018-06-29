# shadergraphnodes

Custom Nodes for Unity Shader Graph

# Quaternion

This is a set of custom nodes for use with Shader Graph that allow you to do quaternion calculations inside the graph.

- Quaternion Inverse
- Quaternion From Euler
- Quaternion From Angle Axis
- Quaternion To Angle Axis
- Quaternion From To Rotation
- Quaternion Multiply
- Quaternion Rotate Vector
- Quaternion Slerp

Quaternions are inputted and outputted as Vector4 values. All angles are in degrees.

To use simply add the .cs file to your project then inside Shader Graph click Create Node > Math > Quaternion and use one of the nodes.

# Pixel Perfect

This is a set of custom nodes for use with Shader Graph that allow you to do pixel perfect drawing of primitives. The lines and points will be exactly one pixel wide regardless of the distance and perspective of the object being rendered to.

- Pixel Point
- Pixel Point Grid
- Pixel Ray
- Pixel Rays
- Pixel Line
- Pixel Lines
- Pixel Circle
- Pixel Polygon

All angles are in degrees.

To use simply add the .cs file to your project then inside Shader Graph click Create Node > Pixel Perfect and use one of the nodes.
