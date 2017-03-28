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
		public List<ContainerPackingResult> Post([FromBody]ContainerPackingRequest request)
		{
			return PackingService.Pack(request.Containers, request.ItemsToPack, request.AlgorithmTypeIDs);
		}
	}
}