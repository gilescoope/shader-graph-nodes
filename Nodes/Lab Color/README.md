# Lab Color

These nodes are for doing color work in Lab and LCH color spaces. 

https://en.wikipedia.org/wiki/CIELAB_color_space

Lab and LCH are designed to approximate human vision so can more useful than RGB and HSV when the perception of color is important.

## Lab Colorspace Conversion

![](https://i.imgur.com/Bdw7Ogs.png)

This node converts between RGB, Lab and LCH color spaces. Alpha is preserved.

## Color Schemes

These nodes use the LCH color space to output color schemes from the color wheel. According to color theory these colors should be harmonious with one another.

|Complementary| Split| Dual|
|---|---|---|
|![](https://i.imgur.com/nPdW4wR.png)|![](https://i.imgur.com/EAQns3U.png)|![](https://i.imgur.com/zi0JXxm.png)|
| Here the Out hue is opposite the In hue | The Out colors lie each an angle Angle away from In<br>An Angle < 90 will give an adjacent scheme<br>An Angle > 120 will give a split complementary scheme<br>An Angle = 120 will give a triadic scheme | The Out colors lie in a rectangle defined by In and Angle<br>An Angle = 90 will give a tetradic scheme |
