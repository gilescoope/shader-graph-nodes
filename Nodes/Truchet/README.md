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
