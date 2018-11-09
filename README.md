## 3D Container Packing in C#

This is a C# library that can be used to find 3D container packing solutions. It includes an implementation of the EB-AFIT packing algorithm originally developed as a master's thesis project by Erhan Baltacıoğlu (EB) at the U.S. Air Force Institute of Technology (AFIT) in 2001. This algorithm is also described in The Distributor's Three-Dimensional Pallet-Packing Problem: A Human Intelligence-Based Heuristic Approach, by Erhan Baltacıoğlu, James T. Moore, and Raymond R. Hill Jr., published in the International Journal of Operational Research in 2006 (volume 1, issue 3).

The EB-AFIT algorithm supports full item rotation and has excellent runtime performance and container utilization.

## Usage

Start by including the ContainerPacking project in your solution.

Create a list of Container objects, which describes the dimensions of the containers:

    List<Container> containers = new List<Container>();
    containers.Add(new Container(id, length, width, height));
    ...

Create a list of items to pack:

    List<Item> itemsToPack = new List<Item>();
    itemsToPack.Add(new Item(id, dim1, dim2, dim3, quantity));
    ...

Create a list of algorithm IDs corresponding to the algorithms you would like to use. (Currently EB-AFIT is the only algorithm implemented.) Algorithm IDs are listed in the AlgorithmType enum.

    List<int> algorithms = new List<int>();
    algorithms.Add((int)AlgorithmType.EB_AFIT);
    ...

Call the Pack method on your container list, item list, and algorithm list:

    List<ContainerPackingResult> result = PackingService.Pack(containers, itemsToPack, algorithms);

The list of ContainerPackingResults contains a ContainerPackingResult object for each container. Within each ContainerPackingResult is the container ID and a list of AlgorithmPackingResult objects, one for each algorithm requested. Within each algorithm packing result is the name and ID of the algorithm used, a list of items that were successfully packed, a list of items that could not be packed, and a few other packing metrics. The items in the packed list are in pack order and include x, y, and z coordinates and x, y, and z pack dimensions. This information is useful if you want to attach a visualization tool to display the packed items in their proper pack locations and orientations.

Internally, the Pack() method will try to pack all the containers with all the items using all the requested algorithms in parallel. If you have a list of containers you want to try, but want them to run serially, then you can call Pack() with one container at a time. For example, if you want to run a large set of containers but would like to update the user interface as each one finishes, then you would want to call Pack() multiple times asynchronously and update the UI as each result returns.

## Demo WebAPI Application

[Live demo](https://containerpacking.moniapp.a2hosted.com/)
Demo hosted by user @EdgarSalazar, and not maintained nor endorsed by this project's owner.

This project also includes a demo web application that lets the user specify an arbitrary set of items, an arbitrary set of containers, and the packing algorithms to use. AJAX packing requests are sent to the server and handled by a WebAPI controller. Once returned, each pack solution can be viewed in the WebGL visualization tool by clicking the camera icon. 

![Container packing visualization](https://github.com/davidmchapman/3DContainerPacking/blob/master/images/packing-1.gif?raw=true "Container Packing")

## Acknowledgements and Related Projects

This project would not have been possible without the support of Dr. Raymond Hill at the Air Force Institute of Technology. It also leans heavily on the original C code included in Erhan Baltacıoğlu's thesis, which was discovered and resurrected by Bill Knechtel (GitHub user wknechtel), and ported to JavaScript by GitHub user keremdemirer.

https://github.com/wknechtel/3d-bin-pack/

https://github.com/keremdemirer/3dbinpackingjs