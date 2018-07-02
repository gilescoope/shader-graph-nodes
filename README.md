# shader-graph-nodes

Custom Nodes for Unity Shader Graph
# Pattern

This is a set of custom nodes for use with Shader Graph to make geometric patterns.

- Zig Zags
![](https://media.giphy.com/media/dtC1zxTtibhfVjzsgN/giphy.gif)
- Sine Waves
![](https://media.giphy.com/media/C8ClFsLJibZ5H1CXac/giphy.gif)
- Round Waves
![](https://media.giphy.com/media/nDVfd6ko6aZkHD3Uef/giphy.gif)
- Spiral
![](https://media.giphy.com/media/5t1Yo55UyjL4nuUWZS/giphy.gif)
- Whirl
![](https://media.giphy.com/media/pVR9GTqVrKLWCCz6Ew/giphy.gif)
- Dots
![](https://media.giphy.com/media/5eG2go6HvKhW73AUuO/giphy.gif)

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

# Halftone

This is a set of custom nodes for use with Shader Graph that allow you to do halftone rendering.

https://en.wikipedia.org/wiki/Halftone

- Halftone Circle
- Halftone Circle Color
- Halftone Square
- Halftone Square Color
- Halftone Smooth
- Halftone Smooth Color

The offset vector input defines the scale and angle of the halftone pattern.

To use simply add the .cs file to your project then inside Shader Graph click Create Node > Halftone and use one of the nodes.
