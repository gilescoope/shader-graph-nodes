# SDF

Signed distance functions give a single float value for each pixel that represents the distance to some boundary. They can be used for rendering crisp text at any resolution or used for produral art.

https://en.wikipedia.org/wiki/Signed_distance_function

## Primitives

These nodes create signed distance functions for primitive shapes. These can be translated and rotated, but any other transformation (scale, shear) may result in unexpected behaviour.

These SDFs can be sampled with the SDF Sample nodes and combined with the SDF Boolean nodes.

| Line | Circle |
|---|---|
| ![](https://i.imgur.com/y0CxcXQ.png) | ![](https://i.imgur.com/bf8YDGH.png) |
| A vertical line passing through (Position, 0)<br> with width Width| A circle centered at Position with radius Radius |

| Rectangle | Polygon |
|---|---|
| ![](https://i.imgur.com/3MtHoIX.png) | ![](https://i.imgur.com/LZSOUF4.png) |
| A Rectangle centered at Position with width Width and height Height. Increasing the Corner Radius creates a rounded rectangle | A regular polygon centered at Position<br> with radius Radius and number of sides Sides (not necessarily an integer). Increasing the Corner Radius creates a rounded polygon |

## Sampling

These nodes can sample the SDFs to give clear shapes. They are anti-aliased at the edges.

| SDF Sample | SDF Sample Strip |
|---|---|
| ![](https://i.imgur.com/7b8mPgw.png) | ![](https://i.imgur.com/wooHvcZ.png) |
| Outputs values less than Offset as white and values greater as black | Outputs values between Offset.x and Offset.y as white and others as black |

## Boolean operators

These nodes allow the combining of SDFs with each other and creation of new SDFs as a result. There are both hard and soft boolean operators. Here we use a primitive Square A and a primitive Circle B to show the possibilities.

|| A | B |
|---|---|---|
| SDF | ![](https://i.imgur.com/NloxNAR.png) | ![](https://i.imgur.com/Mf4rx5L.png)  |
|Sampled|  ![](https://i.imgur.com/oJQxGU8.png) | ![](https://i.imgur.com/KGiYy6q.png) |

| A Union B | A Intersect B | A Difference B |
|---|---|---|
| ![](https://i.imgur.com/rqqDtaX.png) | ![](https://i.imgur.com/KPQdVjY.png) | ![](https://i.imgur.com/2T37Tv7.png) |
| ![](https://i.imgur.com/ZRlgS3D.png)  | ![](https://i.imgur.com/q0ppSQg.png) | ![](https://i.imgur.com/aRwlFPh.png) |

| A Soft Union B | A Soft Intersect B | A Soft Difference B |
|---|---|---|
| ![](https://i.imgur.com/dJtwdsc.png) | ![](https://i.imgur.com/9as2b4e.png) | ![](https://i.imgur.com/8yaLzjG.png) |
| ![](https://i.imgur.com/F4eeylZ.png)  | ![](https://i.imgur.com/y8FqLpm.png) | ![](https://i.imgur.com/22QgnzD.png) |

The Smoothing input controls the amount of smoothing between the two primitives.
