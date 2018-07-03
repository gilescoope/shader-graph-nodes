# shader-graph-nodes

Custom Nodes for Unity Shader Graph

# Truchet

This is a set of custom nodes for use with Shader Graph to make random Truchet tilings procedurally.

https://en.wikipedia.org/wiki/Truchet_tiles

## Square

A grid of squares. Here we use these two tiles.

![](https://i.imgur.com/5Dro33N.png)
![](https://i.imgur.com/g5soK2Z.png)

The tiles are placed, rotated and reflected randomly as so.

![](https://i.imgur.com/N45wDdm.png)

Here's the final result.

![](https://i.imgur.com/FeKdbZQ.png)

## Triangle

A grid of equilateral triangles. This produces a hexagonal looking result.

![](https://i.imgur.com/JHnDAkM.png)

## Hexagon

A grid of regular hexagons.

![](https://i.imgur.com/TuwvUIb.png)

Here we can see the different patterns that can be produced from the same tile as above. Firstly without rotation or reflection.

![](https://i.imgur.com/csCGQCq.png)

Here the tile is only reflected horizontally.

![](https://i.imgur.com/wJrKjVo.png)

## Usage

![](https://i.imgur.com/htzzQlo.png)

The three shape nodes all work the same way.

- The position input takes the input position of the pixel considered. Recommended inputs are the world/object position or UV.
- Size controls the scale of the pattern.
- Number is the numbers of different tiles you have, if you have a Texture2dArray this would be the size of the array.
- Seed is a seed value for the pseudorandom number generator. Change this for a new pattern.
- Rotate controls whether to allow rotation of the tiles.
- Reflect controls whether to allow horizontal reflection of the tiles.

- The Index output is an integer in the range \[0, Number) that says which tile should be drawn, here it is used as an input for the Texture2DArray
- The UV output is a Vector2 that is the UV coordinate of the pixel on the tile. For a square the tile texture is the same as the whole tile, the hexagon fills the tile horizontally and is centred, the triangle has a vertex at the top that touches the edge of the tile and is centred.

As well as texture arrays we can use these UVS to reference procedural shapes. Here is an example where some rectangles rotate over time.

![](https://media.giphy.com/media/fdCJBoak4aWcbyYgJS/giphy.gif)

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

## Pixel Point / Pixel Point Grid

An individual point or square grid of points. Posiiton controls the position of one of the points, width the distance between adjacent points.

## Pixel Ray / Pixel Rays / Pixel Line / Pixel Lines

![](https://media.giphy.com/media/9DppWGWR0eGpJ774dF/giphy.gif)

A pixel-wide line determined by two endpoints. The pixel ray extends to infinity rather than terminating at the endpoints. Width controls the distance between the multiple lines or rays for these nodes.

## Pixel Circle

![](https://media.giphy.com/media/5821ScmvECB4hphcJl/giphy.gif)

A pixel-wide circle. Center controls the center of the circle while Radius controls the radius.

## Pixel Polygon

![](https://media.giphy.com/media/8hZ9I0vajgeCTYAZO9/giphy.gif)

A pixel-wide polygon. Center controls the center of the polygon while Radius controls the radius. Sides is the number of sides, this needn't be an integer. Angle is the orientation of the whole shape in degrees.

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

The ink spots blend between black circles and white circles for light and dark areas respectively.

## Usage

All are available in monotone and three channel colour variants.

The offset vector input defines the scale and angle of the halftone pattern.

To use simply add the .cs file to your project then inside Shader Graph click Create Node > Halftone and use one of the nodes.
