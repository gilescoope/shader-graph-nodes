# shader-graph-nodes

Custom Nodes for Unity Shader Graph
# Pattern

This is a set of custom nodes for use with Shader Graph to make geometric patterns procedurally. All patterns are anti aliased.

## Zig Zags

Stripes made up of zig zags.

![](https://media.giphy.com/media/8JThrbgXEu597ygC77/giphy.gif)
- Widths controls the widths of the black and white stripes respectively.
- Wavelength controls the wavelength of the wave.
- Amplitude controls peak to peak amplitude of the wave.

## Sine Waves

Stripes made up of sine waves.

![](https://media.giphy.com/media/d7p9KoFKXnatt2C1S2/giphy.gif)
- Widths controls the widths of the black and white stripes respectively.
- Wavelength controls the wavelength of the wave.
- Amplitude controls peak to peak amplitude of the wave.
## Round Waves

Stripes made up of arc segments.

![](https://media.giphy.com/media/nDVfd6ko6aZkHD3Uef/giphy.gif)
- Widths controls the widths of the black and white stripes respectively.
- Wavelength controls the wavelength of the wave.
- Amplitude controls peak to peak amplitude of the wave.
## Spiral

An archimedean spiral.

![](https://media.giphy.com/media/5t1Yo55UyjL4nuUWZS/giphy.gif)
- Widths controls the widths of the black and white stripes respectively.

## Whirl

A swirling shape around a point.

![](https://media.giphy.com/media/pVR9GTqVrKLWCCz6Ew/giphy.gif)
- Widths controls the widths of the black and white stripes in angles respectively.
- Whirl controls the amount of swirl.

## Dots

![](https://media.giphy.com/media/5eG2go6HvKhW73AUuO/giphy.gif)
- Spacing controls the distances between the dots.
- Offset controls the displacement of each row relative to the one below.
- Radius controls the radius of the dots.

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

![](https://media.giphy.com/media/c6Xp99uq4cBMuoFLH5/giphy.gif)

https://en.wikipedia.org/wiki/Halftone

## Halftone Circle

The inks spots are circles.

## Halftone Square

The ink spots are squares

## Halftone Smooth

The ink tones blend between circles and inverted circles for light and dark ares respectively.

## Usage

All are available in monotone and three channel colour variants.

The offset vector input defines the scale and angle of the halftone pattern.

To use simply add the .cs file to your project then inside Shader Graph click Create Node > Halftone and use one of the nodes.
