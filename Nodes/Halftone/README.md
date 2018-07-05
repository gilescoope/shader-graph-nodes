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
