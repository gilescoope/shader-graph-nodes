# shader-graph-nodes

Custom Nodes for Unity Shader Graph, to be used in newer Unity versions with the SRP / URP / HDRP.\
It uses CodeFunctionNode API, which was made internal in 2018.2, but is now accessible using 
[AsmRef](https://docs.unity.cn/2019.4/Documentation/Manual/class-AssemblyDefinitionReferenceImporter.html).

**Requires Unity 2019.4 or higher.**\
If you wish to use it in older versions, you'll need to remove .asmref file in Nodes folder.

[SDF](https://github.com/gilescoope/shader-graph-nodes/tree/master/Nodes/SDF)

![](https://media.giphy.com/media/6bdccghcG0TUc1uhbT/giphy.gif)

Signed distance functions for interesting procedural effects.

[Truchet](https://github.com/gilescoope/shader-graph-nodes/tree/master/Nodes/Truchet)

![](https://media.giphy.com/media/E1wbE3JmT8EnNU80Ij/giphy.gif)

Truchet tiling nodes to make irregular patterns.

[Composite](https://github.com/gilescoope/shader-graph-nodes/tree/master/Nodes/Composite)

![](https://media.giphy.com/media/vguUknl0cv6I8roYOo/giphy.gif)

Complete set of Porter Duff transparency operations.

[Halftone](https://github.com/gilescoope/shader-graph-nodes/tree/master/Nodes/Halftone)

![](https://media.giphy.com/media/27IZAO2XUA4ftXNADM/giphy.gif)

Halftone rendering, monochrome or color.

[Lab Color](https://github.com/gilescoope/shader-graph-nodes/tree/master/Nodes/Lab%20Color)

![](https://media.giphy.com/media/3rZT3jhdBvxHVWERsu/giphy.gif)

Manipulate colors in the perception-based color spaces.

[Pattern](https://github.com/gilescoope/shader-graph-nodes/tree/master/Nodes/Pattern)

![](https://media.giphy.com/media/1AIeYQqM3bocEvUG91/giphy.gif)

Procedural geometric patterns.

[Pixel Perfect](https://github.com/gilescoope/shader-graph-nodes/tree/master/Nodes/Pixel%20Perfect)

![](https://media.giphy.com/media/26S9rg68tQxOM1Qq5P/giphy.gif)

Pixel-wide primitives, no matter what the surface.

[Complex](https://github.com/gilescoope/shader-graph-nodes/tree/master/Nodes/Complex)

![](https://media.giphy.com/media/1k4TFmQR4VF2BlgP3V/giphy.gif)

Do complex arithmetic in shader graph.

[Quaternion](https://github.com/gilescoope/shader-graph-nodes/tree/master/Nodes/Quaternion)

![](https://media.giphy.com/media/kFMNHPazCEKS8ySaO2/giphy.gif)

Quaternion algebra for rotation calculations in the graph.

[Symmetry](https://github.com/gilescoope/shader-graph-nodes/tree/master/Nodes/Symmetry)

Reflection, rotation and tiling symmetry nodes.

[Random](https://github.com/gilescoope/shader-graph-nodes/tree/master/Nodes/Random)

Generate pseudorandom vectors, colors and quaternions.
