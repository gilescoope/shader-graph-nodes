# Composite

This Shader Graph node is for alpha compositing two rgba channels using Porter Duff equations.

https://en.wikipedia.org/wiki/Alpha_compositing

Here we see what happens if we composite a blue square A (71, 154, 239, 0.62) and a pink circle B (218, 31, 100, 0.62) using the different composite modes.

|  A | B  |
|---|---|
|  ![](https://i.imgur.com/Q6ULhuF.png) | ![](https://i.imgur.com/wuuEaSK.png)  |

|  A Over B | A In B  | A Out B | A Atop B | A Xor B |
|---|---|---|---|---|
| ![](https://i.imgur.com/UTbi2S0.png) | ![](https://i.imgur.com/kqB3JEd.png) | ![](https://i.imgur.com/vV2i9Of.png) | ![](https://i.imgur.com/r1l2XPU.png) | ![](https://i.imgur.com/vbkoo9x.png) |

Over is the regular painter's algorithm, equivalent to Blend > Normal in Photoshop.

## Usage

Simply place the Composition > Composite node in your shader graph, plug in your A and B and select the desired blend node.

![](https://media.giphy.com/media/17pO92in9DplZOYMmJ/giphy.gif)

The trace colors here are just from .gif compression.
