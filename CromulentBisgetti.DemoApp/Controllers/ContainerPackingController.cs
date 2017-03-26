using CromulentBisgetti.ContainerPacking;
using CromulentBisgetti.ContainerPacking.Algorithms;
using CromulentBisgetti.ContainerPacking.Entities;
using CromulentBisgetti.DemoApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;

namespace CromulentBisgetti.DemoApp.Controllers
{
	/// <summary>
	/// The API controller for container packing.
	/// </summary>
	public class ContainerPackingController : ApiController
	{
		/// <summary>
		/// Posts the specified packing request.
		/// </summary>
		/// <param name="request">The packing request.</param>
		/// <returns>A container packing result with lists of packed and unpacked items.</returns>
		[HttpPost]
		public ContainerPackingResult Post([FromBody]ContainerPackingRequest request)
		{
			Container container = new Container(request.ContainerID, request.ContainerLength, request.ContainerWidth, request.ContainerHeight);
<<<<<<< HEAD
			return PackingService.Pack(container, request.ItemsToPack, request.AlgorithmTypeIDs);
=======
			AlgorithmBase algorithm = PackingService.GetPackingAlgorithmFromTypeID(1);
			
			return PackingService.Pack(container, request.ItemsToPack, algorithm);
>>>>>>> parent of 684e6aa... Changed AlgorithmBase to IPackingAlgorithm.
		}
	}
}