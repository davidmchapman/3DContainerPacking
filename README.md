## 3D Container Packing in C#

This is a fork of David Chapman's C# library that can be used to find 3D container packing solutions (also known as 3D bin packing). It includes an implementation of the EB-AFIT packing algorithm originally developed as a master's thesis project by Erhan Baltacıoğlu (EB) at the U.S. Air Force Institute of Technology (AFIT) in 2001 [Link to Abstract](http://betterwaysystems.github.io/packer/reference/AirForceBinPacking.pdf). This algorithm is also described in The Distributor's Three-Dimensional Pallet-Packing Problem: A Human Intelligence-Based Heuristic Approach, by Erhan Baltacıoğlu, James T. Moore, and Raymond R. Hill Jr., published in the International Journal of Operational Research in 2006 (volume 1, issue 3).

The EB-AFIT algorithm supports full item rotation and has excellent runtime performance and container utilization.

## Usage

This extends the original project in the following ways:

* Randomization of packing units and items
* Updates the 3D model to display labels of each item (adds label property)
* Adds gross weight property
* Allows selection of one or more packing units, displaying weight and volume
* Allows selection of stops, so that containers can be loaded according when they need to be removed first

Future:

* Allow generation of weight heatmap, so loading algorthim favors forward and center loads

## Acknowledgements and Related Projects

This project would not have been possible without the support of Dr. Raymond Hill at the Air Force Institute of Technology. It also leans heavily on the original C code included in Erhan Baltacıoğlu's thesis, which was discovered and resurrected by Bill Knechtel (GitHub user wknechtel), and ported to JavaScript by GitHub user keremdemirer. Also to David Chapman for his work on putting this together in .NET, and letting me carry the torch.

https://github.com/davidmchapman/3DContainerPacking

https://github.com/wknechtel/3d-bin-pack/

https://github.com/keremdemirer/3dbinpackingjs
