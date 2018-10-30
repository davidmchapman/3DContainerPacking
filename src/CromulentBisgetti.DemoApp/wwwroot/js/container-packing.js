var scene;
var camera;
var renderer;
var controls;
var viewModel;
var itemMaterial;

async function PackContainers(request) {
	return $.ajax({
		url: '/api/containerpacking',
		type: 'POST',
		data: request,
		contentType: 'application/json; charset=utf-8'
	});
};

function InitializeDrawing() {
	var container = $('#drawing-container');

	scene = new THREE.Scene();
	camera = new THREE.PerspectiveCamera( 50, window.innerWidth/window.innerHeight, 0.1, 1000 );
	camera.lookAt(scene.position);

	//var axisHelper = new THREE.AxisHelper( 5 );
	//scene.add( axisHelper );

	// LIGHT
	var light = new THREE.PointLight(0xffffff);
	light.position.set(0,150,100);
	scene.add(light);

	// Get the item stuff ready.
	itemMaterial = new THREE.MeshNormalMaterial( { transparent: true, opacity: 0.6 } );

	renderer = new THREE.WebGLRenderer( { antialias: true } ); // WebGLRenderer CanvasRenderer
	renderer.setClearColor( 0xf0f0f0 );
	renderer.setPixelRatio( window.devicePixelRatio );
	renderer.setSize( window.innerWidth / 2, window.innerHeight / 2);
	container.append( renderer.domElement );

	controls = new THREE.OrbitControls( camera, renderer.domElement );
	window.addEventListener( 'resize', onWindowResize, false );

	animate();
};

function onWindowResize() {
	camera.aspect = window.innerWidth / window.innerHeight;
	camera.updateProjectionMatrix();
	renderer.setSize( window.innerWidth / 2, window.innerHeight / 2 );
}
//
function animate() {
	requestAnimationFrame( animate );
	controls.update();
	render();
}
function render() {
	renderer.render( scene, camera );
}

