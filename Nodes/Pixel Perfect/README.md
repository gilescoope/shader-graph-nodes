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
