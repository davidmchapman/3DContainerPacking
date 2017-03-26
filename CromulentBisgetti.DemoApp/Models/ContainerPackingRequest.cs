using CromulentBisgetti.ContainerPacking.Entities;
using System.Collections.Generic;

namespace CromulentBisgetti.DemoApp.Models
{
	public class ContainerPackingRequest
	{
		public int ContainerID { get; set; }

		public decimal ContainerLength { get; set; }

		public decimal ContainerWidth { get; set; }

		public decimal ContainerHeight { get; set; }

		public List<Item> ItemsToPack { get; set; }

		public List<int> AlgorithmTypeIDs { get; set; }
	}
}