var ViewModel = function () {
	var self = this;

	self.ItemCounter = 0;
	self.ContainerCounter = 0;

	self.ItemsToRender = ko.observableArray([]);
	self.LastItemRenderedIndex = ko.observable(-1);

	self.ContainerOriginOffset = {
		x: 0,
		y: 0,
		z: 0
	};

	self.AlgorithmsToUse = ko.observableArray([]);
	self.ItemsToPack = ko.observableArray([]);
	self.Containers = ko.observableArray([]);

	self.NewItemToPack = ko.mapping.fromJS(new ItemToPack());
	self.NewContainer = ko.mapping.fromJS(new Container());

	self.GenerateItemsToPack = function () {
		self.ItemsToPack([]);
		self.ItemsToPack.push(ko.mapping.fromJS({ ID: 1000, Name: 'Item1', Length: 5, Width: 4, Height: 2, Quantity: 1 }));
		self.ItemsToPack.push(ko.mapping.fromJS({ ID: 1001, Name: 'Item2', Length: 2, Width: 1, Height: 1, Quantity: 3 }));
		self.ItemsToPack.push(ko.mapping.fromJS({ ID: 1002, Name: 'Item3', Length: 9, Width: 7, Height: 3, Quantity: 4 }));
		self.ItemsToPack.push(ko.mapping.fromJS({ ID: 1003, Name: 'Item4', Length: 13, Width: 6, Height: 3, Quantity: 8 }));
		self.ItemsToPack.push(ko.mapping.fromJS({ ID: 1004, Name: 'Item5', Length: 17, Width: 8, Height: 6, Quantity: 1 }));
		self.ItemsToPack.push(ko.mapping.fromJS({ ID: 1005, Name: 'Item6', Length: 3, Width: 3, Height: 2, Quantity: 2 }));
	};
	
	self.GenerateContainers = function () {
		self.Containers([]);
		self.Containers.push(ko.mapping.fromJS({ ID: 1000, Name: 'Box1', Length: 15, Width: 13, Height: 9, AlgorithmPackingResults: [] }));
		self.Containers.push(ko.mapping.fromJS({ ID: 1001, Name: 'Box2', Length: 23, Width: 9, Height: 4, AlgorithmPackingResults: [] }));
		self.Containers.push(ko.mapping.fromJS({ ID: 1002, Name: 'Box3', Length: 16, Width: 16, Height: 6, AlgorithmPackingResults: [] }));
		self.Containers.push(ko.mapping.fromJS({ ID: 1003, Name: 'Box4', Length: 10, Width: 8, Height: 5, AlgorithmPackingResults: [] }));
		self.Containers.push(ko.mapping.fromJS({ ID: 1004, Name: 'Box5', Length: 40, Width: 28, Height: 20, AlgorithmPackingResults: [] }));
		self.Containers.push(ko.mapping.fromJS({ ID: 1005, Name: 'Box6', Length: 29, Width: 19, Height: 4, AlgorithmPackingResults: [] }));
		self.Containers.push(ko.mapping.fromJS({ ID: 1006, Name: 'Box7', Length: 18, Width: 13, Height: 1, AlgorithmPackingResults: [] }));
		self.Containers.push(ko.mapping.fromJS({ ID: 1007, Name: 'Box8', Length: 6, Width: 6, Height: 6, AlgorithmPackingResults: [] }));
		self.Containers.push(ko.mapping.fromJS({ ID: 1008, Name: 'Box9', Length: 8, Width: 5, Height: 5, AlgorithmPackingResults: [] }));
		self.Containers.push(ko.mapping.fromJS({ ID: 1009, Name: 'Box10', Length: 18, Width: 13, Height: 8, AlgorithmPackingResults: [] }));
		self.Containers.push(ko.mapping.fromJS({ ID: 1010, Name: 'Box11', Length: 17, Width: 16, Height: 15, AlgorithmPackingResults: [] }));
		self.Containers.push(ko.mapping.fromJS({ ID: 1011, Name: 'Box12', Length: 32, Width: 10, Height: 9, AlgorithmPackingResults: [] }));
		self.Containers.push(ko.mapping.fromJS({ ID: 1012, Name: 'Box13', Length: 60, Width: 60, Height: 60, AlgorithmPackingResults: [] }));
	};

	self.AddAlgorithmToUse = function () {
		var algorithmID = $('#algorithm-select option:selected').val();
		var algorithmName = $('#algorithm-select option:selected').text();
		self.AlgorithmsToUse.push({ AlgorithmID: algorithmID, AlgorithmName: algorithmName });
	};

	self.RemoveAlgorithmToUse = function (item) {
		self.AlgorithmsToUse.remove(item);
	};

	self.AddNewItemToPack = function () {
		self.NewItemToPack.ID(self.ItemCounter++);
		self.ItemsToPack.push(ko.mapping.fromJS(ko.mapping.toJS(self.NewItemToPack)));
		self.NewItemToPack.Name('');
		self.NewItemToPack.Length('');
		self.NewItemToPack.Width('');
		self.NewItemToPack.Height('');
		self.NewItemToPack.Quantity('');
	};

	self.RemoveItemToPack = function (item) {
		self.ItemsToPack.remove(item);
	};

	self.AddNewContainer = function () {
		self.NewContainer.ID(self.ContainerCounter++);
		self.Containers.push(ko.mapping.fromJS(ko.mapping.toJS(self.NewContainer)));
		self.NewContainer.Name('');
		self.NewContainer.Length('');
		self.NewContainer.Width('');
		self.NewContainer.Height('');
	};

	self.RemoveContainer = function (item) {
		self.Containers.remove(item);
	};

	self.PackContainers = function () {
		var algorithmsToUse = [];

		self.AlgorithmsToUse().forEach(algorithm => {
			algorithmsToUse.push(algorithm.AlgorithmID);
		});
		
		var itemsToPack = [];

		self.ItemsToPack().forEach(item => {
			var itemToPack = {
				ID: item.ID(),
				Dim1: item.Length(),
				Dim2: item.Width(),
				Dim3: item.Height(),
				Quantity: item.Quantity()
			};
			
			itemsToPack.push(itemToPack);
		});
		
		var containers = [];

		// Send a packing request for each container in the list.
		self.Containers().forEach(container => {
			var containerToUse = {
				ID: container.ID(),
				Length: container.Length(),
				Width: container.Width(),
				Height: container.Height()
			};

			containers.push(containerToUse);
		});
		
		// Build container packing request.
		var request = {
			Containers: containers,
			ItemsToPack: itemsToPack,
			AlgorithmTypeIDs: algorithmsToUse
		};
		
		PackContainers(JSON.stringify(request))
			.then(response => {
				// Tie this response back to the correct containers.
				response.forEach(containerPackingResult => {
					self.Containers().forEach(container => {
						if (container.ID() == containerPackingResult.ContainerID) {
							container.AlgorithmPackingResults(containerPackingResult.AlgorithmPackingResults);
						}
					});
				});
			});
	};
	
	self.ShowPackingView = function (algorithmPackingResult) {
		var container = this;
		var selectedObject = scene.getObjectByName('container');
		scene.remove( selectedObject );
		
		for (var i = 0; i < 1000; i++) {
			var selectedObject = scene.getObjectByName('cube' + i);
			scene.remove(selectedObject);
		}
		
		camera.position.set(container.Length(), container.Length(), container.Length());

		self.ItemsToRender(algorithmPackingResult.PackedItems);
		self.LastItemRenderedIndex(-1);

		self.ContainerOriginOffset.x = -1 * container.Length() / 2;
		self.ContainerOriginOffset.y = -1 * container.Height() / 2;
		self.ContainerOriginOffset.z = -1 * container.Width() / 2;

		var geometry = new THREE.BoxGeometry(container.Length(), container.Height(), container.Width());
		var geo = new THREE.EdgesGeometry( geometry ); // or WireframeGeometry( geometry )
		var mat = new THREE.LineBasicMaterial( { color: 0x000000, linewidth: 2 } );
		var wireframe = new THREE.LineSegments( geo, mat );
		wireframe.position.set(0, 0, 0);
		wireframe.name = 'container';
		scene.add( wireframe );
	};

	self.AreItemsPacked = function () {
		if (self.LastItemRenderedIndex() > -1) {
			return true;
		}

		return false;
	};

	self.AreAllItemsPacked = function () {
		if (self.ItemsToRender().length === self.LastItemRenderedIndex() + 1) {
			return true;
		}

		return false;
	};

	self.PackItemInRender = function () {
		var itemIndex = self.LastItemRenderedIndex() + 1;

		var itemOriginOffset = {
			x: self.ItemsToRender()[itemIndex].PackDimX / 2,
			y: self.ItemsToRender()[itemIndex].PackDimY / 2,
			z: self.ItemsToRender()[itemIndex].PackDimZ / 2
		};

		var itemGeometry = new THREE.BoxGeometry(self.ItemsToRender()[itemIndex].PackDimX, self.ItemsToRender()[itemIndex].PackDimY, self.ItemsToRender()[itemIndex].PackDimZ);
		var cube = new THREE.Mesh(itemGeometry, itemMaterial);
		cube.position.set(self.ContainerOriginOffset.x + itemOriginOffset.x + self.ItemsToRender()[itemIndex].CoordX, self.ContainerOriginOffset.y + itemOriginOffset.y + self.ItemsToRender()[itemIndex].CoordY, self.ContainerOriginOffset.z + itemOriginOffset.z + self.ItemsToRender()[itemIndex].CoordZ);
		cube.name = 'cube' + itemIndex;
		scene.add( cube );

		self.LastItemRenderedIndex(itemIndex);
	};

	self.UnpackItemInRender = function () {
		var selectedObject = scene.getObjectByName('cube' + self.LastItemRenderedIndex());
		scene.remove( selectedObject );
		self.LastItemRenderedIndex(self.LastItemRenderedIndex() - 1);
	};
};

var ItemToPack = function () {
	this.ID = '';
	this.Name = '';
	this.Length = '';
	this.Width = '';
	this.Height = '',
	this.Quantity = '';
}

var Container = function () {
	this.ID = '';
	this.Name = '';
	this.Length = '';
	this.Width = '';
	this.Height = '';
	this.AlgorithmPackingResults = [];
}

$(document).ready(() => {
	$('[data-toggle="tooltip"]').tooltip(); 
	InitializeDrawing();

	viewModel = new ViewModel();
	ko.applyBindings(viewModel);
});