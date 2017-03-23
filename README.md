## 3D Container Packing in C#

This is a C# library that can be used to find 3D container packing solutions. It includes an implementation of the EB-AFIT packing algorithm originally developed as a master's thesis project by Erhan Baltacıoğlu (EB) at the U.S. Air Force Institute of Technology (AFIT) in 2001. This algorithm is also described in The Distributor's Three-Dimensional Pallet-Packing Problem: A Human Intelligence-Based Heuristic Approach, by Erhan Baltacıoğlu, James T. Moore, and Raymond R. Hill Jr., published in the International Journal of Operational Research in 2006 (volume 1, issue 3).

The EB-AFIT algorithm supports full item rotation and has excellent runtime performance and container utilization.

## Usage

Include the ContainerPacking project in your solution. Create a new instance of the algorithm to use for packing (currently EB-AFIT only), either directly or by using one of the algorithm type IDs included in the AlgorithType enum:

    AlgorithmBase algorithm = PackingService.GetPackingAlgorithmFromTypeID(1);

Create a Container object, which describes the dimensions of the container:

    Container container = new Container(id, length, width, height);

Create a list of items to pack:

    List<Item> itemsToPack = new List<Item>();
    itemsToPack.Add(new Item(id, dim1, dim2, dim3, quantity));
    ...

Call the Pack method on your container and item list:

    ContainerPackingResult result = PackingService.Pack(container, request.ItemsToPack, algorithm);

The ContainerPackingResult contains the container ID, a list of items that were successfully packed, a list of items that could not be packed, and a few other packing metrics. The items in the packed list are in pack order and include x, y, and z coordinates and x, y, and z pack dimensions. This information is useful if you want to attach a visualization tool to display the packed items in their proper pack locations and orientations.

## Demo WebAPI Application

This project also includes a demo web application that lets the user specify an arbitrary set of items and an arbitrary set of containers. AJAX packing requests are sent to the server and handled by a WebAPI controller. Once returned, each pack solution can be viewed in the WebGL visualization tool by clicking the camera icon. 

![Container packing visualization](https://github.com/davidmchapman/3DContainerPacking/images/packing1.gif?raw=true "Container Packing")

## Acknowledgements and Related Projects

This project would not have been possible without the support of Dr. Raymond Hill at the Air Force Institute of Technology. It also leans heavily on the original C code included in Erhan Baltacıoğlu's thesis, which was discovered and ressurected by Bill Knechtel (GitHub user wknechtel), and ported to JavaScript by GitHub user keremdemirer:

https://github.com/wknechtel/3d-bin-pack/

https://github.com/keremdemirer/3dbinpackingjs